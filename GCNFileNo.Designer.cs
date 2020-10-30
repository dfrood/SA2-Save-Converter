namespace SA2_Save_Converter
{
    partial class GCNFileNoAndRegion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GCNFileNoAndRegion));
            this.btn_SetGCNFileNo = new MetroFramework.Controls.MetroButton();
            this.nud_GCNFileNo = new System.Windows.Forms.NumericUpDown();
            this.rb_USA = new MetroFramework.Controls.MetroRadioButton();
            this.rb_EUR = new MetroFramework.Controls.MetroRadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.nud_GCNFileNo)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_SetGCNFileNo
            // 
            this.btn_SetGCNFileNo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_SetGCNFileNo.BackgroundImage")));
            this.btn_SetGCNFileNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_SetGCNFileNo.Location = new System.Drawing.Point(239, 61);
            this.btn_SetGCNFileNo.Name = "btn_SetGCNFileNo";
            this.btn_SetGCNFileNo.Size = new System.Drawing.Size(23, 23);
            this.btn_SetGCNFileNo.TabIndex = 0;
            this.btn_SetGCNFileNo.UseSelectable = true;
            this.btn_SetGCNFileNo.Click += new System.EventHandler(this.btn_SetGCNFileNo_Click);
            // 
            // nud_GCNFileNo
            // 
            this.nud_GCNFileNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.nud_GCNFileNo.ForeColor = System.Drawing.Color.LightGray;
            this.nud_GCNFileNo.Location = new System.Drawing.Point(185, 63);
            this.nud_GCNFileNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_GCNFileNo.Name = "nud_GCNFileNo";
            this.nud_GCNFileNo.Size = new System.Drawing.Size(48, 20);
            this.nud_GCNFileNo.TabIndex = 1;
            this.nud_GCNFileNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rb_USA
            // 
            this.rb_USA.AutoSize = true;
            this.rb_USA.Location = new System.Drawing.Point(23, 66);
            this.rb_USA.Name = "rb_USA";
            this.rb_USA.Size = new System.Drawing.Size(45, 15);
            this.rb_USA.TabIndex = 2;
            this.rb_USA.Text = "USA";
            this.rb_USA.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rb_USA.UseSelectable = true;
            // 
            // rb_EUR
            // 
            this.rb_EUR.AutoSize = true;
            this.rb_EUR.Location = new System.Drawing.Point(74, 66);
            this.rb_EUR.Name = "rb_EUR";
            this.rb_EUR.Size = new System.Drawing.Size(44, 15);
            this.rb_EUR.TabIndex = 3;
            this.rb_EUR.Text = "EUR";
            this.rb_EUR.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rb_EUR.UseSelectable = true;
            // 
            // GCNFileNoAndRegion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 94);
            this.Controls.Add(this.rb_EUR);
            this.Controls.Add(this.rb_USA);
            this.Controls.Add(this.nud_GCNFileNo);
            this.Controls.Add(this.btn_SetGCNFileNo);
            this.MaximizeBox = false;
            this.Name = "GCNFileNoAndRegion";
            this.Resizable = false;
            this.Text = "GCN Region + File No.";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.nud_GCNFileNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btn_SetGCNFileNo;
        private System.Windows.Forms.NumericUpDown nud_GCNFileNo;
        private MetroFramework.Controls.MetroRadioButton rb_USA;
        private MetroFramework.Controls.MetroRadioButton rb_EUR;
    }
}