using MetroFramework.Forms;
using System;

namespace SA2_Save_Converter
{
    public partial class GCNRegion : MetroForm
    {
        public static int GameRegion = 255;
        public GCNRegion()
        {
            InitializeComponent();
        }

        private void btn_SetGCNRegion_Click(object sender, EventArgs e)
        {
            if (rb_EUR.Checked) { GameRegion = 1; }
            if (rb_USA.Checked) { GameRegion = 0; }
            this.Hide();
        }
    }
}
