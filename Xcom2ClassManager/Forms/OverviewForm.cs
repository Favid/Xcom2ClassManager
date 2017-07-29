using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xcom2ClassManager.Forms
{
    public partial class OverviewForm : Form
    {
        public OverviewForm()
        {
            InitializeComponent();
        }

        private void TabbedPrototype_Load(object sender, EventArgs e)
        {
            initAbilitiesDataSources();
            chDragAndDrop.Checked = false;

            //SoldierClass openSoldierCLass = ProjectState.getOpenSoldierClass();
            //if (openSoldierCLass != null)
            //{
            //    open(ProjectState.getOpenSoldierClass());
            //}
        }

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
            //List<Ability> abilities = new List<Ability>(ProjectState.getAbilities());
            //abilities.Add(new Ability());
            //abilities = abilities.OrderBy(x => x.internalName).ToList();

            //combo.DataSource = abilities;
        }

        private void setupMenuItemOpen()
        {
            //menuItemOpen.DropDownItems.Clear();

            //List<SoldierClass> soldierClasses = ProjectState.getSoldierClasses();
            //foreach (SoldierClass soldierClass in soldierClasses)
            //{
            //    ToolStripMenuItem item = new ToolStripMenuItem(soldierClass.metadata.internalName);
            //    item.Tag = soldierClass;
            //    item.Click += new System.EventHandler(this.menuItemOpenSoldierClass_Click);
            //    menuItemOpen.DropDownItems.Add(item);
            //}
        }

        private void menuItemOpenSoldierClass_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            SoldierClass soldierClass = (SoldierClass)item.Tag;
            open(soldierClass);
        }

        private void open(SoldierClass soldierClass)
        {
            //SoldierClass openSoldierClass = ProjectState.setOpenSoldierClass(soldierClass.getInternalName());
            //setupMenuItemOpen();

            //tDisplayName.Text = soldierClass.metadata.displayName;
            //tDescription.Text = soldierClass.metadata.description;
            //tIconString.Text = soldierClass.metadata.iconString;

            //tNumInForcedDeck.Text = soldierClass.experience.numberInForcedDeck.ToString();
            //tNumInDeck.Text = soldierClass.experience.numberInDeck.ToString();
            //tKillAssistsPerKill.Text = soldierClass.experience.killAssistsPerKill.ToString();

            //tSquaddieLoadout.Text = soldierClass.equipment.squaddieLoadout;
            //tAllowedArmor.Text = soldierClass.equipment.allowedArmors;

            //BindingList<Weapon> weapons = new BindingList<Weapon>(soldierClass.equipment.weapons);
            //lWeapons.DataSource = weapons;

            //openSoldierStats(soldierClass);
            //openSoldierAbilities(soldierClass);
        }

        private SoldierClass buildSoldierClass()
        {
            //SoldierClass soldierClass = new SoldierClass();

            //soldierClass.metadata.internalName = ProjectState.getOpenSoldierClass().metadata.internalName;
            //soldierClass.metadata.displayName = tDisplayName.Text;
            //soldierClass.metadata.description = tDescription.Text;
            //soldierClass.metadata.iconString = tIconString.Text;

            //soldierClass.experience.numberInForcedDeck = int.Parse(tNumInForcedDeck.Text);
            //soldierClass.experience.numberInDeck = int.Parse(tNumInDeck.Text);
            //soldierClass.experience.killAssistsPerKill = int.Parse(tKillAssistsPerKill.Text);

            //soldierClass.equipment.squaddieLoadout = tSquaddieLoadout.Text;
            //soldierClass.equipment.allowedArmors = tAllowedArmor.Text;
            //soldierClass.equipment.weapons = (lWeapons.DataSource as BindingList<Weapon>).ToList();

            //soldierClass.stats = buildSoldierClassStats();
            //soldierClass.soldierAbilities = buildSoldierClassAbilities();

            //return soldierClass;

            return null;
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
            //SoldierClassStat soldierStat = new SoldierClassStat();

            //soldierStat.rank = rank;
            //soldierStat.stat = stat;
            //soldierStat.value = Utils.parseStringToInt(control.Text);

            //return soldierStat;

            return null;
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

        private void selectClassToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO make sure rest of data can be saved first

            //SoldierClass soldierClass = ProjectState.saveClass(buildSoldierClass());
            //open(soldierClass);
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Rename rename = new Rename(ProjectState.getOpenSoldierClass().metadata.internalName);
            //rename.FormClosing += renameClosingListener;
            //rename.ShowDialog(this);
        }

        private void renameClosingListener(object sender, FormClosingEventArgs e)
        {
            //Rename renameForm = sender as Rename;
            //if (renameForm != null)
            //{
            //    if (!renameForm.getNewName().Equals(ProjectState.getOpenSoldierClassInternalName()))
            //    {
            //        ProjectState.renameClass(renameForm.getNewName());
            //        setupMenuItemOpen();
            //    }
            //}
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SoldierClass newClass = ProjectState.addClass();
            //open(newClass);
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //ProjectState.deleteClass();
            //open(ProjectState.getOpenSoldierClass());
        }

        private void bEditWeapon_Click(object sender, EventArgs e)
        {
            //WeaponEditor weaponEditor = new WeaponEditor(lWeapons.SelectedItem as Weapon, EditorState.EDIT);
            //weaponEditor.FormClosing += weaponEditorClosingListener;
            //weaponEditor.ShowDialog(this);
        }

        private void bAddWeapon_Click(object sender, EventArgs e)
        {
            //WeaponEditor weaponEditor = new WeaponEditor(null, EditorState.ADD);
            //weaponEditor.FormClosing += weaponEditorClosingListener;
            //weaponEditor.ShowDialog(this);
        }

        private void weaponEditorClosingListener(object sender, FormClosingEventArgs e)
        {
            //WeaponEditor weaponEditor = sender as WeaponEditor;
            //if (weaponEditor != null)
            //{
            //    BindingList<Weapon> weapons = lWeapons.DataSource as BindingList<Weapon>;
            //    Weapon oldWeapon = weaponEditor.oldWeapon;
            //    Weapon newWeapon = weaponEditor.newWeapon;

            //    if (weaponEditor.editorState == EditorState.CANCEL)
            //    {
            //        return;
            //    }
            //    else if (weaponEditor.editorState == EditorState.ADD)
            //    {
            //        weapons.Add(newWeapon);
            //        lWeapons.DataSource = weapons;
            //    }
            //    else if (weaponEditor.editorState == EditorState.EDIT)
            //    {
            //        int index = weapons.IndexOf(oldWeapon);

            //        if (index == -1)
            //        {
            //            weapons.Add(newWeapon);
            //        }
            //        else
            //        {
            //            weapons[index] = newWeapon;
            //        }

            //        lWeapons.DataSource = weapons;
            //    }
            //}
        }

        private void bDeleteWeapon_Click(object sender, EventArgs e)
        {
            BindingList<Weapon> weapons = lWeapons.DataSource as BindingList<Weapon>;
            weapons.Remove(lWeapons.SelectedItem as Weapon);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            //ComboBox comboSender = sender as ComboBox;
            //ComboBox comboDragged = e.Data.GetData(typeof(ComboBox)) as ComboBox;

            //string temp = comboSender.Text;

            //Ability senderAbility = ProjectState.getAbility(comboSender.Text);
            //if (senderAbility == null)
            //{
            //    senderAbility = new Ability();
            //}

            //Ability draggedAbility = ProjectState.getAbility(comboDragged.Text);
            //if (draggedAbility == null)
            //{
            //    draggedAbility = new Ability();
            //}

            //comboSender.SelectedIndex = comboSender.Items.IndexOf(draggedAbility);
            //comboDragged.SelectedIndex = comboSender.Items.IndexOf(senderAbility);
        }

        private void abilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Import Abilities
            //ImportAbilities dialog = new ImportAbilities(ProjectState.getAbilities());
            //dialog.ShowDialog();
        }

        private void classesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ImportClasses dialog = new ImportClasses();
            //dialog.ShowDialog();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SoldierClass classToCopy = ProjectState.getOpenSoldierClass();
            //SoldierClass newClass = new SoldierClass(classToCopy);

            //// TODO actually validate the name
            //newClass.metadata.internalName += "New";
            //newClass = ProjectState.addClass(newClass);

            //open(newClass);
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

        private void updateHelpText(string text)
        {
            laHelp.Text = text;
        }

        // new class pack
        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        // open class pack
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open file browser and let user select a class pack


        }

        // save class pack
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        // save as class pack
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
