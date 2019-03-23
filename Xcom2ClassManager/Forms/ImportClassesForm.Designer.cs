namespace Xcom2ClassManager.Forms
{
    partial class ImportClassesForm
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
            this.laFileInt = new System.Windows.Forms.Label();
            this.bImport = new System.Windows.Forms.Button();
            this.bBrowseInt = new System.Windows.Forms.Button();
            this.tFileInt = new System.Windows.Forms.TextBox();
            this.chListClasses = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bUpdate = new System.Windows.Forms.Button();
            this.laDisplayName = new System.Windows.Forms.Label();
            this.tDisplayName = new System.Windows.Forms.TextBox();
            this.laInternalName = new System.Windows.Forms.Label();
            this.tInternalName = new System.Windows.Forms.TextBox();
            this.laFileClass = new System.Windows.Forms.Label();
            this.tFileClass = new System.Windows.Forms.TextBox();
            this.bSave = new System.Windows.Forms.Button();
            this.bBrowseClass = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tFileGame = new System.Windows.Forms.TextBox();
            this.bBrowseGame = new System.Windows.Forms.Button();
            this.tModName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(358, 286);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(75, 23);
            this.bClose.TabIndex = 38;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // laFileInt
            // 
            this.laFileInt.AutoSize = true;
            this.laFileInt.Location = new System.Drawing.Point(12, 15);
            this.laFileInt.Name = "laFileInt";
            this.laFileInt.Size = new System.Drawing.Size(87, 13);
            this.laFileInt.TabIndex = 37;
            this.laFileInt.Text = "XComGame.INT:";
            // 
            // bImport
            // 
            this.bImport.Location = new System.Drawing.Point(439, 89);
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
            // chListClasses
            // 
            this.chListClasses.FormattingEnabled = true;
            this.chListClasses.Location = new System.Drawing.Point(15, 135);
            this.chListClasses.Name = "chListClasses";
            this.chListClasses.Size = new System.Drawing.Size(178, 124);
            this.chListClasses.TabIndex = 33;
            this.chListClasses.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chListClasses_ItemCheck);
            this.chListClasses.SelectedValueChanged += new System.EventHandler(this.chListClasses_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bUpdate);
            this.groupBox1.Controls.Add(this.laDisplayName);
            this.groupBox1.Controls.Add(this.tDisplayName);
            this.groupBox1.Controls.Add(this.laInternalName);
            this.groupBox1.Controls.Add(this.tInternalName);
            this.groupBox1.Location = new System.Drawing.Point(199, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 124);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Class Metadata";
            // 
            // bUpdate
            // 
            this.bUpdate.Location = new System.Drawing.Point(225, 95);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(75, 23);
            this.bUpdate.TabIndex = 41;
            this.bUpdate.Text = "Update";
            this.bUpdate.UseVisualStyleBackColor = true;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
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
            this.tDisplayName.Location = new System.Drawing.Point(88, 56);
            this.tDisplayName.Name = "tDisplayName";
            this.tDisplayName.Size = new System.Drawing.Size(212, 20);
            this.tDisplayName.TabIndex = 31;
            // 
            // laInternalName
            // 
            this.laInternalName.AutoSize = true;
            this.laInternalName.Location = new System.Drawing.Point(6, 33);
            this.laInternalName.Name = "laInternalName";
            this.laInternalName.Size = new System.Drawing.Size(76, 13);
            this.laInternalName.TabIndex = 30;
            this.laInternalName.Text = "Internal Name:";
            // 
            // tInternalName
            // 
            this.tInternalName.Location = new System.Drawing.Point(88, 30);
            this.tInternalName.Name = "tInternalName";
            this.tInternalName.Size = new System.Drawing.Size(212, 20);
            this.tInternalName.TabIndex = 29;
            // 
            // laFileClass
            // 
            this.laFileClass.AutoSize = true;
            this.laFileClass.Location = new System.Drawing.Point(12, 41);
            this.laFileClass.Name = "laFileClass";
            this.laFileClass.Size = new System.Drawing.Size(99, 13);
            this.laFileClass.TabIndex = 38;
            this.laFileClass.Text = "XComClassData.ini:";
            // 
            // tFileClass
            // 
            this.tFileClass.Location = new System.Drawing.Point(120, 38);
            this.tFileClass.Name = "tFileClass";
            this.tFileClass.ReadOnly = true;
            this.tFileClass.Size = new System.Drawing.Size(313, 20);
            this.tFileClass.TabIndex = 37;
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(439, 286);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(75, 23);
            this.bSave.TabIndex = 40;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bBrowseClass
            // 
            this.bBrowseClass.Location = new System.Drawing.Point(439, 36);
            this.bBrowseClass.Name = "bBrowseClass";
            this.bBrowseClass.Size = new System.Drawing.Size(75, 23);
            this.bBrowseClass.TabIndex = 41;
            this.bBrowseClass.Text = "Browse";
            this.bBrowseClass.UseVisualStyleBackColor = true;
            this.bBrowseClass.Click += new System.EventHandler(this.bBrowseClass_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "XComGameData.ini:";
            // 
            // tFileGame
            // 
            this.tFileGame.Location = new System.Drawing.Point(120, 64);
            this.tFileGame.Name = "tFileGame";
            this.tFileGame.ReadOnly = true;
            this.tFileGame.Size = new System.Drawing.Size(313, 20);
            this.tFileGame.TabIndex = 43;
            // 
            // bBrowseGame
            // 
            this.bBrowseGame.Location = new System.Drawing.Point(439, 62);
            this.bBrowseGame.Name = "bBrowseGame";
            this.bBrowseGame.Size = new System.Drawing.Size(75, 23);
            this.bBrowseGame.TabIndex = 44;
            this.bBrowseGame.Text = "Browse";
            this.bBrowseGame.UseVisualStyleBackColor = true;
            this.bBrowseGame.Click += new System.EventHandler(this.bBrowseGame_Click);
            // 
            // tModName
            // 
            this.tModName.Location = new System.Drawing.Point(120, 90);
            this.tModName.Name = "tModName";
            this.tModName.Size = new System.Drawing.Size(313, 20);
            this.tModName.TabIndex = 45;
            this.tModName.TextChanged += new System.EventHandler(this.tModName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Mod Name:";
            // 
            // ImportClassesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 328);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tModName);
            this.Controls.Add(this.bBrowseGame);
            this.Controls.Add(this.tFileGame);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bBrowseClass);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.laFileClass);
            this.Controls.Add(this.tFileClass);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.laFileInt);
            this.Controls.Add(this.bImport);
            this.Controls.Add(this.bBrowseInt);
            this.Controls.Add(this.tFileInt);
            this.Controls.Add(this.chListClasses);
            this.Name = "ImportClassesForm";
            this.Text = "Import Classes";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Label laFileInt;
        private System.Windows.Forms.Button bImport;
        private System.Windows.Forms.Button bBrowseInt;
        private System.Windows.Forms.TextBox tFileInt;
        private System.Windows.Forms.CheckedListBox chListClasses;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label laDisplayName;
        private System.Windows.Forms.TextBox tDisplayName;
        private System.Windows.Forms.Label laInternalName;
        private System.Windows.Forms.TextBox tInternalName;
        private System.Windows.Forms.Label laFileClass;
        private System.Windows.Forms.TextBox tFileClass;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.Button bBrowseClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tFileGame;
        private System.Windows.Forms.Button bBrowseGame;
        private System.Windows.Forms.TextBox tModName;
        private System.Windows.Forms.Label label2;
    }
}