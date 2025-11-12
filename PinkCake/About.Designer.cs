namespace PinkCake
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.StatusStripBottom = new System.Windows.Forms.StatusStrip();
            this.TsslServiceStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.TbPicturesFolder = new System.Windows.Forms.TextBox();
            this.BtnSelectFolder = new System.Windows.Forms.Button();
            this.LbSearchFilter = new System.Windows.Forms.Label();
            this.TbFilter = new System.Windows.Forms.TextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.ListBoxSources = new System.Windows.Forms.ListBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnRemove = new System.Windows.Forms.Button();
            this.StatusStripBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusStripBottom
            // 
            this.StatusStripBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsslServiceStatus});
            this.StatusStripBottom.Location = new System.Drawing.Point(0, 237);
            this.StatusStripBottom.Name = "StatusStripBottom";
            this.StatusStripBottom.Size = new System.Drawing.Size(499, 22);
            this.StatusStripBottom.TabIndex = 0;
            this.StatusStripBottom.Text = "statusStrip1";
            // 
            // TsslServiceStatus
            // 
            this.TsslServiceStatus.Name = "TsslServiceStatus";
            this.TsslServiceStatus.Size = new System.Drawing.Size(113, 17);
            this.TsslServiceStatus.Text = "Service not Running";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pictures Folder";
            // 
            // TbPicturesFolder
            // 
            this.TbPicturesFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbPicturesFolder.Location = new System.Drawing.Point(95, 145);
            this.TbPicturesFolder.Name = "TbPicturesFolder";
            this.TbPicturesFolder.Size = new System.Drawing.Size(305, 20);
            this.TbPicturesFolder.TabIndex = 2;
            this.TbPicturesFolder.TextChanged += new System.EventHandler(this.TbPicturesFolder_TextChanged);
            // 
            // BtnSelectFolder
            // 
            this.BtnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSelectFolder.Location = new System.Drawing.Point(406, 145);
            this.BtnSelectFolder.Name = "BtnSelectFolder";
            this.BtnSelectFolder.Size = new System.Drawing.Size(33, 23);
            this.BtnSelectFolder.TabIndex = 3;
            this.BtnSelectFolder.Text = "...";
            this.BtnSelectFolder.UseVisualStyleBackColor = true;
            this.BtnSelectFolder.Click += new System.EventHandler(this.BtnSelectFolder_Click);
            // 
            // LbSearchFilter
            // 
            this.LbSearchFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LbSearchFilter.AutoSize = true;
            this.LbSearchFilter.Location = new System.Drawing.Point(12, 175);
            this.LbSearchFilter.Name = "LbSearchFilter";
            this.LbSearchFilter.Size = new System.Drawing.Size(29, 13);
            this.LbSearchFilter.TabIndex = 4;
            this.LbSearchFilter.Text = "Filter";
            // 
            // TbFilter
            // 
            this.TbFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TbFilter.Location = new System.Drawing.Point(95, 172);
            this.TbFilter.Name = "TbFilter";
            this.TbFilter.Size = new System.Drawing.Size(104, 20);
            this.TbFilter.TabIndex = 5;
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnSave.Location = new System.Drawing.Point(406, 200);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 6;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // ListBoxSources
            // 
            this.ListBoxSources.FormattingEnabled = true;
            this.ListBoxSources.Location = new System.Drawing.Point(95, 12);
            this.ListBoxSources.Name = "ListBoxSources";
            this.ListBoxSources.Size = new System.Drawing.Size(344, 121);
            this.ListBoxSources.TabIndex = 7;
            this.ListBoxSources.SelectedIndexChanged += new System.EventHandler(this.ListBoxSources_SelectedIndexChanged);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAdd.Enabled = false;
            this.BtnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.Location = new System.Drawing.Point(448, 145);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(33, 23);
            this.BtnAdd.TabIndex = 8;
            this.BtnAdd.Text = "+";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnRemove
            // 
            this.BtnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnRemove.Enabled = false;
            this.BtnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRemove.Location = new System.Drawing.Point(448, 12);
            this.BtnRemove.Name = "BtnRemove";
            this.BtnRemove.Size = new System.Drawing.Size(33, 23);
            this.BtnRemove.TabIndex = 9;
            this.BtnRemove.Text = "X";
            this.BtnRemove.UseVisualStyleBackColor = true;
            this.BtnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 259);
            this.Controls.Add(this.BtnRemove);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.ListBoxSources);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.TbFilter);
            this.Controls.Add(this.LbSearchFilter);
            this.Controls.Add(this.BtnSelectFolder);
            this.Controls.Add(this.TbPicturesFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StatusStripBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "About";
            this.Text = "PinkCake";
            this.StatusStripBottom.ResumeLayout(false);
            this.StatusStripBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip StatusStripBottom;
        private System.Windows.Forms.ToolStripStatusLabel TsslServiceStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbPicturesFolder;
        private System.Windows.Forms.Button BtnSelectFolder;
        private System.Windows.Forms.Label LbSearchFilter;
        private System.Windows.Forms.TextBox TbFilter;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.ListBox ListBoxSources;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnRemove;
    }
}

