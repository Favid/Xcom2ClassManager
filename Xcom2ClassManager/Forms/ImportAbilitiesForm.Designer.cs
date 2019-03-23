namespace Xcom2ClassManager.Forms
{
    partial class ImportAbilitiesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bClose = new System.Windows.Forms.Button();
            this.laFile = new System.Windows.Forms.Label();
            this.bImport = new System.Windows.Forms.Button();
            this.bBrowseInt = new System.Windows.Forms.Button();
            this.tFileInt = new System.Windows.Forms.TextBox();
            this.chListAbilities = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cWeaponSlot = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.laDescription = new System.Windows.Forms.Label();
            this.tDescription = new System.Windows.Forms.TextBox();
            this.laDisplayName = new System.Windows.Forms.Label();
            this.tDisplayName = new System.Windows.Forms.TextBox();
            this.laInternalName = new System.Windows.Forms.Label();
            this.tInternalName = new System.Windows.Forms.TextBox();
            this.laRequiredMod = new System.Windows.Forms.Label();
            this.tModName = new System.Windows.Forms.TextBox();
            this.bSave = new System.Windows.Forms.Button();
            this.tFileClass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bBrowseClass = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(358, 358);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(75, 23);
            this.bClose.TabIndex = 38;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // laFile
            // 
            this.laFile.AutoSize = true;
            this.laFile.Location = new System.Drawing.Point(12, 15);
            this.laFile.Name = "laFile";
            this.laFile.Size = new System.Drawing.Size(87, 13);
            this.laFile.TabIndex = 37;
            this.laFile.Text = "XComGame.INT:";
            // 
            // bImport
            // 
            this.bImport.Location = new System.Drawing.Point(439, 62);
            this.bImport.Name = "bImport";
            this.bImport.Size = new System.Drawing.Size(75, 23);
            this.bImport.TabIndex = 36;
            this.bImport.Text = "Import";
            this.bImport.UseVisualStyleBackColor = true;
            this.bImport.Click += new System.EventHandler(this.bImport_Click);
            // 
            // bBrowseInt
            // 
            this.bBrowseInt.Location = new System.Drawing.Point(439, 10);
            this.bBrowseInt.Name = "bBrowseInt";
            this.bBrowseInt.Size = new System.Drawing.Size(75, 23);
            this.bBrowseInt.TabIndex = 35;
            this.bBrowseInt.Text = "Browse";
            this.bBrowseInt.UseVisualStyleBackColor = true;
            this.bBrowseInt.Click += new System.EventHandler(this.bBrowseInt_Click);
            // 
            // tFileInt
            // 
            this.tFileInt.Location = new System.Drawing.Point(120, 12);
            this.tFileInt.Name = "tFileInt";
            this.tFileInt.ReadOnly = true;
            this.tFileInt.Size = new System.Drawing.Size(313, 20);
            this.tFileInt.TabIndex = 34;
            // 
            // chListAbilities
            // 
            this.chListAbilities.FormattingEnabled = true;
            this.chListAbilities.Location = new System.Drawing.Point(12, 101);
            this.chListAbilities.Name = "chListAbilities";
            this.chListAbilities.Size = new System.Drawing.Size(178, 229);
            this.chListAbilities.TabIndex = 33;
            this.chListAbilities.SelectedValueChanged += new System.EventHandler(this.chListClasses_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cWeaponSlot);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.laDescription);
            this.groupBox1.Controls.Add(this.tDescription);
            this.groupBox1.Controls.Add(this.laDisplayName);
            this.groupBox1.Controls.Add(this.tDisplayName);
            this.groupBox1.Controls.Add(this.laInternalName);
            this.groupBox1.Controls.Add(this.tInternalName);
            this.groupBox1.Location = new System.Drawing.Point(199, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 229);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ability Metadata";
            // 
            // cWeaponSlot
            // 
            this.cWeaponSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cWeaponSlot.FormattingEnabled = true;
            this.cWeaponSlot.Location = new System.Drawing.Point(97, 150);
            this.cWeaponSlot.Name = "cWeaponSlot";
            this.cWeaponSlot.Size = new System.Drawing.Size(212, 21);
            this.cWeaponSlot.TabIndex = 39;
            this.cWeaponSlot.SelectedValueChanged += new System.EventHandler(this.cWeaponSlot_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Weapon Slot";
            // 
            // laDescription
            // 
            this.laDescription.AutoSize = true;
            this.laDescription.Location = new System.Drawing.Point(6, 85);
            this.laDescription.Name = "laDescription";
            this.laDescription.Size = new System.Drawing.Size(60, 13);
            this.laDescription.TabIndex = 34;
            this.laDescription.Text = "Description";
            // 
            // tDescription
            // 
            this.tDescription.Location = new System.Drawing.Point(97, 82);
            this.tDescription.Multiline = true;
            this.tDescription.Name = "tDescription";
            this.tDescription.Size = new System.Drawing.Size(212, 62);
            this.tDescription.TabIndex = 33;
            this.tDescription.Leave += new System.EventHandler(this.tDescription_Leave);
            // 
            // laDisplayName
            // 
            this.laDisplayName.AutoSize = true;
            this.laDisplayName.Location = new System.Drawing.Point(6, 59);
            this.laDisplayName.Name = "laDisplayName";
            this.laDisplayName.Size = new System.Drawing.Size(75, 13);
            this.laDisplayName.TabIndex = 32;
            this.laDisplayName.Text = "Display Name:";
            // 
            // tDisplayName
            // 
            this.tDisplayName.Location = new System.Drawing.Point(97, 56);
            this.tDisplayName.Name = "tDisplayName";
            this.tDisplayName.Size = new System.Drawing.Size(212, 20);
            this.tDisplayName.TabIndex = 31;
            this.tDisplayName.Leave += new System.EventHandler(this.tDisplayName_Leave);
            // 
            // laInternalName
            // 
            this.laInternalName.AutoSize = true;
            this.laInternalName.Location = new System.Drawing.Point(6, 33);
            this.laInternalName.Name = "laInternalName";
            this.laInternalName.Size = new System.Drawing.Size(85, 13);
            this.laInternalName.TabIndex = 30;
            this.laInternalName.Text = "Template Name:";
            // 
            // tInternalName
            // 
            this.tInternalName.Location = new System.Drawing.Point(97, 30);
            this.tInternalName.Name = "tInternalName";
            this.tInternalName.Size = new System.Drawing.Size(212, 20);
            this.tInternalName.TabIndex = 29;
            this.tInternalName.Leave += new System.EventHandler(this.tInternalName_Leave);
            // 
            // laRequiredMod
            // 
            this.laRequiredMod.AutoSize = true;
            this.laRequiredMod.Location = new System.Drawing.Point(12, 68);
            this.laRequiredMod.Name = "laRequiredMod";
            this.laRequiredMod.Size = new System.Drawing.Size(62, 13);
            this.laRequiredMod.TabIndex = 38;
            this.laRequiredMod.Text = "Mod Name:";
            // 
            // tModName
            // 
            this.tModName.Location = new System.Drawing.Point(120, 64);
            this.tModName.Name = "tModName";
            this.tModName.Size = new System.Drawing.Size(313, 20);
            this.tModName.TabIndex = 37;
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(439, 358);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(75, 23);
            this.bSave.TabIndex = 40;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // tFileClass
            // 
            this.tFileClass.Location = new System.Drawing.Point(120, 38);
            this.tFileClass.Name = "tFileClass";
            this.tFileClass.ReadOnly = true;
            this.tFileClass.Size = new System.Drawing.Size(313, 20);
            this.tFileClass.TabIndex = 41;
            this.tFileClass.Text = "     (Optional)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "XComClassData.ini:";
            // 
            // bBrowseClass
            // 
            this.bBrowseClass.Location = new System.Drawing.Point(439, 36);
            this.bBrowseClass.Name = "bBrowseClass";
            this.bBrowseClass.Size = new System.Drawing.Size(75, 23);
            this.bBrowseClass.TabIndex = 43;
            this.bBrowseClass.Text = "Browse";
            this.bBrowseClass.UseVisualStyleBackColor = true;
            this.bBrowseClass.Click += new System.EventHandler(this.bBrowseClass_Click);
            // 
            // ImportAbilitiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 393);
            this.Controls.Add(this.bBrowseClass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tFileClass);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.laRequiredMod);
            this.Controls.Add(this.tModName);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.laFile);
            this.Controls.Add(this.bImport);
            this.Controls.Add(this.bBrowseInt);
            this.Controls.Add(this.tFileInt);
            this.Controls.Add(this.chListAbilities);
            this.Name = "ImportAbilitiesForm";
            this.Text = "ImportAbilities";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Label laFile;
        private System.Windows.Forms.Button bImport;
        private System.Windows.Forms.Button bBrowseInt;
        private System.Windows.Forms.TextBox tFileInt;
        private System.Windows.Forms.CheckedListBox chListAbilities;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label laDescription;
        private System.Windows.Forms.TextBox tDescription;
        private System.Windows.Forms.Label laDisplayName;
        private System.Windows.Forms.TextBox tDisplayName;
        private System.Windows.Forms.Label laInternalName;
        private System.Windows.Forms.TextBox tInternalName;
        private System.Windows.Forms.Label laRequiredMod;
        private System.Windows.Forms.TextBox tModName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cWeaponSlot;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.TextBox tFileClass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bBrowseClass;
    }
}