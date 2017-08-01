namespace Xcom2ClassManager.Forms
{
    partial class ClassPackExportForm
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
            this.chlClasses = new System.Windows.Forms.CheckedListBox();
            this.laDestination = new System.Windows.Forms.Label();
            this.bBrowse = new System.Windows.Forms.Button();
            this.tDestination = new System.Windows.Forms.TextBox();
            this.bExport = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chXcomClassDataIni = new System.Windows.Forms.CheckBox();
            this.chXcomGameInt = new System.Windows.Forms.CheckBox();
            this.chXcomLwOverhaulIni = new System.Windows.Forms.CheckBox();
            this.chForceVanilla = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lRequiredMods = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chlClasses
            // 
            this.chlClasses.FormattingEnabled = true;
            this.chlClasses.Location = new System.Drawing.Point(12, 12);
            this.chlClasses.Name = "chlClasses";
            this.chlClasses.Size = new System.Drawing.Size(377, 124);
            this.chlClasses.TabIndex = 0;
            this.chlClasses.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chlClasses_ItemCheck);
            // 
            // laDestination
            // 
            this.laDestination.AutoSize = true;
            this.laDestination.Location = new System.Drawing.Point(12, 148);
            this.laDestination.Name = "laDestination";
            this.laDestination.Size = new System.Drawing.Size(63, 13);
            this.laDestination.TabIndex = 40;
            this.laDestination.Text = "Destination:";
            // 
            // bBrowse
            // 
            this.bBrowse.Location = new System.Drawing.Point(309, 145);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(80, 21);
            this.bBrowse.TabIndex = 39;
            this.bBrowse.Text = "Browse";
            this.bBrowse.UseVisualStyleBackColor = true;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // tDestination
            // 
            this.tDestination.Location = new System.Drawing.Point(80, 145);
            this.tDestination.Name = "tDestination";
            this.tDestination.Size = new System.Drawing.Size(223, 20);
            this.tDestination.TabIndex = 38;
            // 
            // bExport
            // 
            this.bExport.Location = new System.Drawing.Point(309, 370);
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(80, 21);
            this.bExport.TabIndex = 41;
            this.bExport.Text = "Export";
            this.bExport.UseVisualStyleBackColor = true;
            this.bExport.Click += new System.EventHandler(this.bExport_Click);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(228, 370);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(75, 21);
            this.bClose.TabIndex = 42;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chXcomLwOverhaulIni);
            this.groupBox1.Controls.Add(this.chXcomGameInt);
            this.groupBox1.Controls.Add(this.chXcomClassDataIni);
            this.groupBox1.Location = new System.Drawing.Point(12, 188);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 96);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export Files";
            // 
            // chXcomClassDataIni
            // 
            this.chXcomClassDataIni.AutoSize = true;
            this.chXcomClassDataIni.Location = new System.Drawing.Point(7, 20);
            this.chXcomClassDataIni.Name = "chXcomClassDataIni";
            this.chXcomClassDataIni.Size = new System.Drawing.Size(115, 17);
            this.chXcomClassDataIni.TabIndex = 0;
            this.chXcomClassDataIni.Text = "XComClassData.ini";
            this.chXcomClassDataIni.UseVisualStyleBackColor = true;
            // 
            // chXcomGameInt
            // 
            this.chXcomGameInt.AutoSize = true;
            this.chXcomGameInt.Location = new System.Drawing.Point(7, 43);
            this.chXcomGameInt.Name = "chXcomGameInt";
            this.chXcomGameInt.Size = new System.Drawing.Size(103, 17);
            this.chXcomGameInt.TabIndex = 1;
            this.chXcomGameInt.Text = "XComGame.INT";
            this.chXcomGameInt.UseVisualStyleBackColor = true;
            // 
            // chXcomLwOverhaulIni
            // 
            this.chXcomLwOverhaulIni.AutoSize = true;
            this.chXcomLwOverhaulIni.Location = new System.Drawing.Point(7, 66);
            this.chXcomLwOverhaulIni.Name = "chXcomLwOverhaulIni";
            this.chXcomLwOverhaulIni.Size = new System.Drawing.Size(133, 17);
            this.chXcomLwOverhaulIni.TabIndex = 2;
            this.chXcomLwOverhaulIni.Text = "XComLW_Overhaul.ini";
            this.chXcomLwOverhaulIni.UseVisualStyleBackColor = true;
            // 
            // chForceVanilla
            // 
            this.chForceVanilla.AutoSize = true;
            this.chForceVanilla.Location = new System.Drawing.Point(6, 19);
            this.chForceVanilla.Name = "chForceVanilla";
            this.chForceVanilla.Size = new System.Drawing.Size(148, 17);
            this.chForceVanilla.TabIndex = 3;
            this.chForceVanilla.Text = "Force Vanilla Compatibility";
            this.chForceVanilla.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chForceVanilla);
            this.groupBox2.Location = new System.Drawing.Point(12, 301);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 52);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Export Options";
            // 
            // lRequiredMods
            // 
            this.lRequiredMods.FormattingEnabled = true;
            this.lRequiredMods.Location = new System.Drawing.Point(396, 13);
            this.lRequiredMods.Name = "lRequiredMods";
            this.lRequiredMods.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lRequiredMods.Size = new System.Drawing.Size(321, 121);
            this.lRequiredMods.TabIndex = 45;
            // 
            // ClassPackExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 402);
            this.Controls.Add(this.lRequiredMods);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bExport);
            this.Controls.Add(this.laDestination);
            this.Controls.Add(this.bBrowse);
            this.Controls.Add(this.tDestination);
            this.Controls.Add(this.chlClasses);
            this.Name = "ClassPackExportForm";
            this.Text = "ClassPackExportForm";
            this.Load += new System.EventHandler(this.ClassPackExportForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chlClasses;
        private System.Windows.Forms.Label laDestination;
        private System.Windows.Forms.Button bBrowse;
        private System.Windows.Forms.TextBox tDestination;
        private System.Windows.Forms.Button bExport;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chXcomGameInt;
        private System.Windows.Forms.CheckBox chXcomClassDataIni;
        private System.Windows.Forms.CheckBox chXcomLwOverhaulIni;
        private System.Windows.Forms.CheckBox chForceVanilla;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lRequiredMods;
    }
}