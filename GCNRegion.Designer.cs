using System;

namespace SA2_Save_Converter
{
    partial class GCNRegion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GCNRegion));
            this.btn_SetGCNRegion = new MetroFramework.Controls.MetroButton();
            this.rb_EUR = new MetroFramework.Controls.MetroRadioButton();
            this.rb_USA = new MetroFramework.Controls.MetroRadioButton();
            this.SuspendLayout();
            // 
            // btn_SetGCNRegion
            // 
            this.btn_SetGCNRegion.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_SetGCNRegion.BackgroundImage")));
            this.btn_SetGCNRegion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_SetGCNRegion.Location = new System.Drawing.Point(130, 63);
            this.btn_SetGCNRegion.Name = "btn_SetGCNRegion";
            this.btn_SetGCNRegion.Size = new System.Drawing.Size(23, 23);
            this.btn_SetGCNRegion.TabIndex = 0;
            this.btn_SetGCNRegion.UseSelectable = true;
            this.btn_SetGCNRegion.Click += new System.EventHandler(this.btn_SetGCNRegion_Click);
            // 
            // rb_EUR
            // 
            this.rb_EUR.AutoSize = true;
            this.rb_EUR.Location = new System.Drawing.Point(80, 68);
            this.rb_EUR.Name = "rb_EUR";
            this.rb_EUR.Size = new System.Drawing.Size(44, 15);
            this.rb_EUR.TabIndex = 5;
            this.rb_EUR.Text = "EUR";
            this.rb_EUR.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rb_EUR.UseSelectable = true;
            // 
            // rb_USA
            // 
            this.rb_USA.AutoSize = true;
            this.rb_USA.Location = new System.Drawing.Point(29, 68);
            this.rb_USA.Name = "rb_USA";
            this.rb_USA.Size = new System.Drawing.Size(45, 15);
            this.rb_USA.TabIndex = 4;
            this.rb_USA.Text = "USA";
            this.rb_USA.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.rb_USA.UseSelectable = true;
            // 
            // GCNRegion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 97);
            this.Controls.Add(this.rb_EUR);
            this.Controls.Add(this.rb_USA);
            this.Controls.Add(this.btn_SetGCNRegion);
            this.MaximizeBox = false;
            this.Name = "GCNRegion";
            this.Resizable = false;
            this.Text = "GCN Region";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private MetroFramework.Controls.MetroButton btn_SetGCNRegion;
        private MetroFramework.Controls.MetroRadioButton rb_EUR;
        private MetroFramework.Controls.MetroRadioButton rb_USA;
    }
}