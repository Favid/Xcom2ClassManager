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
using Xcom2ClassManager.FileManagers;

namespace Xcom2ClassManager.Forms
{
    public partial class OverviewForm : Form
    {
        private int previousSelectedSoldierClassIndex;

        private const int NICKNAME_COLUMN_NAME = 0;
        private const int NICKNAME_COLUMN_GENDER = 1;
        private bool nicknameSortAscending = true;

        public OverviewForm()
        {
            InitializeComponent();
            previousSelectedSoldierClassIndex = -1;
            initTooltips();
        }

        private void initTooltips()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(laDisplayName, "The name displayed everywhere.");
            toolTip.SetToolTip(gbExperience, "Experience stuff.");
        }

        private void OverviewForm_Load(object sender, EventArgs e)
        {
            initAbilitiesDataSources();
            chDragAndDrop.Checked = false;
            cNicknameGender.SelectedIndex = 0;
            cWeaponSlot.SelectedIndex = 0;

            if (isDefaultClassPackPathValid())
            {
                FileStream myStream = new FileStream(Properties.Settings.Default.ClassPackFilePath, FileMode.Open);
                ClassPack classPack = ClassPackManager.loadClassPack(myStream);
                Properties.Settings.Default.ClassPackFilePath = classPack.filePath;

                loadClassPack(classPack);
            }
            else
            {
                unloadClassPack();
            }
        }

        private bool isDefaultClassPackPathValid()
        {
            return !string.IsNullOrEmpty(Properties.Settings.Default.ClassPackFilePath)
                && File.Exists(Properties.Settings.Default.ClassPackFilePath);
        }

        #region Initialize Controls
        
        private void defaultEnableControls()
        {
            setControlEnable(this, true);

            bAddNickname.Enabled = false;
            bRemoveNickname.Enabled = false;
            
            bAddLoadout.Enabled = false;
            bRemoveLoadout.Enabled = false;

            bAddWeapon.Enabled = false;
            bDeleteWeapon.Enabled = false;

            closeToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            nicknamesToolStripMenuItem.Enabled = true;
            exportToolStripMenuItem.Enabled = true;

            addToolStripMenuItem.Enabled = true;
            copyToolStripMenuItem.Enabled = true;
            renameToolStripMenuItem.Enabled = true;
            deleteToolStripMenuItem.Enabled = true;
        }

        private void disableAllControls()
        {
            setControlEnable(this, false);

            closeToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            nicknamesToolStripMenuItem.Enabled = false;
            exportToolStripMenuItem.Enabled = false;

            addToolStripMenuItem.Enabled = false;
            copyToolStripMenuItem.Enabled = false;
            renameToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;
        }

        private void setControlEnable(Control control, bool enable)
        {
            foreach (Control childControl in control.Controls)
            {
                setControlEnable(childControl, enable);
            }

            if(control is TextBox
                || control is NumericUpDown
                || control is ComboBox
                || control is CheckBox
                || control is ListView
                || control is Button)
            {
                control.Enabled = enable;
            }
        }

        private void clearAllControls()
        {
            clearControl(this);

            laHelp.Text = "";
        }

        private void clearControl(Control control)
        {
            foreach(Control childControl in control.Controls)
            {
                clearControl(childControl);
            }

            if (control is TextBox)
            {
                control.Text = "";
            }
            else if (control is NumericUpDown)
            {
                ((NumericUpDown)control).Value = 0;
            }
            else if (control is ComboBox)
            {
                ((ComboBox)control).SelectedIndex = -1;
            }
            else if (control is CheckBox)
            {
                ((CheckBox)control).Checked = false;
            }
            else if (control is ListView)
            {
                ((ListView)control).Items.Clear();
            }
        }

        #endregion Initialize Controls

        private void initAbilitiesDataSources()
        {
            initAbilityDataSource(cSquaddie1);
            initAbilityDataSource(cSquaddie2);
            initAbilityDataSource(cSquaddie3);
            initAbilityDataSource(cSquaddie4);
            initAbilityDataSource(cSquaddie5);
            initAbilityDataSource(cSquaddie6);

            initAbilityDataSource(cCorporal1);
            initAbilityDataSource(cCorporal2);
            initAbilityDataSource(cCorporal3);

            initAbilityDataSource(cSergeant1);
            initAbilityDataSource(cSergeant2);
            initAbilityDataSource(cSergeant3);

            initAbilityDataSource(cLieutenant1);
            initAbilityDataSource(cLieutenant2);
            initAbilityDataSource(cLieutenant3);

            initAbilityDataSource(cCaptain1);
            initAbilityDataSource(cCaptain2);
            initAbilityDataSource(cCaptain3);

            initAbilityDataSource(cMajor1);
            initAbilityDataSource(cMajor2);
            initAbilityDataSource(cMajor3);

            initAbilityDataSource(cColonel1);
            initAbilityDataSource(cColonel2);
            initAbilityDataSource(cColonel3);

            initAbilityDataSource(cBrigadier1);
            initAbilityDataSource(cBrigadier2);
            initAbilityDataSource(cBrigadier3);
        }

        private void initAbilityDataSource(ComboBox combo)
        {
            List<Ability> abilities = new List<Ability>(ProjectState.getAbilities());
            abilities.Add(new Ability());
            abilities = abilities.OrderBy(x => x.internalName).ToList();

            combo.DataSource = abilities;
        }

        #region Populate Form With Class

        private void open(SoldierClass soldierClass)
        {
            ProjectState.setOpenSoldierClass(soldierClass);

            tDisplayName.Text = soldierClass.metadata.displayName;
            tDescription.Text = soldierClass.metadata.description;
            tIconString.Text = soldierClass.metadata.iconString;
            
            nNumInForcedDeck.Value = soldierClass.experience.numberInForcedDeck;
            nNumInDeck.Value = soldierClass.experience.numberInDeck;
            nKillAssistsPerKill.Value = soldierClass.experience.killAssistsPerKill;
            nMissionExperienceWeight.Value = (decimal)soldierClass.experience.missionExperienceWeight;

            tSquaddieLoadout.Text = soldierClass.equipment.squaddieLoadout;
            tAllowedArmor.Text = soldierClass.equipment.allowedArmors;
            
            tLeftTree.Text = soldierClass.leftTreeName;
            tRightTree.Text = soldierClass.rightTreeName;

            openSoldierStats(soldierClass);
            openSoldierAbilities(soldierClass);
            openSoldierNicknames(soldierClass);
            openSoldierLoadout(soldierClass);
            openSoldierWeapons(soldierClass);
        }

        private void openSoldierStats(SoldierClass soldierClass)
        {
            tSquaddieHp.Text = soldierClass.getStatValueText(SoldierRank.Squaddie, Stat.HP);
            tCorporalHp.Text = soldierClass.getStatValueText(SoldierRank.Corporal, Stat.HP);
            tSergeantHp.Text = soldierClass.getStatValueText(SoldierRank.Sergeant, Stat.HP);
            tLieutenantHp.Text = soldierClass.getStatValueText(SoldierRank.Lieutenant, Stat.HP);
            tCaptainHp.Text = soldierClass.getStatValueText(SoldierRank.Captain, Stat.HP);
            tMajorHp.Text = soldierClass.getStatValueText(SoldierRank.Major, Stat.HP);
            tColonelHp.Text = soldierClass.getStatValueText(SoldierRank.Colonel, Stat.HP);
            tBrigadierHp.Text = soldierClass.getStatValueText(SoldierRank.Brigadier, Stat.HP);

            tSquaddieAim.Text = soldierClass.getStatValueText(SoldierRank.Squaddie, Stat.Aim);
            tCorporalAim.Text = soldierClass.getStatValueText(SoldierRank.Corporal, Stat.Aim);
            tSergeantAim.Text = soldierClass.getStatValueText(SoldierRank.Sergeant, Stat.Aim);
            tLieutenantAim.Text = soldierClass.getStatValueText(SoldierRank.Lieutenant, Stat.Aim);
            tCaptainAim.Text = soldierClass.getStatValueText(SoldierRank.Captain, Stat.Aim);
            tMajorAim.Text = soldierClass.getStatValueText(SoldierRank.Major, Stat.Aim);
            tColonelAim.Text = soldierClass.getStatValueText(SoldierRank.Colonel, Stat.Aim);
            tBrigadierAim.Text = soldierClass.getStatValueText(SoldierRank.Brigadier, Stat.Aim);

            tSquaddieStrength.Text = soldierClass.getStatValueText(SoldierRank.Squaddie, Stat.Strength);
            tCorporalStrength.Text = soldierClass.getStatValueText(SoldierRank.Corporal, Stat.Strength);
            tSergeantStrength.Text = soldierClass.getStatValueText(SoldierRank.Sergeant, Stat.Strength);
            tLieutenantStrength.Text = soldierClass.getStatValueText(SoldierRank.Lieutenant, Stat.Strength);
            tCaptainStrength.Text = soldierClass.getStatValueText(SoldierRank.Captain, Stat.Strength);
            tMajorStrength.Text = soldierClass.getStatValueText(SoldierRank.Major, Stat.Strength);
            tColonelStrength.Text = soldierClass.getStatValueText(SoldierRank.Colonel, Stat.Strength);
            tBrigadierStrength.Text = soldierClass.getStatValueText(SoldierRank.Brigadier, Stat.Strength);

            tSquaddieHacking.Text = soldierClass.getStatValueText(SoldierRank.Squaddie, Stat.Hacking);
            tCorporalHacking.Text = soldierClass.getStatValueText(SoldierRank.Corporal, Stat.Hacking);
            tSergeantHacking.Text = soldierClass.getStatValueText(SoldierRank.Sergeant, Stat.Hacking);
            tLieutenantHacking.Text = soldierClass.getStatValueText(SoldierRank.Lieutenant, Stat.Hacking);
            tCaptainHacking.Text = soldierClass.getStatValueText(SoldierRank.Captain, Stat.Hacking);
            tMajorHacking.Text = soldierClass.getStatValueText(SoldierRank.Major, Stat.Hacking);
            tColonelHacking.Text = soldierClass.getStatValueText(SoldierRank.Colonel, Stat.Hacking);
            tBrigadierHacking.Text = soldierClass.getStatValueText(SoldierRank.Brigadier, Stat.Hacking);

            tSquaddiePsi.Text = soldierClass.getStatValueText(SoldierRank.Squaddie, Stat.Psi);
            tCorporalPsi.Text = soldierClass.getStatValueText(SoldierRank.Corporal, Stat.Psi);
            tSergeantPsi.Text = soldierClass.getStatValueText(SoldierRank.Sergeant, Stat.Psi);
            tLieutenantPsi.Text = soldierClass.getStatValueText(SoldierRank.Lieutenant, Stat.Psi);
            tCaptainPsi.Text = soldierClass.getStatValueText(SoldierRank.Captain, Stat.Psi);
            tMajorPsi.Text = soldierClass.getStatValueText(SoldierRank.Major, Stat.Psi);
            tColonelPsi.Text = soldierClass.getStatValueText(SoldierRank.Colonel, Stat.Psi);
            tBrigadierPsi.Text = soldierClass.getStatValueText(SoldierRank.Brigadier, Stat.Psi);

            tSquaddieMobility.Text = soldierClass.getStatValueText(SoldierRank.Squaddie, Stat.Mobility);
            tCorporalMobility.Text = soldierClass.getStatValueText(SoldierRank.Corporal, Stat.Mobility);
            tSergeantMobility.Text = soldierClass.getStatValueText(SoldierRank.Sergeant, Stat.Mobility);
            tLieutenantMobility.Text = soldierClass.getStatValueText(SoldierRank.Lieutenant, Stat.Mobility);
            tCaptainMobility.Text = soldierClass.getStatValueText(SoldierRank.Captain, Stat.Mobility);
            tMajorMobility.Text = soldierClass.getStatValueText(SoldierRank.Major, Stat.Mobility);
            tColonelMobility.Text = soldierClass.getStatValueText(SoldierRank.Colonel, Stat.Mobility);
            tBrigadierMobility.Text = soldierClass.getStatValueText(SoldierRank.Brigadier, Stat.Mobility);

            tSquaddieWill.Text = soldierClass.getStatValueText(SoldierRank.Squaddie, Stat.Will);
            tCorporalWill.Text = soldierClass.getStatValueText(SoldierRank.Corporal, Stat.Will);
            tSergeantWill.Text = soldierClass.getStatValueText(SoldierRank.Sergeant, Stat.Will);
            tLieutenantWill.Text = soldierClass.getStatValueText(SoldierRank.Lieutenant, Stat.Will);
            tCaptainWill.Text = soldierClass.getStatValueText(SoldierRank.Captain, Stat.Will);
            tMajorWill.Text = soldierClass.getStatValueText(SoldierRank.Major, Stat.Will);
            tColonelWill.Text = soldierClass.getStatValueText(SoldierRank.Colonel, Stat.Will);
            tBrigadierWill.Text = soldierClass.getStatValueText(SoldierRank.Brigadier, Stat.Will);

            tSquaddieDodge.Text = soldierClass.getStatValueText(SoldierRank.Squaddie, Stat.Dodge);
            tCorporalDodge.Text = soldierClass.getStatValueText(SoldierRank.Corporal, Stat.Dodge);
            tSergeantDodge.Text = soldierClass.getStatValueText(SoldierRank.Sergeant, Stat.Dodge);
            tLieutenantDodge.Text = soldierClass.getStatValueText(SoldierRank.Lieutenant, Stat.Dodge);
            tCaptainDodge.Text = soldierClass.getStatValueText(SoldierRank.Captain, Stat.Dodge);
            tMajorDodge.Text = soldierClass.getStatValueText(SoldierRank.Major, Stat.Dodge);
            tColonelDodge.Text = soldierClass.getStatValueText(SoldierRank.Colonel, Stat.Dodge);
            tBrigadierDodge.Text = soldierClass.getStatValueText(SoldierRank.Brigadier, Stat.Dodge);

            updateStatTotals();
        }

        private void openSoldierAbilities(SoldierClass soldierClass)
        {
            setCombo(soldierClass, cSquaddie1, SoldierRank.Squaddie, 1);
            setCombo(soldierClass, cSquaddie2, SoldierRank.Squaddie, 2);
            setCombo(soldierClass, cSquaddie3, SoldierRank.Squaddie, 3);
            setCombo(soldierClass, cSquaddie4, SoldierRank.Squaddie, 4);
            setCombo(soldierClass, cSquaddie5, SoldierRank.Squaddie, 5);
            setCombo(soldierClass, cSquaddie6, SoldierRank.Squaddie, 6);

            setCombo(soldierClass, cCorporal1, SoldierRank.Corporal, 1);
            setCombo(soldierClass, cCorporal2, SoldierRank.Corporal, 2);
            setCombo(soldierClass, cCorporal3, SoldierRank.Corporal, 3);

            setCombo(soldierClass, cSergeant1, SoldierRank.Sergeant, 1);
            setCombo(soldierClass, cSergeant2, SoldierRank.Sergeant, 2);
            setCombo(soldierClass, cSergeant3, SoldierRank.Sergeant, 3);

            setCombo(soldierClass, cLieutenant1, SoldierRank.Lieutenant, 1);
            setCombo(soldierClass, cLieutenant2, SoldierRank.Lieutenant, 2);
            setCombo(soldierClass, cLieutenant3, SoldierRank.Lieutenant, 3);

            setCombo(soldierClass, cCaptain1, SoldierRank.Captain, 1);
            setCombo(soldierClass, cCaptain2, SoldierRank.Captain, 2);
            setCombo(soldierClass, cCaptain3, SoldierRank.Captain, 3);

            setCombo(soldierClass, cMajor1, SoldierRank.Major, 1);
            setCombo(soldierClass, cMajor2, SoldierRank.Major, 2);
            setCombo(soldierClass, cMajor3, SoldierRank.Major, 3);

            setCombo(soldierClass, cColonel1, SoldierRank.Colonel, 1);
            setCombo(soldierClass, cColonel2, SoldierRank.Colonel, 2);
            setCombo(soldierClass, cColonel3, SoldierRank.Colonel, 3);

            setCombo(soldierClass, cBrigadier1, SoldierRank.Brigadier, 1);
            setCombo(soldierClass, cBrigadier2, SoldierRank.Brigadier, 2);
            setCombo(soldierClass, cBrigadier3, SoldierRank.Brigadier, 3);
        }

        private void setCombo(SoldierClass soldierClass, ComboBox combo, SoldierRank rank, int slot)
        {
            combo.SelectedIndex = combo.Items.IndexOf(getAbilityForCombo(soldierClass, rank, slot));
        }

        private Ability getAbilityForCombo(SoldierClass soldierClass, SoldierRank rank, int slot)
        {
            SoldierClassAbility soldierAbility = soldierClass.getSoldierAbility(rank, slot);

            if (soldierAbility != null)
            {
                return soldierAbility.getAbility();
            }

            return new Ability();
        }

        private void openSoldierNicknames(SoldierClass soldierClass)
        {
            lvUnisexNicknames.Items.Clear();
            foreach (ClassNickname nickname in soldierClass.nicknames)
            {
                ListViewItem item = new ListViewItem(nickname.getListViewStringArray());
                lvUnisexNicknames.Items.Add(item);
            }
        }

        private void openSoldierLoadout(SoldierClass soldierClass)
        {
            lvSquaddieLoadout.Items.Clear();
            foreach (string loadoutItem in soldierClass.loadoutItems)
            {
                lvSquaddieLoadout.Items.Add(loadoutItem);
            }
        }

        private void openSoldierWeapons(SoldierClass soldierClass)
        {
            lvWeapons.Items.Clear();
            foreach (Weapon weapon in soldierClass.equipment.weapons)
            {
                ListViewItem item = new ListViewItem(weapon.getListViewStringArray());
                lvWeapons.Items.Add(item);
            }
        }

        #endregion Open Class

        #region Build Class From Form

        private SoldierClass buildSoldierClass()
        {
            SoldierClass soldierClass = new SoldierClass();

            soldierClass.metadata.internalName = ProjectState.getOpenSoldierClass().metadata.internalName;
            soldierClass.metadata.displayName = tDisplayName.Text;
            soldierClass.metadata.description = tDescription.Text;
            soldierClass.metadata.iconString = tIconString.Text;
            
            soldierClass.experience.numberInForcedDeck = (int)nNumInForcedDeck.Value;
            soldierClass.experience.numberInDeck = (int)nNumInDeck.Value;
            soldierClass.experience.killAssistsPerKill = (int)nKillAssistsPerKill.Value;
            soldierClass.experience.missionExperienceWeight = (double)nMissionExperienceWeight.Value;

            soldierClass.equipment.squaddieLoadout = tSquaddieLoadout.Text;
            soldierClass.equipment.allowedArmors = tAllowedArmor.Text;
            soldierClass.equipment.weapons = buildSoldierClassWeapons();

            soldierClass.stats = buildSoldierClassStats();
            soldierClass.soldierAbilities = buildSoldierClassAbilities();

            soldierClass.leftTreeName = tLeftTree.Text;
            soldierClass.rightTreeName = tRightTree.Text;

            soldierClass.nicknames = buildSoldierClassNicknames();
            soldierClass.loadoutItems = buildSoldierClassLoadout();

            return soldierClass;
        }

        private List<SoldierClassStat> buildSoldierClassStats()
        {
            List<SoldierClassStat> soldierStats = new List<SoldierClassStat>();

            soldierStats.Add(buildSoldierStatFromControl(tSquaddieHp, SoldierRank.Squaddie, Stat.HP));
            soldierStats.Add(buildSoldierStatFromControl(tCorporalHp, SoldierRank.Corporal, Stat.HP));
            soldierStats.Add(buildSoldierStatFromControl(tSergeantHp, SoldierRank.Sergeant, Stat.HP));
            soldierStats.Add(buildSoldierStatFromControl(tLieutenantHp, SoldierRank.Lieutenant, Stat.HP));
            soldierStats.Add(buildSoldierStatFromControl(tCaptainHp, SoldierRank.Captain, Stat.HP));
            soldierStats.Add(buildSoldierStatFromControl(tMajorHp, SoldierRank.Major, Stat.HP));
            soldierStats.Add(buildSoldierStatFromControl(tColonelHp, SoldierRank.Colonel, Stat.HP));
            soldierStats.Add(buildSoldierStatFromControl(tBrigadierHp, SoldierRank.Brigadier, Stat.HP));

            soldierStats.Add(buildSoldierStatFromControl(tSquaddieAim, SoldierRank.Squaddie, Stat.Aim));
            soldierStats.Add(buildSoldierStatFromControl(tCorporalAim, SoldierRank.Corporal, Stat.Aim));
            soldierStats.Add(buildSoldierStatFromControl(tSergeantAim, SoldierRank.Sergeant, Stat.Aim));
            soldierStats.Add(buildSoldierStatFromControl(tLieutenantAim, SoldierRank.Lieutenant, Stat.Aim));
            soldierStats.Add(buildSoldierStatFromControl(tCaptainAim, SoldierRank.Captain, Stat.Aim));
            soldierStats.Add(buildSoldierStatFromControl(tMajorAim, SoldierRank.Major, Stat.Aim));
            soldierStats.Add(buildSoldierStatFromControl(tColonelAim, SoldierRank.Colonel, Stat.Aim));
            soldierStats.Add(buildSoldierStatFromControl(tBrigadierAim, SoldierRank.Brigadier, Stat.Aim));

            soldierStats.Add(buildSoldierStatFromControl(tSquaddieStrength, SoldierRank.Squaddie, Stat.Strength));
            soldierStats.Add(buildSoldierStatFromControl(tCorporalStrength, SoldierRank.Corporal, Stat.Strength));
            soldierStats.Add(buildSoldierStatFromControl(tSergeantStrength, SoldierRank.Sergeant, Stat.Strength));
            soldierStats.Add(buildSoldierStatFromControl(tLieutenantStrength, SoldierRank.Lieutenant, Stat.Strength));
            soldierStats.Add(buildSoldierStatFromControl(tCaptainStrength, SoldierRank.Captain, Stat.Strength));
            soldierStats.Add(buildSoldierStatFromControl(tMajorStrength, SoldierRank.Major, Stat.Strength));
            soldierStats.Add(buildSoldierStatFromControl(tColonelStrength, SoldierRank.Colonel, Stat.Strength));
            soldierStats.Add(buildSoldierStatFromControl(tBrigadierStrength, SoldierRank.Brigadier, Stat.Strength));

            soldierStats.Add(buildSoldierStatFromControl(tSquaddieHacking, SoldierRank.Squaddie, Stat.Hacking));
            soldierStats.Add(buildSoldierStatFromControl(tCorporalHacking, SoldierRank.Corporal, Stat.Hacking));
            soldierStats.Add(buildSoldierStatFromControl(tSergeantHacking, SoldierRank.Sergeant, Stat.Hacking));
            soldierStats.Add(buildSoldierStatFromControl(tLieutenantHacking, SoldierRank.Lieutenant, Stat.Hacking));
            soldierStats.Add(buildSoldierStatFromControl(tCaptainHacking, SoldierRank.Captain, Stat.Hacking));
            soldierStats.Add(buildSoldierStatFromControl(tMajorHacking, SoldierRank.Major, Stat.Hacking));
            soldierStats.Add(buildSoldierStatFromControl(tColonelHacking, SoldierRank.Colonel, Stat.Hacking));
            soldierStats.Add(buildSoldierStatFromControl(tBrigadierHacking, SoldierRank.Brigadier, Stat.Hacking));

            soldierStats.Add(buildSoldierStatFromControl(tSquaddiePsi, SoldierRank.Squaddie, Stat.Psi));
            soldierStats.Add(buildSoldierStatFromControl(tCorporalPsi, SoldierRank.Corporal, Stat.Psi));
            soldierStats.Add(buildSoldierStatFromControl(tSergeantPsi, SoldierRank.Sergeant, Stat.Psi));
            soldierStats.Add(buildSoldierStatFromControl(tLieutenantPsi, SoldierRank.Lieutenant, Stat.Psi));
            soldierStats.Add(buildSoldierStatFromControl(tCaptainPsi, SoldierRank.Captain, Stat.Psi));
            soldierStats.Add(buildSoldierStatFromControl(tMajorPsi, SoldierRank.Major, Stat.Psi));
            soldierStats.Add(buildSoldierStatFromControl(tColonelPsi, SoldierRank.Colonel, Stat.Psi));
            soldierStats.Add(buildSoldierStatFromControl(tBrigadierPsi, SoldierRank.Brigadier, Stat.Psi));

            soldierStats.Add(buildSoldierStatFromControl(tSquaddieMobility, SoldierRank.Squaddie, Stat.Mobility));
            soldierStats.Add(buildSoldierStatFromControl(tCorporalMobility, SoldierRank.Corporal, Stat.Mobility));
            soldierStats.Add(buildSoldierStatFromControl(tSergeantMobility, SoldierRank.Sergeant, Stat.Mobility));
            soldierStats.Add(buildSoldierStatFromControl(tLieutenantMobility, SoldierRank.Lieutenant, Stat.Mobility));
            soldierStats.Add(buildSoldierStatFromControl(tCaptainMobility, SoldierRank.Captain, Stat.Mobility));
            soldierStats.Add(buildSoldierStatFromControl(tMajorMobility, SoldierRank.Major, Stat.Mobility));
            soldierStats.Add(buildSoldierStatFromControl(tColonelMobility, SoldierRank.Colonel, Stat.Mobility));
            soldierStats.Add(buildSoldierStatFromControl(tBrigadierMobility, SoldierRank.Brigadier, Stat.Mobility));

            soldierStats.Add(buildSoldierStatFromControl(tSquaddieWill, SoldierRank.Squaddie, Stat.Will));
            soldierStats.Add(buildSoldierStatFromControl(tCorporalWill, SoldierRank.Corporal, Stat.Will));
            soldierStats.Add(buildSoldierStatFromControl(tSergeantWill, SoldierRank.Sergeant, Stat.Will));
            soldierStats.Add(buildSoldierStatFromControl(tLieutenantWill, SoldierRank.Lieutenant, Stat.Will));
            soldierStats.Add(buildSoldierStatFromControl(tCaptainWill, SoldierRank.Captain, Stat.Will));
            soldierStats.Add(buildSoldierStatFromControl(tMajorWill, SoldierRank.Major, Stat.Will));
            soldierStats.Add(buildSoldierStatFromControl(tColonelWill, SoldierRank.Colonel, Stat.Will));
            soldierStats.Add(buildSoldierStatFromControl(tBrigadierWill, SoldierRank.Brigadier, Stat.Will));

            soldierStats.Add(buildSoldierStatFromControl(tSquaddieDodge, SoldierRank.Squaddie, Stat.Dodge));
            soldierStats.Add(buildSoldierStatFromControl(tCorporalDodge, SoldierRank.Corporal, Stat.Dodge));
            soldierStats.Add(buildSoldierStatFromControl(tSergeantDodge, SoldierRank.Sergeant, Stat.Dodge));
            soldierStats.Add(buildSoldierStatFromControl(tLieutenantDodge, SoldierRank.Lieutenant, Stat.Dodge));
            soldierStats.Add(buildSoldierStatFromControl(tCaptainDodge, SoldierRank.Captain, Stat.Dodge));
            soldierStats.Add(buildSoldierStatFromControl(tMajorDodge, SoldierRank.Major, Stat.Dodge));
            soldierStats.Add(buildSoldierStatFromControl(tColonelDodge, SoldierRank.Colonel, Stat.Dodge));
            soldierStats.Add(buildSoldierStatFromControl(tBrigadierDodge, SoldierRank.Brigadier, Stat.Dodge));

            return soldierStats;
        }

        private SoldierClassStat buildSoldierStatFromControl(TextBox control, SoldierRank rank, Stat stat)
        {
            SoldierClassStat soldierStat = new SoldierClassStat();

            soldierStat.rank = rank;
            soldierStat.stat = stat;
            soldierStat.value = Utils.parseStringToInt(control.Text);

            return soldierStat;
        }

        private List<SoldierClassAbility> buildSoldierClassAbilities()
        {
            List<SoldierClassAbility> soldierAbilities = new List<SoldierClassAbility>();

            soldierAbilities.Add(buildSoldierAbilityFromCombo(cSquaddie1, SoldierRank.Squaddie, 1));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cSquaddie2, SoldierRank.Squaddie, 2));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cSquaddie3, SoldierRank.Squaddie, 3));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cSquaddie4, SoldierRank.Squaddie, 4));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cSquaddie5, SoldierRank.Squaddie, 5));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cSquaddie6, SoldierRank.Squaddie, 6));

            soldierAbilities.Add(buildSoldierAbilityFromCombo(cCorporal1, SoldierRank.Corporal, 1));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cCorporal2, SoldierRank.Corporal, 2));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cCorporal3, SoldierRank.Corporal, 3));

            soldierAbilities.Add(buildSoldierAbilityFromCombo(cSergeant1, SoldierRank.Sergeant, 1));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cSergeant2, SoldierRank.Sergeant, 2));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cSergeant3, SoldierRank.Sergeant, 3));

            soldierAbilities.Add(buildSoldierAbilityFromCombo(cLieutenant1, SoldierRank.Lieutenant, 1));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cLieutenant2, SoldierRank.Lieutenant, 2));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cLieutenant3, SoldierRank.Lieutenant, 3));

            soldierAbilities.Add(buildSoldierAbilityFromCombo(cCaptain1, SoldierRank.Captain, 1));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cCaptain2, SoldierRank.Captain, 2));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cCaptain3, SoldierRank.Captain, 3));

            soldierAbilities.Add(buildSoldierAbilityFromCombo(cMajor1, SoldierRank.Major, 1));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cMajor2, SoldierRank.Major, 2));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cMajor3, SoldierRank.Major, 3));

            soldierAbilities.Add(buildSoldierAbilityFromCombo(cColonel1, SoldierRank.Colonel, 1));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cColonel2, SoldierRank.Colonel, 2));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cColonel3, SoldierRank.Colonel, 3));

            soldierAbilities.Add(buildSoldierAbilityFromCombo(cBrigadier1, SoldierRank.Brigadier, 1));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cBrigadier2, SoldierRank.Brigadier, 2));
            soldierAbilities.Add(buildSoldierAbilityFromCombo(cBrigadier3, SoldierRank.Brigadier, 3));

            return soldierAbilities;
        }

        private SoldierClassAbility buildSoldierAbilityFromCombo(ComboBox combo, SoldierRank rank, int slot)
        {
            SoldierClassAbility soldierAbility = new SoldierClassAbility();
            Ability comboAbility = combo.SelectedItem as Ability;

            if (comboAbility != null)
            {
                soldierAbility.internalName = comboAbility.internalName;
                soldierAbility.displayName = comboAbility.displayName;
                soldierAbility.description = comboAbility.description;
                soldierAbility.weaponSlot = comboAbility.weaponSlot;
                soldierAbility.requiredMod = comboAbility.requiredMod;
                soldierAbility.rank = rank;
                soldierAbility.slot = slot;
            }

            return soldierAbility;
        }

        private List<Weapon> buildSoldierClassWeapons()
        {
            List<Weapon> weapons = new List<Weapon>();
            foreach (ListViewItem item in lvWeapons.Items)
            {
                weapons.Add(new Weapon(item.SubItems[0].Text, item.SubItems[1].Text));
            }

            return weapons;
        }

        private List<ClassNickname> buildSoldierClassNicknames()
        {
            List<ClassNickname> nicknames = new List<ClassNickname>();
            foreach(ListViewItem item in lvUnisexNicknames.Items)
            {
                nicknames.Add(new ClassNickname(item.SubItems[0].Text, item.SubItems[1].Text));
            }

            return nicknames;
        }

        private List<string> buildSoldierClassLoadout()
        {
            List<string> loadoutItems = new List<string>();
            foreach (ListViewItem item in lvSquaddieLoadout.Items)
            {
                loadoutItems.Add(item.Text);
            }

            return loadoutItems;
        }
        
        #endregion Save Class

        #region Rename Class

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameForm rename = new RenameForm(ProjectState.getOpenSoldierClass().metadata.internalName);
            rename.FormClosing += renameClosingListener;
            rename.ShowDialog(this);
        }

        private void renameClosingListener(object sender, FormClosingEventArgs e)
        {
            RenameForm renameForm = sender as RenameForm;
            if (renameForm != null)
            {
                if (!renameForm.getNewName().Equals(ProjectState.getOpenSoldierClass().metadata.internalName))
                {
                    SoldierClass soldierClass = buildSoldierClass();
                    soldierClass.metadata.internalName = renameForm.getNewName();
                    ProjectState.updateClassPackSoldierClass(renameForm.getOldName(), soldierClass);
                }
            }
        }

        #endregion Rename Class

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Delete Class

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            previousSelectedSoldierClassIndex = -1;
            ProjectState.deleteOpenSoldierClass();
        }

        #endregion Delete Class

        #region Modify Weapons
        
        private void bAddWeapon_Click(object sender, EventArgs e)
        {
            if (addWeapon())
            {
                tNewWeapon.Text = "";
            }
        }

        private void tNewWeapon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (addWeapon())
                {
                    tNewWeapon.Text = "";
                    e.Handled = true;
                }
            }
        }

        private bool addWeapon()
        {
            bool success = false;
            string newWeapon = tNewWeapon.Text;

            if (!string.IsNullOrEmpty(newWeapon))
            {
                ListViewItem x = new ListViewItem(new Weapon(newWeapon, (string)cWeaponSlot.SelectedItem).getListViewStringArray());
                lvWeapons.Items.Add(x);
                success = true;
            }

            return success;
        }

        private void bDeleteWeapon_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in lvWeapons.SelectedItems)
            {
                lvWeapons.Items.Remove(eachItem);
            }
        }

        private void lvWeapons_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvWeapons.SelectedIndices.Count > 0)
            {
                bDeleteWeapon.Enabled = true;
            }
            else
            {
                bDeleteWeapon.Enabled = false;
            }
        }

        private void tNewWeapon_TextChanged(object sender, EventArgs e)
        {
            if (tNewWeapon.Text.Length > 0)
            {
                bAddWeapon.Enabled = true;
            }
            else
            {
                bAddWeapon.Enabled = false;
            }
        }

        #endregion Modify Weapons

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassPackExportForm classPackExportForm = new ClassPackExportForm(ProjectState.getClassPack());
            classPackExportForm.ShowDialog();
        }

        #region Ability Events

        private void cAbility_MouseDown(object sender, MouseEventArgs e)
        {
            if (chDragAndDrop.Checked)
            {
                ComboBox comboSender = sender as ComboBox;
                comboSender.DoDragDrop(comboSender, DragDropEffects.Copy);
            }
        }

        private void cAbility_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void cAbility_DragDrop(object sender, DragEventArgs e)
        {
            ComboBox comboSender = sender as ComboBox;
            ComboBox comboDragged = e.Data.GetData(typeof(ComboBox)) as ComboBox;

            Object temp = comboSender.SelectedItem;
            comboSender.SelectedIndex = comboSender.Items.IndexOf(comboDragged.SelectedItem);
            comboDragged.SelectedIndex = comboDragged.Items.IndexOf(temp);
        }

        private void cAbility_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;

            if (combo != null)
            {
                Ability ability = combo.SelectedItem as Ability;

                if (ability != null)
                {
                    updateHelpText(ability.description);
                }
            }
        }

        private void cAbility_Clicked(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;

            if (combo != null)
            {
                Ability ability = combo.SelectedItem as Ability;

                if (ability != null)
                {
                    updateHelpText(ability.description);
                }
            }
        }

        #endregion Ability Events

        private void abilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportAbilitiesForm dialog = new ImportAbilitiesForm(ProjectState.getAbilities());
            dialog.ShowDialog();
        }

        private void updateHelpText(string text)
        {
            laHelp.Text = text;
        }
        
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassPack classPack = new ClassPack();

            // TODO once the overview form has a state for no loaded class, remove this
            SoldierClass soldierClass = new SoldierClass();
            soldierClass.metadata.internalName = "NewClass";
            classPack.soldierClasses.Add(soldierClass);

            ProjectState.setClassPack(classPack);
            loadClassPack(classPack);
        }

        private void loadClassPack(ClassPack classPack)
        {
            ProjectState.setClassPack(classPack);
            cSoldierClass.DataSource = ProjectState.getClassPack().soldierClasses;
            SoldierClass soldierClass = classPack.soldierClasses.First();
            cSoldierClass.SelectedIndex = cSoldierClass.Items.IndexOf(soldierClass);

            defaultEnableControls();
        }

        private void unloadClassPack()
        {
            disableAllControls();
            clearAllControls();
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            ClassPack classPack = ClassPackManager.loadClassPack(myStream);
                            classPack.filePath = openFileDialog1.FileName;
                            Properties.Settings.Default.ClassPackFilePath = classPack.filePath;
                            Properties.Settings.Default.Save();
                            loadClassPack(classPack);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassPack classPack = ProjectState.getClassPack();
            if(!string.IsNullOrEmpty(classPack.filePath))
            {
                FileStream stream = new FileStream(classPack.filePath, FileMode.Open);
                ClassPackManager.saveClassPack(ProjectState.getClassPack(), stream);
                stream.Close();
            }
        }
        
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    ClassPackManager.saveClassPack(ProjectState.getClassPack(), myStream);
                    myStream.Close();
                }
            }

        }

        private void cSoldierClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            
            if (combo != null)
            {
                SoldierClass soldierClass = combo.SelectedItem as SoldierClass;

                if (previousSelectedSoldierClassIndex > -1 && previousSelectedSoldierClassIndex != cSoldierClass.SelectedIndex && soldierClass != null)
                {
                    saveOpenClass();
                }

                combo.SelectedItem = soldierClass;

                if (soldierClass != null)
                {
                    open(soldierClass);
                    previousSelectedSoldierClassIndex = combo.SelectedIndex;
                }
            }
        }

        private void saveOpenClass()
        {
            SoldierClass soldierClass = buildSoldierClass();
            ProjectState.updateClassPackSoldierClass(soldierClass.metadata.internalName, soldierClass);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SoldierClass newClass = ProjectState.addNewClassPackSoldierClass();
            cSoldierClass.SelectedIndex = cSoldierClass.Items.IndexOf(newClass);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SoldierClass newClass = ProjectState.copyOpenSoldierClass();
            cSoldierClass.SelectedIndex = cSoldierClass.Items.IndexOf(newClass);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        #region Stat Totals

        private void updateStatTotals()
        {
            updateTotalHp();
            updateTotalAim();
            updateTotalStrength();
            updateTotalHacking();
            updateTotalPsi();
            updateTotalMobility();
            updateTotalWill();
            updateTotalDodge();
        }

        private void tHp_TextChanged(object sender, EventArgs e)
        {
            updateTotalHp();
        }

        private void updateTotalHp()
        {
            int totalHp = 0;

            totalHp += getStatValueForAddition(tSquaddieHp);
            totalHp += getStatValueForAddition(tCorporalHp);
            totalHp += getStatValueForAddition(tSergeantHp);
            totalHp += getStatValueForAddition(tLieutenantHp);
            totalHp += getStatValueForAddition(tCaptainHp);
            totalHp += getStatValueForAddition(tMajorHp);
            totalHp += getStatValueForAddition(tColonelHp);
            totalHp += getStatValueForAddition(tBrigadierHp);

            laTotalHp.Text = totalHp.ToString();
        }

        private void tAim_TextChanged(object sender, EventArgs e)
        {
            updateTotalAim();
        }

        private void updateTotalAim()
        {
            int totalAim = 0;

            totalAim += getStatValueForAddition(tSquaddieAim);
            totalAim += getStatValueForAddition(tCorporalAim);
            totalAim += getStatValueForAddition(tSergeantAim);
            totalAim += getStatValueForAddition(tLieutenantAim);
            totalAim += getStatValueForAddition(tCaptainAim);
            totalAim += getStatValueForAddition(tMajorAim);
            totalAim += getStatValueForAddition(tColonelAim);
            totalAim += getStatValueForAddition(tBrigadierAim);

            laTotalAim.Text = totalAim.ToString();
        }

        private void tStrength_TextChanged(object sender, EventArgs e)
        {
            updateTotalStrength();
        }

        private void updateTotalStrength()
        {
            int totalStrength = 0;

            totalStrength += getStatValueForAddition(tSquaddieStrength);
            totalStrength += getStatValueForAddition(tCorporalStrength);
            totalStrength += getStatValueForAddition(tSergeantStrength);
            totalStrength += getStatValueForAddition(tLieutenantStrength);
            totalStrength += getStatValueForAddition(tCaptainStrength);
            totalStrength += getStatValueForAddition(tMajorStrength);
            totalStrength += getStatValueForAddition(tColonelStrength);
            totalStrength += getStatValueForAddition(tBrigadierStrength);

            laTotalStrength.Text = totalStrength.ToString();
        }

        private void tHacking_TextChanged(object sender, EventArgs e)
        {
            updateTotalHacking();
        }

        private void updateTotalHacking()
        {
            int totalHacking = 0;

            totalHacking += getStatValueForAddition(tSquaddieHacking);
            totalHacking += getStatValueForAddition(tCorporalHacking);
            totalHacking += getStatValueForAddition(tSergeantHacking);
            totalHacking += getStatValueForAddition(tLieutenantHacking);
            totalHacking += getStatValueForAddition(tCaptainHacking);
            totalHacking += getStatValueForAddition(tMajorHacking);
            totalHacking += getStatValueForAddition(tColonelHacking);
            totalHacking += getStatValueForAddition(tBrigadierHacking);

            laTotalHacking.Text = totalHacking.ToString();
        }

        private void tPsi_TextChanged(object sender, EventArgs e)
        {
            updateTotalPsi();
        }

        private void updateTotalPsi()
        {
            int totalPsi = 0;

            totalPsi += getStatValueForAddition(tSquaddiePsi);
            totalPsi += getStatValueForAddition(tCorporalPsi);
            totalPsi += getStatValueForAddition(tSergeantPsi);
            totalPsi += getStatValueForAddition(tLieutenantPsi);
            totalPsi += getStatValueForAddition(tCaptainPsi);
            totalPsi += getStatValueForAddition(tMajorPsi);
            totalPsi += getStatValueForAddition(tColonelPsi);
            totalPsi += getStatValueForAddition(tBrigadierPsi);

            laTotalPsi.Text = totalPsi.ToString();
        }

        private void tMobility_TextChanged(object sender, EventArgs e)
        {
            updateTotalMobility();
        }

        private void updateTotalMobility()
        {
            int totalMobility = 0;

            totalMobility += getStatValueForAddition(tSquaddieMobility);
            totalMobility += getStatValueForAddition(tCorporalMobility);
            totalMobility += getStatValueForAddition(tSergeantMobility);
            totalMobility += getStatValueForAddition(tLieutenantMobility);
            totalMobility += getStatValueForAddition(tCaptainMobility);
            totalMobility += getStatValueForAddition(tMajorMobility);
            totalMobility += getStatValueForAddition(tColonelMobility);
            totalMobility += getStatValueForAddition(tBrigadierMobility);

            laTotalMobility.Text = totalMobility.ToString();
        }

        private void tWill_TextChanged(object sender, EventArgs e)
        {
            updateTotalWill();
        }

        private void updateTotalWill()
        {
            int totalWill = 0;

            totalWill += getStatValueForAddition(tSquaddieWill);
            totalWill += getStatValueForAddition(tCorporalWill);
            totalWill += getStatValueForAddition(tSergeantWill);
            totalWill += getStatValueForAddition(tLieutenantWill);
            totalWill += getStatValueForAddition(tCaptainWill);
            totalWill += getStatValueForAddition(tMajorWill);
            totalWill += getStatValueForAddition(tColonelWill);
            totalWill += getStatValueForAddition(tBrigadierWill);

            laTotalWill.Text = totalWill.ToString();
        }

        private void tDodge_TextChanged(object sender, EventArgs e)
        {
            updateTotalDodge();
        }

        private void updateTotalDodge()
        {
            int totalDodge = 0;

            totalDodge += getStatValueForAddition(tSquaddieDodge);
            totalDodge += getStatValueForAddition(tCorporalDodge);
            totalDodge += getStatValueForAddition(tSergeantDodge);
            totalDodge += getStatValueForAddition(tLieutenantDodge);
            totalDodge += getStatValueForAddition(tCaptainDodge);
            totalDodge += getStatValueForAddition(tMajorDodge);
            totalDodge += getStatValueForAddition(tColonelDodge);
            totalDodge += getStatValueForAddition(tBrigadierDodge);

            laTotalDodge.Text = totalDodge.ToString();
        }

        private int getStatValueForAddition(TextBox textbox)
        {
            int? intValue = Utils.parseStringToInt(textbox.Text);
            if(intValue == null)
            {
                return 0;
            }
            else
            {
                return intValue.Value;
            }
        }

        #endregion Stat Totals

        private void bAddUnisexNickname_Click(object sender, EventArgs e)
        {
            if(addName())
            {
                tNewUnisexNickname.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in lvUnisexNicknames.SelectedItems)
            {
                lvUnisexNicknames.Items.Remove(eachItem);
            }
        }

        private void tNewUnisexNickname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                if (addName())
                {
                    tNewUnisexNickname.Text = "";
                    e.Handled = true;
                }
            }
        }

        private bool addName()
        {
            bool success = false;
            string newUnisexNickname = tNewUnisexNickname.Text;

            if (!string.IsNullOrEmpty(newUnisexNickname))
            {
                ListViewItem x = new ListViewItem(new ClassNickname(newUnisexNickname, (string)cNicknameGender.SelectedItem).getListViewStringArray());
                lvUnisexNicknames.Items.Add(x);
                success = true;
            }

            return success;
        }

        private void nicknamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportNicknamesForm form = new ImportNicknamesForm(ProjectState.getClassPack());
            form.ShowDialog();
        }

        private void bAddLoadout_Click(object sender, EventArgs e)
        {
            if (addLoadoutItem())
            {
                tNewLoadout.Text = "";
            }
        }

        private void tNewLoadout_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (addLoadoutItem())
                {
                    tNewLoadout.Text = "";
                    e.Handled = true;
                }
            }
        }

        private bool addLoadoutItem()
        {
            bool success = false;
            string newLoadoutItem = tNewLoadout.Text;

            if (!string.IsNullOrEmpty(newLoadoutItem))
            {
                ListViewItem x = new ListViewItem(newLoadoutItem);
                lvSquaddieLoadout.Items.Add(x);
                success = true;
            }

            return success;
        }

        private void bRemoveLoadout_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in lvSquaddieLoadout.SelectedItems)
            {
                lvSquaddieLoadout.Items.Remove(eachItem);
            }
        }

        private void tStat_Leave(object sender, EventArgs e)
        {
            TextBox statTextbox = sender as TextBox;
            
            if(!string.IsNullOrEmpty(statTextbox.Text))
            {
                int intStat;
                if(!int.TryParse(statTextbox.Text, out intStat))
                {
                    statTextbox.Text = "";
                }
            }
        }

        private void lvUnisexNicknames_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if(lvUnisexNicknames.SelectedIndices.Count > 0)
            {
                bRemoveNickname.Enabled = true;
            }
            else
            {
                bRemoveNickname.Enabled = false;
            }
        }

        private void tNewUnisexNickname_TextChanged(object sender, EventArgs e)
        {
            if(tNewUnisexNickname.Text.Length > 0)
            {
                bAddNickname.Enabled = true;
            }
            else
            {
                bAddNickname.Enabled = false;
            }
        }

        private void lvSquaddieLoadout_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvSquaddieLoadout.SelectedIndices.Count > 0)
            {
                bRemoveLoadout.Enabled = true;
            }
            else
            {
                bRemoveLoadout.Enabled = false;
            }
        }

        private void tNewLoadout_TextChanged(object sender, EventArgs e)
        {
            if (tNewLoadout.Text.Length > 0)
            {
                bAddLoadout.Enabled = true;
            }
            else
            {
                bAddLoadout.Enabled = false;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unloadClassPack();
        }
    }
}
