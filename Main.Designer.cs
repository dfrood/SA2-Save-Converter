namespace SA2_Save_Converter
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btn_ToPC = new MetroFramework.Controls.MetroButton();
            this.btn_ToGCN = new MetroFramework.Controls.MetroButton();
            this.btn_To360 = new MetroFramework.Controls.MetroButton();
            this.btn_ToPS3 = new MetroFramework.Controls.MetroButton();
            this.btn_LoadSave = new MetroFramework.Controls.MetroButton();
            this.btn_GitHub = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // btn_ToPC
            // 
            this.btn_ToPC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ToPC.BackgroundImage")));
            this.btn_ToPC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_ToPC.Enabled = false;
            this.btn_ToPC.Location = new System.Drawing.Point(23, 76);
            this.btn_ToPC.Name = "btn_ToPC";
            this.btn_ToPC.Size = new System.Drawing.Size(104, 113);
            this.btn_ToPC.TabIndex = 0;
            this.btn_ToPC.UseSelectable = true;
            this.btn_ToPC.Click += new System.EventHandler(this.btn_ToPC_Click);
            // 
            // btn_ToGCN
            // 
            this.btn_ToGCN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ToGCN.BackgroundImage")));
            this.btn_ToGCN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_ToGCN.Enabled = false;
            this.btn_ToGCN.Location = new System.Drawing.Point(133, 76);
            this.btn_ToGCN.Name = "btn_ToGCN";
            this.btn_ToGCN.Size = new System.Drawing.Size(104, 113);
            this.btn_ToGCN.TabIndex = 1;
            this.btn_ToGCN.UseSelectable = true;
            this.btn_ToGCN.Click += new System.EventHandler(this.btn_ToGCN_Click);
            // 
            // btn_To360
            // 
            this.btn_To360.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_To360.BackgroundImage")));
            this.btn_To360.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_To360.Enabled = false;
            this.btn_To360.Location = new System.Drawing.Point(23, 195);
            this.btn_To360.Name = "btn_To360";
            this.btn_To360.Size = new System.Drawing.Size(104, 113);
            this.btn_To360.TabIndex = 2;
            this.btn_To360.UseSelectable = true;
            this.btn_To360.Click += new System.EventHandler(this.btn_To360_Click);
            // 
            // btn_ToPS3
            // 
            this.btn_ToPS3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ToPS3.BackgroundImage")));
            this.btn_ToPS3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_ToPS3.Enabled = false;
            this.btn_ToPS3.Location = new System.Drawing.Point(133, 195);
            this.btn_ToPS3.Name = "btn_ToPS3";
            this.btn_ToPS3.Size = new System.Drawing.Size(104, 113);
            this.btn_ToPS3.TabIndex = 3;
            this.btn_ToPS3.UseSelectable = true;
            this.btn_ToPS3.Click += new System.EventHandler(this.btn_ToPS3_Click);
            // 
            // btn_LoadSave
            // 
            this.btn_LoadSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_LoadSave.BackgroundImage")));
            this.btn_LoadSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_LoadSave.Location = new System.Drawing.Point(244, 268);
            this.btn_LoadSave.Name = "btn_LoadSave";
            this.btn_LoadSave.Size = new System.Drawing.Size(40, 40);
            this.btn_LoadSave.TabIndex = 4;
            this.btn_LoadSave.UseSelectable = true;
            this.btn_LoadSave.Click += new System.EventHandler(this.btn_LoadSave_Click);
            // 
            // btn_GitHub
            // 
            this.btn_GitHub.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_GitHub.BackgroundImage")));
            this.btn_GitHub.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_GitHub.Location = new System.Drawing.Point(244, 76);
            this.btn_GitHub.Name = "btn_GitHub";
            this.btn_GitHub.Size = new System.Drawing.Size(40, 40);
            this.btn_GitHub.TabIndex = 6;
            this.btn_GitHub.UseSelectable = true;
            this.btn_GitHub.Click += new System.EventHandler(this.btn_GitHub_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 331);
            this.Controls.Add(this.btn_GitHub);
            this.Controls.Add(this.btn_LoadSave);
            this.Controls.Add(this.btn_ToPS3);
            this.Controls.Add(this.btn_To360);
            this.Controls.Add(this.btn_ToGCN);
            this.Controls.Add(this.btn_ToPC);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Padding = new System.Windows.Forms.Padding(20, 65, 20, 20);
            this.Resizable = false;
            this.Text = "SA2 Save Converter";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton btn_ToPC;
        private MetroFramework.Controls.MetroButton btn_ToGCN;
        private MetroFramework.Controls.MetroButton btn_To360;
        private MetroFramework.Controls.MetroButton btn_ToPS3;
        private MetroFramework.Controls.MetroButton btn_LoadSave;
        private MetroFramework.Controls.MetroButton btn_GitHub;
    }
}

