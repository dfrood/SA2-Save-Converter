namespace SA2_Save_Converter
{
    partial class SaveSlotSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveSlotSelector));
            this.btn_SelectSaveSlot = new MetroFramework.Controls.MetroButton();
            this.cb_SaveSlots = new MetroFramework.Controls.MetroComboBox();
            this.SuspendLayout();
            // 
            // btn_SelectSaveSlot
            // 
            this.btn_SelectSaveSlot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_SelectSaveSlot.BackgroundImage")));
            this.btn_SelectSaveSlot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_SelectSaveSlot.Location = new System.Drawing.Point(318, 66);
            this.btn_SelectSaveSlot.Name = "btn_SelectSaveSlot";
            this.btn_SelectSaveSlot.Size = new System.Drawing.Size(23, 23);
            this.btn_SelectSaveSlot.TabIndex = 1;
            this.btn_SelectSaveSlot.UseSelectable = true;
            this.btn_SelectSaveSlot.Click += new System.EventHandler(this.btn_SelectSaveSlot_Click);
            // 
            // cb_SaveSlots
            // 
            this.cb_SaveSlots.FormattingEnabled = true;
            this.cb_SaveSlots.ItemHeight = 23;
            this.cb_SaveSlots.Location = new System.Drawing.Point(23, 63);
            this.cb_SaveSlots.Name = "cb_SaveSlots";
            this.cb_SaveSlots.Size = new System.Drawing.Size(272, 29);
            this.cb_SaveSlots.TabIndex = 2;
            this.cb_SaveSlots.UseSelectable = true;
            // 
            // SaveSlotSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 110);
            this.Controls.Add(this.cb_SaveSlots);
            this.Controls.Add(this.btn_SelectSaveSlot);
            this.MaximizeBox = false;
            this.Name = "SaveSlotSelector";
            this.Resizable = false;
            this.Text = "Which save should we convert?";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton btn_SelectSaveSlot;
        private MetroFramework.Controls.MetroComboBox cb_SaveSlots;
    }
}