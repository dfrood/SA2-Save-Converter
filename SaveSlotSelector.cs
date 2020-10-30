using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA2_Save_Converter
{
    public partial class SaveSlotSelector : MetroForm
    {
        public static Dictionary<int, int> cbSlotList = new Dictionary<int, int>();

        Offsets offsets = new Offsets();

        public SaveSlotSelector()
        {
            InitializeComponent();
            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            cbSlotList = new Dictionary<int, int>();
            int saveSize = 0;
            int index = 0;
            if (Main.saveConsole == 2) { saveSize = 0x6008; }
            if (Main.saveConsole == 3) { saveSize = 0x6004; }
            foreach (byte[] main in Main.SplitByteArray(Main.loadedSave.ToArray(), saveSize))
            {
                if (BitConverter.ToString(main.Take(4).ToArray()) != "00-00-00-00")
                {
                    string comboBoxText;
                    List<byte> emblemText = new List<byte>(main.Skip(0x2F).Take(0x19));
                    int playTime = BitConverter.ToInt32(main.Skip(Convert.ToInt32(offsets.main.PlayTime + saveSize - 0x6000)).Take(4).Reverse().ToArray(), 0);
                    int hours = playTime / 216000;
                    int minutes = (playTime - (hours * 216000)) / 3600;
                    int seconds = ((playTime - (hours * 216000)) - ((playTime - (hours * 21600)) - ((playTime - (hours * 21600)) - minutes * 3600))) / 60;
                    string timeString = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
                    comboBoxText = Encoding.UTF8.GetString(emblemText.Take(emblemText.IndexOf(0x00)).ToArray()) + " - " + timeString;

                    cb_SaveSlots.Items.Add(comboBoxText);
                    cbSlotList.Add(cb_SaveSlots.Items.Count-1, index);
                }
                index++;
            }
        }

        private void btn_SelectSaveSlot_Click(object sender, EventArgs e)
        {
            Main.selectedSlot = cbSlotList.Where(x => x.Key == cb_SaveSlots.SelectedIndex).First().Value;
            this.Dispose();
        }
    }
}
