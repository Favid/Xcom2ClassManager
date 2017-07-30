using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xcom2ClassManager.DataObjects;

namespace Xcom2ClassManager.Forms
{
    public partial class ImportNicknamesForm : Form
    {
        private ClassPack classPack;

        public ImportNicknamesForm(ClassPack classPack)
        {
            this.classPack = classPack;
            InitializeComponent();
            cSoldierClass.DataSource = classPack.soldierClasses;
            cSoldierClass.SelectedIndex = 0;
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            ValidationResult validFolder = new ValidationResult();
            validFolder.valid = false;

            OpenFileDialog dialog;

            do
            {
                dialog = new OpenFileDialog();
                DialogResult dialogResult = dialog.ShowDialog(this);

                if (dialogResult == DialogResult.OK)
                {
                    validFolder = validateIntFile(dialog.FileName);

                    if (!validFolder.valid)
                    {
                        MessageBox.Show(validFolder.message);
                    }
                }
                else
                {
                    return;
                }
            } while (!validFolder.valid);

            tFile.Text = dialog.FileName;
        }

        private ValidationResult validateIntFile(string fileName)
        {
            ValidationResult validFile = new ValidationResult();
            validFile.valid = true;
            validFile.message = "";

            if (!fileName.EndsWith("XComGame.INT", StringComparison.OrdinalIgnoreCase))
            {
                validFile.valid = false;
                validFile.message += "Must select XComGame.INT file.";
                validFile.message += "\n";
            }

            if (!validFile.valid)
            {
                validFile.message += "Please choose a new file.";
            }

            return validFile;
        }

        private void bImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tFile.Text))
            {
                return;
            }

            List<Ability> foundAbilities = new List<Ability>();

            int counter = 0;
            string line;
            
            StreamReader file = new StreamReader(tFile.Text);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains(" X2SoldierClassTemplate]"))
                {
                    string className = line.Substring(1, line.IndexOf(' ') - 1);
                    
                    List<ClassNickname> classNicknames = importClassNicknames(file, line);

                    List<TreeNode> unisexChildren = new List<TreeNode>();
                    List<ClassNickname> unisexNicknames = classNicknames.Where(x => x.gender == NicknameGender.Unisex).ToList();
                    foreach(ClassNickname unisexNickname in unisexNicknames)
                    {
                        unisexChildren.Add(new TreeNode(unisexNickname.nickname));
                    }
                    TreeNode unisexNode = new TreeNode("Unisex", unisexChildren.ToArray());

                    List<TreeNode> maleChildren = new List<TreeNode>();
                    List<ClassNickname> maleNicknames = classNicknames.Where(x => x.gender == NicknameGender.Male).ToList();
                    foreach (ClassNickname maleNickname in maleNicknames)
                    {
                        maleChildren.Add(new TreeNode(maleNickname.nickname));
                    }
                    TreeNode maleNode = new TreeNode("Male", maleChildren.ToArray());

                    List<TreeNode> femaleChildren = new List<TreeNode>();
                    List<ClassNickname> femaleNicknames = classNicknames.Where(x => x.gender == NicknameGender.Female).ToList();
                    foreach (ClassNickname femaleNickname in femaleNicknames)
                    {
                        femaleChildren.Add(new TreeNode(femaleNickname.nickname));
                    }
                    TreeNode femaleNode = new TreeNode("Female", femaleChildren.ToArray());

                    List<TreeNode> classChildren = new List<TreeNode>();
                    classChildren.Add(unisexNode);
                    classChildren.Add(maleNode);
                    classChildren.Add(femaleNode);

                    TreeNode classNode = new TreeNode(className, classChildren.ToArray());
                    tvClassNicknames.Nodes.Add(classNode);
                }
                counter++;
            }

            file.Close();
        }

        private List<ClassNickname> importClassNicknames(StreamReader file, string startingLine)
        {
            List<ClassNickname> classNicknames = new List<ClassNickname>();
            
            string nextLine = file.ReadLine();
            while (!(nextLine.Equals("") && classNicknames.Count > 0))
            {
                if (nextLine.StartsWith("RandomNickNames[", StringComparison.OrdinalIgnoreCase))
                {
                    ClassNickname nickname = new ClassNickname();
                    int startIndex = nextLine.IndexOf('"') + 1;
                    int endIndex = nextLine.LastIndexOf('"');
                    nickname.nickname = nextLine.Substring(startIndex, endIndex - startIndex);
                    nickname.gender = NicknameGender.Unisex;
                    classNicknames.Add(nickname);
                }
                else if (nextLine.StartsWith("RandomNickNames_Male[", StringComparison.OrdinalIgnoreCase))
                {
                    ClassNickname nickname = new ClassNickname();
                    int startIndex = nextLine.IndexOf('"') + 1;
                    int endIndex = nextLine.LastIndexOf('"');
                    nickname.nickname = nextLine.Substring(startIndex, endIndex - startIndex);
                    nickname.gender = NicknameGender.Male;
                    classNicknames.Add(nickname);
                }
                else if (nextLine.StartsWith("RandomNicknames_Female[", StringComparison.OrdinalIgnoreCase))
                {
                    ClassNickname nickname = new ClassNickname();
                    int startIndex = nextLine.IndexOf('"') + 1;
                    int endIndex = nextLine.LastIndexOf('"');
                    nickname.nickname = nextLine.Substring(startIndex, endIndex - startIndex);
                    nickname.gender = NicknameGender.Female;
                    classNicknames.Add(nickname);
                }

                nextLine = file.ReadLine();

                if(nextLine == null)
                {
                    break;
                }
            }

            return classNicknames;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            SoldierClass soldierClass = (SoldierClass)cSoldierClass.SelectedItem;
            soldierClass.nicknames.AddRange(getSelectedNicknames());

            ProjectState.updateClassPackSoldierClass(soldierClass.getInternalName(), soldierClass);

            Close();
        }

        private List<ClassNickname> getSelectedNicknames()
        {
            List<ClassNickname> nicknames = new List<ClassNickname>();

            foreach(TreeNode classNode in tvClassNicknames.Nodes)
            {
                foreach(TreeNode genderNode in classNode.Nodes)
                {
                    foreach(TreeNode nicknameNode in genderNode.Nodes)
                    {
                        if(nicknameNode.Checked)
                        {
                            ClassNickname nickname = new ClassNickname();
                            nickname.nickname = nicknameNode.Text;

                            if (genderNode.Text.Equals("Unisex"))
                            {
                                nickname.gender = NicknameGender.Unisex;
                            }
                            else if (genderNode.Text.Equals("Male"))
                            {
                                nickname.gender = NicknameGender.Male;
                            }
                            else if(genderNode.Text.Equals("Female"))
                            {
                                nickname.gender = NicknameGender.Female;
                            }

                            nicknames.Add(nickname);
                        }
                    }
                }
            }

            return nicknames;
        }
    }
}
