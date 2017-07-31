namespace Xcom2ClassManager.Forms
{
    partial class ImportNicknamesForm
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
            this.laFile = new System.Windows.Forms.Label();
            this.bImport = new System.Windows.Forms.Button();
            this.bBrowse = new System.Windows.Forms.Button();
            this.tFile = new System.Windows.Forms.TextBox();
            this.tvClassNicknames = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cSoldierClass = new System.Windows.Forms.ComboBox();
            this.bSave = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // laFile
            // 
            this.laFile.AutoSize = true;
            this.laFile.Location = new System.Drawing.Point(6, 17);
            this.laFile.Name = "laFile";
            this.laFile.Size = new System.Drawing.Size(47, 13);
            this.laFile.TabIndex = 41;
            this.laFile.Text = "INT File:";
            // 
            // bImport
            // 
            this.bImport.Location = new System.Drawing.Point(463, 12);
            this.bImport.Name = "bImport";
            this.bImport.Size = new System.Drawing.Size(75, 23);
            this.bImport.TabIndex = 40;
            this.bImport.Text = "Import";
            this.bImport.UseVisualStyleBackColor = true;
            this.bImport.Click += new System.EventHandler(this.bImport_Click);
            // 
            // bBrowse
            // 
            this.bBrowse.Location = new System.Drawing.Point(382, 12);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(75, 23);
            this.bBrowse.TabIndex = 39;
            this.bBrowse.Text = "Browse";
            this.bBrowse.UseVisualStyleBackColor = true;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // tFile
            // 
            this.tFile.Location = new System.Drawing.Point(74, 12);
            this.tFile.Name = "tFile";
            this.tFile.Size = new System.Drawing.Size(290, 20);
            this.tFile.TabIndex = 38;
            // 
            // tvClassNicknames
            // 
            this.tvClassNicknames.CheckBoxes = true;
            this.tvClassNicknames.FullRowSelect = true;
            this.tvClassNicknames.Location = new System.Drawing.Point(74, 53);
            this.tvClassNicknames.Name = "tvClassNicknames";
            this.tvClassNicknames.Size = new System.Drawing.Size(464, 207);
            this.tvClassNicknames.TabIndex = 42;
            this.tvClassNicknames.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvClassNicknames_AfterCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Nicknames:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Target:";
            // 
            // cSoldierClass
            // 
            this.cSoldierClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cSoldierClass.FormattingEnabled = true;
            this.cSoldierClass.Items.AddRange(new object[] {
            "Unisex",
            "Male",
            "Female"});
            this.cSoldierClass.Location = new System.Drawing.Point(74, 275);
            this.cSoldierClass.Name = "cSoldierClass";
            this.cSoldierClass.Size = new System.Drawing.Size(224, 21);
            this.cSoldierClass.TabIndex = 45;
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(463, 273);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(75, 23);
            this.bSave.TabIndex = 47;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(382, 273);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(75, 23);
            this.bClose.TabIndex = 46;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            // 
            // ImportNicknamesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 317);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.cSoldierClass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvClassNicknames);
            this.Controls.Add(this.laFile);
            this.Controls.Add(this.bImport);
            this.Controls.Add(this.bBrowse);
            this.Controls.Add(this.tFile);
            this.Name = "ImportNicknamesForm";
            this.Text = "ImportNicknamesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label laFile;
        private System.Windows.Forms.Button bImport;
        private System.Windows.Forms.Button bBrowse;
        private System.Windows.Forms.TextBox tFile;
        private System.Windows.Forms.TreeView tvClassNicknames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cSoldierClass;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bClose;
    }
}