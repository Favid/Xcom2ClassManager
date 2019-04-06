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
        private bool imported;

        public ImportNicknamesForm(ClassPack classPack)
        {
            imported = false;
            this.classPack = classPack;
            InitializeComponent();
            cSoldierClass.DataSource = classPack.soldierClasses;
            cSoldierClass.SelectedIndex = 0;

            updateControls();
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
            updateControls();
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

            List<string> lines = File.ReadAllLines(tFile.Text).ToList();
            List<int> classIndices = lines.Select((x, index) => new { Line = x, Index = index })
                .Where(x => x.Line.Contains(" X2SoldierClassTemplate]"))
                .Select(x => x.Index)
                .ToList();

            // loop through classes
            for(int i = 0; i < classIndices.Count; i++)
            {
                int startIndex = classIndices[i];
                int stopIndex = 0;
                if (i == (classIndices.Count - 1))
                {
                    stopIndex = lines.Count;
                }
                else
                {
                    stopIndex = classIndices[i + 1];
                }
                
                string className = lines[startIndex].Substring(1, lines[startIndex].IndexOf(' ') - 1);

                int workingIndex = startIndex;
                List<ClassNickname> nicknames = new List<ClassNickname>();
                while (workingIndex < stopIndex)
                {
                    ClassNickname nickname = getNicknameFromLine(lines[workingIndex]);
                    if(nickname != null)
                    {
                        nicknames.Add(nickname);
                    }

                    workingIndex++;
                }

                populateTree(nicknames, className);
            }

            imported = true;
            updateControls();
        }

        private ClassNickname getNicknameFromLine(string line)
        {
            if(!line.StartsWith("RandomNickNames", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            ClassNickname nickname = new ClassNickname();

            int startIndex = line.IndexOf('"') + 1;
            int endIndex = line.LastIndexOf('"');

            if (line.StartsWith("RandomNickNames[", StringComparison.OrdinalIgnoreCase))
            {
                nickname.nickname = line.Substring(startIndex, endIndex - startIndex);
                nickname.gender = NicknameGender.Unisex;
            }
            else if (line.StartsWith("RandomNickNames_Male[", StringComparison.OrdinalIgnoreCase))
            {
                nickname.nickname = line.Substring(startIndex, endIndex - startIndex);
                nickname.gender = NicknameGender.Male;
            }
            else if (line.StartsWith("RandomNicknames_Female[", StringComparison.OrdinalIgnoreCase))
            {
                nickname.nickname = line.Substring(startIndex, endIndex - startIndex);
                nickname.gender = NicknameGender.Female;
            }

            return nickname;
        }

        private void populateTree(List<ClassNickname> nicknames, string className)
        {
            List<TreeNode> unisexChildren = new List<TreeNode>();
            List<ClassNickname> unisexNicknames = nicknames.Where(x => x.gender == NicknameGender.Unisex).ToList();
            foreach (ClassNickname unisexNickname in unisexNicknames)
            {
                unisexChildren.Add(new TreeNode(unisexNickname.nickname));
            }
            TreeNode unisexNode = new TreeNode("Unisex", unisexChildren.ToArray());

            List<TreeNode> maleChildren = new List<TreeNode>();
            List<ClassNickname> maleNicknames = nicknames.Where(x => x.gender == NicknameGender.Male).ToList();
            foreach (ClassNickname maleNickname in maleNicknames)
            {
                maleChildren.Add(new TreeNode(maleNickname.nickname));
            }
            TreeNode maleNode = new TreeNode("Male", maleChildren.ToArray());

            List<TreeNode> femaleChildren = new List<TreeNode>();
            List<ClassNickname> femaleNicknames = nicknames.Where(x => x.gender == NicknameGender.Female).ToList();
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

        private void tvClassNicknames_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Checked)
            {
                foreach (TreeNode childNode in e.Node.Nodes)
                {
                    childNode.Checked = true;
                }
            }
            else
            {
                if(allNodesChecked(e.Node.Nodes))
                {
                    foreach (TreeNode childNode in e.Node.Nodes)
                    {
                        childNode.Checked = false;
                    }
                }
            }

            updateControls();
        }

        private bool allNodesChecked(TreeNodeCollection treeNodes)
        {
            foreach (TreeNode treeNode in treeNodes)
            {
                if(!treeNode.Checked)
                {
                    return false;
                }
            }

            return true;
        }

        private void updateControls()
        {
            bool fileSelected = (tFile.Text != string.Empty);

            bImport.Enabled = !imported && fileSelected;
            tvClassNicknames.Enabled = imported;
            cSoldierClass.Enabled = imported;
            bSave.Enabled = AnyChecked();
        }

        private bool AnyChecked()
        {
            foreach (TreeNode classNode in tvClassNicknames.Nodes)
            {
                foreach (TreeNode genderNode in classNode.Nodes)
                {
                    foreach (TreeNode nicknameNode in genderNode.Nodes)
                    {
                        if (nicknameNode.Checked)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }


    }
}
