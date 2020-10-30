using MetroFramework.Forms;
using System;

namespace SA2_Save_Converter
{
    public partial class GCNFileNoAndRegion : MetroForm
    {
        public static int FileNo = 255;
        public static int GameRegion = 255;
        public GCNFileNoAndRegion()
        {
            InitializeComponent();
        }

        private void btn_SetGCNFileNo_Click(object sender, EventArgs e)
        {
            FileNo = (int)nud_GCNFileNo.Value;
            if (rb_EUR.Checked) { GameRegion = 1; }
            if (rb_USA.Checked) { GameRegion = 0; }
            this.Hide();
        }
    }
}
