using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SA2_Save_Converter
{
    public partial class Main : MetroForm
    {
        public GCNFileNoAndRegion GCNFileNo = new GCNFileNoAndRegion();
        public GCNRegion GCNRegion = new GCNRegion();

        public static byte[] loadedSave;

        public static string loadedFile;

        //PC-0, GCN-1, PS3-2, 360-3
        public static int saveConsole;
        //Main-0, Chao-1
        public static int saveType;

        public static int selectedSlot;

        public Main()
        {
            InitializeComponent();
        }

        public static List<byte[]> SplitByteArray(byte[] bytes, int BlockLength)
        {
            List<byte[]> _byteArrayList = new List<byte[]>();

            byte[] buffer;

            for (int i = 0; i < bytes.Length; i += BlockLength)
            {
                if ((i + BlockLength) > bytes.Length)
                {
                    buffer = new byte[bytes.Length - i];
                    Buffer.BlockCopy(bytes, i, buffer, 0, bytes.Length - i);
                }
                else
                {
                    buffer = new byte[BlockLength];
                    Buffer.BlockCopy(bytes, i, buffer, 0, BlockLength);
                }

                _byteArrayList.Add(buffer);
            }

            return _byteArrayList;
        }

        private void btn_LoadSave_Click(object sender, EventArgs e)
        {
            //Setup dialog OpenFileDialog for loading save file
            OpenFileDialog loadSave = new OpenFileDialog();
            loadSave.Filter = "Sonic Adventure 2 Main/Chao Save|*.*";
            loadSave.Title = "Load a Save";

            if (loadSave.ShowDialog() == DialogResult.OK)
            {
                bool validSave = false;
                loadedSave = File.ReadAllBytes(loadSave.FileName);

                if (loadedSave.Length == 0x6000) //PC Main Save
                {
                    saveConsole = 0;
                    saveType = 0;
                    validSave = true;
                }
                if (loadedSave.Length == 0x10000) //PC/360/PS3 Chao Save
                {
                    DialogResult result = MetroMessageBox.Show(this, "Is the save you're loading a PC Chao Save?", "PC or 360/PS3 Chao Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        saveConsole = 0;
                    }
                    if (result == DialogResult.No)
                    {
                        //360/PS3 is identical for Chao
                        saveConsole = 2;
                    }
                    saveType = 1;
                    validSave = true;
                }
                if (loadedSave.Length == 0x3C028) //360 Main Save
                {
                    saveConsole = 3;
                    saveType = 0;
                    validSave = true;
                    //Get the save slot
                    SaveSlotSelector SaveSlotSelector = new SaveSlotSelector();
                    SaveSlotSelector.ShowDialog();
                }
                if (loadedSave.Length == 0x3C050) //PS3 Main Save
                {
                    saveConsole = 2;
                    saveType = 0;
                    validSave = true;
                    //Get the save slot
                    SaveSlotSelector SaveSlotSelector = new SaveSlotSelector();
                    SaveSlotSelector.ShowDialog();
                }
                if (loadedSave.Length == 0x6040) //GCN Main Save
                {
                    saveConsole = 1;
                    saveType = 0;
                    loadedSave = loadedSave.Skip(0x40).ToArray();
                    validSave = true;
                }
                if (loadedSave.Length == 0x10040) //GCN Chao Save
                {
                    saveConsole = 1;
                    saveType = 1;
                    loadedSave = loadedSave.Skip(0x40).ToArray();
                    validSave = true;
                }

                if (validSave)
                {
                    loadedFile = loadSave.FileName;

                    string headerText = "";

                    switch (saveConsole)
                    {
                        case 0:
                            btn_ToPC.Enabled = false;
                            btn_ToGCN.Enabled = true;
                            btn_ToPS3.Enabled = true;
                            btn_To360.Enabled = true;
                            headerText += "PC ";
                            break;
                        case 1:
                            btn_ToPC.Enabled = true;
                            btn_ToGCN.Enabled = false;
                            btn_ToPS3.Enabled = true;
                            btn_To360.Enabled = true;
                            headerText += "GCN ";
                            break;
                        case 2:
                            btn_ToPC.Enabled = true;
                            btn_ToGCN.Enabled = true;
                            btn_ToPS3.Enabled = false;
                            btn_To360.Enabled = true;
                            headerText += "PS3 ";
                            break;
                        case 3:
                            btn_ToPC.Enabled = true;
                            btn_ToGCN.Enabled = true;
                            btn_ToPS3.Enabled = true;
                            btn_To360.Enabled = false;
                            headerText += "360 ";
                            break;
                    }

                    switch (saveType)
                    {
                        case 0:
                            headerText += "| Main Save";
                            break;
                        case 1:
                            headerText += "| Chao Save";
                            if (saveConsole == 2)
                            {
                                headerText = "PS3/360 | Chao Save";
                                btn_ToPS3.Enabled = true;
                                btn_To360.Enabled = true;
                            }
                            break;
                    }

                    this.Text = headerText;
                    this.Refresh();
                }
                else
                {
                    MetroMessageBox.Show(this, "That doesn't appear to be a Sonic Adventure 2 save file.", "Error loading save file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    loadedSave = null;
                }
            }
        }

        private void btn_ToPC_Click(object sender, EventArgs e)
        {
            string pcFileName;
            int index;

            //Converting to PC so only continue if we're not already a PC save.
            if (saveConsole != 0)
            {
                switch (saveType)
                {
                    case 0: //Main
                        //PS3 + 360 games are an array of saves so need to deal with that, will modify this in the future to deal with selecting which save to convert
                        //Only converts first save in file atm.
                        List<byte> toSave = new List<byte>();
                        switch (saveConsole)
                        {
                            case 1: //GC
                                toSave = MainSave.GCTextLanguageRemovedFix(loadedSave, false).ToList();
                                break;
                            case 2: //PS3
                                toSave = SplitByteArray(loadedSave, 0x6008)[selectedSlot].ToList();
                                toSave.RemoveRange(0, 0x08);
                                break;
                            case 3: //360
                                toSave = SplitByteArray(loadedSave, 0x6004)[selectedSlot].ToList();
                                toSave.RemoveRange(0, 0x04);
                                break;
                        }

                        //Byte swap the main save data.
                        toSave = MainSave.ByteSwapMain(toSave.ToArray()).ToList();

                        //Correct the checksum for the main save data.
                        toSave = new List<byte>(Checksum.MainChecksumCorrected(toSave.ToArray(), 0));

                        //Get a valid save file name.
                        pcFileName = Path.GetDirectoryName(loadedFile) + @"\SONIC2B__S01";
                        index = 1;
                        while (true)
                        {
                            if (!File.Exists(pcFileName))
                            {
                                break;
                            }
                            else
                            {
                                pcFileName = Path.GetDirectoryName(loadedFile) + @"\SONIC2B__S" + index.ToString("00");
                                index++;
                            }
                        }

                        //Write the converted data to the save file name.
                        File.WriteAllBytes(pcFileName, toSave.ToArray());
                        MetroMessageBox.Show(this, "Save file has been saved to " + pcFileName + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                        break;

                    case 1: //Chao
                        List<byte> byteList = new List<byte>();
                        //Add the data before the chao to the byte list.
                        byteList.AddRange(loadedSave.Take(0x3AA4).ToArray());

                        //Byte swap the chao data and add it back to the byte list.
                        foreach (byte[] chao in SplitByteArray(loadedSave.Skip(0x3AA4).Take(0xC000).ToArray(), 0x800))
                        {
                            byteList.AddRange(ChaoSave.ByteSwapChao(chao));
                        }

                        //Add the remaining data to the byte list.
                        byteList.AddRange(loadedSave.Skip(0xFAA4).Take(0x55C).ToArray());

                        //Byte swap the chao world data.
                        byteList = ChaoSave.ByteSwapChaoWorld(byteList.ToArray()).ToList();

                        //Convert the byte list to a byte array.
                        byte[] chaoToSave = byteList.ToArray();

                        //Split the save and apply the checksum correction.
                        byte[] splitForChecksum = chaoToSave.Skip(0x3040).ToArray();
                        Checksum.WriteChaoChecksum(splitForChecksum, true);

                        //Create a new byte list and create the final save.
                        List<byte> byteArray = new List<byte>();
                        byteArray.AddRange(chaoToSave.Take(0x3040).ToArray());
                        byteArray.AddRange(splitForChecksum);
                        chaoToSave = byteArray.ToArray();

                        //Get a valid save file name.
                        pcFileName = Path.GetDirectoryName(loadedFile) + @"\SONIC2B__ALF";
                        index = 1;
                        while (true)
                        {
                            if (!File.Exists(pcFileName))
                            {
                                break;
                            }
                            else
                            {
                                pcFileName = Path.GetDirectoryName(loadedFile) + @"\SONIC2B__ALF" + index;
                                index++;
                            }
                        }
                        //Write the converted data to the save file name.
                        File.WriteAllBytes(pcFileName, chaoToSave);
                        MetroMessageBox.Show(this, "Chao save file has been saved to " + pcFileName + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                        break;
                }
            }
        }

        private void btn_ToGCN_Click(object sender, EventArgs e)
        {
            string gcnFileName;
            int index;
            int gcnRegion;
            //Converting to GCN so only continue if we're not already a GCN save.
            if (saveConsole != 1)
            {
                switch (saveType)
                {
                    case 0: //Main
                        //Get the GCN file number.
                        GCNFileNo.ShowDialog();

                        int gcnFileNo = GCNFileNoAndRegion.FileNo;
                        gcnRegion = GCNFileNoAndRegion.GameRegion;

                        //If the GCN file number is valid, continue.
                        if (gcnFileNo != 100 && gcnFileNo != 255)
                        {
                            //Create the byte list we're going to save.
                            List<byte> toSave = new List<byte>();
                            //Byte swap if the save is converting from PC.

                            switch (saveConsole)
                            {
                                case 0: //PC
                                    toSave = MainSave.GCTextLanguageRemovedFix(loadedSave, true).ToList();
                                    toSave = MainSave.ByteSwapMain(toSave.ToArray()).ToList();
                                    break;
                                case 2: //PS3
                                    toSave = SplitByteArray(loadedSave, 0x6008)[selectedSlot].ToList();
                                    toSave.RemoveRange(0, 0x08);
                                    toSave = MainSave.GCTextLanguageRemovedFix(toSave.ToArray(), true).ToList();
                                    break;
                                case 3: //360
                                    toSave = SplitByteArray(loadedSave, 0x6004)[selectedSlot].ToList();
                                    toSave.RemoveRange(0, 0x04);
                                    toSave = MainSave.GCTextLanguageRemovedFix(toSave.ToArray(), true).ToList();
                                    break;
                            }
                            //Correct the Checksum of the converted save.
                            toSave = new List<byte>(Checksum.MainChecksumCorrected(toSave.ToArray(), 1));

                            //Insert headers.
                            toSave.InsertRange(0x00, MainSave.GCNMainHeader1);
                            if (gcnRegion == 1)
                            {
                                toSave.RemoveAt(0x03);
                                toSave.Insert(0x03, 0x50);
                            }
                            toSave.RemoveRange(0x80, 0x2800);
                            toSave.InsertRange(0x80, MainSave.GCNMainHeader2);
                            //Convert the int to string then to bytes and insert them.
                            string gcnFileNoString = gcnFileNo.ToString("00");
                            byte[] gcnFileNoBytes = Encoding.UTF8.GetBytes(gcnFileNoString);
                            toSave.RemoveRange(0x12, 0x02);
                            toSave.InsertRange(0x12, gcnFileNoBytes);

                            //Find a valid filename.
                            gcnFileName = Path.GetDirectoryName(loadedFile) + @"\SA2MAIN" + gcnFileNoString + ".gci";
                            index = 1;
                            while (true)
                            {
                                if (!File.Exists(gcnFileName))
                                {
                                    break;
                                }
                                else
                                {
                                    gcnFileName = Path.GetDirectoryName(loadedFile) + @"\SA2MAIN" + gcnFileNoString + index.ToString("00") + ".gci";
                                    index++;
                                }
                            }
                            //Write the converted save to the file name.
                            File.WriteAllBytes(gcnFileName, toSave.ToArray());
                            MetroMessageBox.Show(this, "Save file has been saved to " + gcnFileName + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                        break;
                    case 1: //Chao
                        
                        //Get the desired region
                        GCNRegion.ShowDialog();
                        gcnRegion = GCNRegion.GameRegion;

                        List<byte> byteList = new List<byte>();
                        List<byte[]> chaoList = new List<byte[]>();
                        //Add the beginning chao data to the byte list.
                        byteList.AddRange(loadedSave.Take(0x3AA4).ToArray());
                        //Split the chao list from the chao data.
                        chaoList = SplitByteArray(loadedSave.Skip(0x3AA4).Take(0xC000).ToArray(), 0x800);

                        //Byte swap all individual chao and add to byte list.
                        foreach (byte[] chao in chaoList)
                        {
                            if (saveConsole == 1) { byteList.AddRange(ChaoSave.ByteSwapChao(chao)); }
                            else { byteList.AddRange(chao); }
                        }

                        //Add the remaining data to the byte list.
                        byteList.AddRange(loadedSave.Skip(0xFAA4).Take(0x55C).ToArray());

                        //Byte swap if we're converting from PC.
                        if (saveConsole == 1) { byteList = ChaoSave.ByteSwapChaoWorld(byteList.ToArray()).ToList(); }

                        //Insert headers to byte list.
                        byteList.InsertRange(0x00, ChaoSave.GCNChaoHeader1);
                        if (gcnRegion == 1) 
                        {
                            byteList.RemoveAt(0x03);
                            byteList.Insert(0x03, 0x50);
                        }
                        byteList.RemoveRange(0x80, 0x3000);
                        byteList.InsertRange(0x80, ChaoSave.GCNChaoHeader2);

                        //Split the save for checksum calculation and re-combine it.
                        byte[] splitForChecksum = byteList.Skip(0x3080).ToArray();
                        Checksum.WriteChaoChecksum(splitForChecksum, true);
                        byteList.RemoveRange(0x3080, byteList.Count - 0x3080);
                        byteList.AddRange(splitForChecksum.ToList());

                        //Get a valid save name.
                        string gcFileName = Path.GetDirectoryName(loadedFile) + @"\SA2CHAO.gci";
                        index = 1;
                        while (true)
                        {
                            if (!File.Exists(gcFileName))
                            {
                                break;
                            }
                            else
                            {
                                gcFileName = Path.GetDirectoryName(loadedFile) + @"\SA2CHAO" + index + ".gci";
                                index++;
                            }
                        }
                        //Save the file to the save name.
                        File.WriteAllBytes(gcFileName, byteList.ToArray());
                        MetroMessageBox.Show(this, "Chao save file has been saved to " + gcFileName + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                        break;
                }
            }
        }

        private void btn_To360_Click(object sender, EventArgs e)
        {
            if (saveType == 0) //Main Save
            {
                DialogResult result = MetroMessageBox.Show(this, "Would you like to append to an existing 360 save?", "Append or fresh save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //If it's a fresh save
                if (result == DialogResult.No)
                {
                    List<byte> toSave = new List<byte>();

                    switch (saveConsole)
                    {
                        case 0: //PC
                            toSave = MainSave.ByteSwapMain(loadedSave).ToList();
                            break;
                        case 1: //GC
                            toSave = MainSave.GCTextLanguageRemovedFix(loadedSave, false).ToList();
                            break;
                        case 2: //PS3
                            toSave = SplitByteArray(loadedSave, 0x6008)[selectedSlot].ToList();
                            toSave.RemoveRange(0, 0x08);
                            break;
                    }

                    //Write Checksum
                    toSave = Checksum.MainChecksumCorrected(toSave.ToArray(), 2).ToList();

                    //Add 360 Header stuff
                    toSave.InsertRange(0, new byte[] { 0x00, 0x00, 0x00, 0x01 });

                    //Pad save to desired size
                    for (int i = toSave.Count; i < 0x3C028; i++)
                    {
                        toSave.Add(0x00);
                    }
                    //Get a valid filename
                    string consoleFileName = Path.GetDirectoryName(loadedFile) + @"\savedata.bin";
                    int index = 1;
                    while (true)
                    {
                        if (!File.Exists(consoleFileName))
                        {
                            break;
                        }
                        else
                        {
                            consoleFileName = Path.GetDirectoryName(loadedFile) + @"\savedata" + index + ".bin";
                            index++;
                        }
                    }
                    //Write to filename
                    File.WriteAllBytes(consoleFileName, toSave.ToArray());
                    MetroMessageBox.Show(this, "Save file has been saved to " + consoleFileName + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    OpenFileDialog loadToAppend = new OpenFileDialog();
                    loadToAppend.Filter = "360 Main Save|*.bin";
                    loadToAppend.Title = "Choose a file to add this save to";
                    loadToAppend.ShowDialog();
                    if (loadToAppend.FileName != "")
                    {
                        //Load file into array
                        byte[] save = File.ReadAllBytes(loadToAppend.FileName);
                        //Ensure the save is correct length for 360 save
                        if (save.Length == 0x3C028)
                        {
                            //Check for a free slot in the save
                            int slotIndex = 0;
                            int slotNotTaken = 0;
                            List<int> takenSlots = new List<int>();
                            foreach (byte[] saveSlot in SplitByteArray(save, 0x6004)) { takenSlots.Add(saveSlot[3]); }

                            for (int i = 1; i < 10; i++)
                            {
                                if (!takenSlots.Contains(i)) { slotNotTaken = i; break; }
                            }
                            if (slotNotTaken != 0)
                            {
                                List<byte> combinedSave = new List<byte>();
                                foreach (byte[] saveSlot in SplitByteArray(save, 0x6004))
                                {
                                    if (saveSlot[3] == 0x00)
                                    {
                                        List<byte> toSave = new List<byte>();

                                        switch (saveConsole)
                                        {
                                            case 0: //PC
                                                toSave = MainSave.ByteSwapMain(loadedSave).ToList();
                                                break;
                                            case 1: //GC
                                                toSave = MainSave.GCTextLanguageRemovedFix(loadedSave, false).ToList();
                                                break;
                                            case 2: //PS3
                                                toSave = SplitByteArray(loadedSave, 0x6008)[selectedSlot].ToList();
                                                toSave.RemoveRange(0, 0x08);
                                                break;
                                        }

                                        //Write Checksum
                                        toSave = Checksum.MainChecksumCorrected(toSave.ToArray(), 2).ToList();

                                        //Add 360 Header stuff
                                        toSave.InsertRange(0, new byte[] { 0x00, 0x00, 0x00, (byte)slotNotTaken });

                                        //Combine save into the final product.
                                        combinedSave.AddRange(save.Take(0x6004 * slotIndex));
                                        combinedSave.AddRange(toSave);
                                        combinedSave.AddRange(save.Skip(0x6004 * (slotIndex + 1)));
                                        break;
                                    }
                                    slotIndex++;
                                }
                                string consoleFileName = Path.GetDirectoryName(loadToAppend.FileName) + @"\savedata.bin";
                                int index = 1;
                                while (true)
                                {
                                    if (!File.Exists(consoleFileName))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        consoleFileName = Path.GetDirectoryName(loadToAppend.FileName) + @"\savedata" + index + ".bin";
                                        index++;
                                    }
                                }
                                File.WriteAllBytes(consoleFileName, combinedSave.ToArray());
                                MetroMessageBox.Show(this, "Save file has been saved to " + consoleFileName + ", in slot " + slotNotTaken + ".", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                            }
                            else
                            {
                                MetroMessageBox.Show(this, "Couldn't find a slot to save to!", "Error writing save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "That doesn't appear to be a 360 main save.", "Error writing save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else //Chao Save
            {
                List<byte> byteList = new List<byte>();
                //Add header
                byteList.AddRange(loadedSave.Take(0x3AA4).ToArray());
                foreach (byte[] chao in SplitByteArray(loadedSave.Skip(0x3AA4).Take(0xC000).ToArray(), 0x800))
                {
                    //Byte swap the chao if we're converting from PC
                    if (saveConsole == 0) { byteList.AddRange(ChaoSave.ByteSwapChao(chao)); }
                    else { byteList.AddRange(chao); }
                }
                //Add remaining data
                byteList.AddRange(loadedSave.Skip(0xFAA4).Take(0x55C).ToArray());

                //Byte swap chao world data if we're converting from PC
                if (saveConsole == 0) { byteList = ChaoSave.ByteSwapChaoWorld(byteList.ToArray()).ToList(); }

                byte[] chaoToSave = byteList.ToArray();

                //Correct checksum
                byte[] splitForChecksum = chaoToSave.Skip(0x3040).ToArray();
                Checksum.WriteChaoChecksum(splitForChecksum, true);
                List<byte> byteArray = new List<byte>();
                byteArray.AddRange(chaoToSave.Take(0x3040).ToArray());
                byteArray.AddRange(splitForChecksum);
                chaoToSave = byteArray.ToArray();
                //Get a valid filename
                string consoleFileName = Path.GetDirectoryName(loadedFile) + @"\savedata.bin";
                int index = 1;
                while (true)
                {
                    if (!File.Exists(consoleFileName))
                    {
                        break;
                    }
                    else
                    {
                        consoleFileName = Path.GetDirectoryName(loadedFile) + @"\savedata" + index + ".bin";
                        index++;
                    }
                }
                //Write to filename
                File.WriteAllBytes(consoleFileName, chaoToSave);
                MetroMessageBox.Show(this, "Chao save file has been saved to " + consoleFileName + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }
        private void btn_ToPS3_Click(object sender, EventArgs e)
        {
            if (saveType == 0) //Main Save
            {
                DialogResult result = MetroMessageBox.Show(this, "Would you like to append to an existing PS3 save?", "Append or fresh save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //If it's a fresh save
                if (result == DialogResult.No)
                {
                    List<byte> toSave = new List<byte>();

                    switch (saveConsole)
                    {
                        case 0: //PC
                            toSave = MainSave.ByteSwapMain(loadedSave).ToList();
                            break;
                        case 1: //GC
                            toSave = MainSave.GCTextLanguageRemovedFix(loadedSave, false).ToList();
                            break;
                        case 3: //360
                            toSave = SplitByteArray(loadedSave, 0x6004)[selectedSlot].ToList();
                            toSave.RemoveRange(0, 0x04);
                            break;
                    }

                    //Write Checksum
                    toSave = Checksum.MainChecksumCorrected(toSave.ToArray(), 2).ToList();

                    //Add PS3 Header stuff
                    toSave.InsertRange(0, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00 });

                    //Pad save to desired size
                    for (int i = toSave.Count; i < 0x3C050; i++)
                    {
                        toSave.Add(0x00);
                    }
                    //Get a valid filename
                    string consoleFileName = Path.GetDirectoryName(loadedFile) + @"\SA2SAVE";
                    int index = 1;
                    while (true)
                    {
                        if (!File.Exists(consoleFileName))
                        {
                            break;
                        }
                        else
                        {
                            consoleFileName = Path.GetDirectoryName(loadedFile) + @"\SA2SAVE" + index;
                            index++;
                        }
                    }
                    //Write to filename
                    File.WriteAllBytes(consoleFileName, toSave.ToArray());
                    MetroMessageBox.Show(this, "Save file has been saved to " + consoleFileName + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    OpenFileDialog loadToAppend = new OpenFileDialog();
                    loadToAppend.Filter = "PS3 Main Save|*.*";
                    loadToAppend.Title = "Choose a file to add this save to";
                    loadToAppend.ShowDialog();
                    if (loadToAppend.FileName != "")
                    {
                        //Load file into array
                        byte[] save = File.ReadAllBytes(loadToAppend.FileName);
                        //Ensure the save is correct length for PS3 save
                        if (save.Length == 0x3C050)
                        {
                            //Check for a free slot in the save
                            int slotIndex = 0;
                            int slotNotTaken = 0;
                            List<int> takenSlots = new List<int>();
                            foreach (byte[] saveSlot in SplitByteArray(save, 0x6008)) { takenSlots.Add(saveSlot[3]); }

                            for (int i = 1; i < 10; i++)
                            {
                                if (!takenSlots.Contains(i)) { slotNotTaken = i; break; }
                            }
                            if (slotNotTaken != 0)
                            {
                                List<byte> combinedSave = new List<byte>();
                                foreach (byte[] saveSlot in SplitByteArray(save, 0x6008))
                                {
                                    if (saveSlot[3] == 0x00)
                                    {
                                        List<byte> toSave = new List<byte>();

                                        switch (saveConsole)
                                        {
                                            case 0: //PC
                                                toSave = MainSave.ByteSwapMain(loadedSave).ToList();
                                                break;
                                            case 1: //GC
                                                toSave = MainSave.GCTextLanguageRemovedFix(loadedSave, false).ToList();
                                                break;
                                            case 3: //360
                                                toSave = SplitByteArray(loadedSave, 0x6004)[selectedSlot].ToList();
                                                toSave.RemoveRange(0, 0x04);
                                                break;
                                        }

                                        //Write Checksum
                                        toSave = Checksum.MainChecksumCorrected(toSave.ToArray(), 2).ToList();

                                        //Add PS3 Header stuff
                                        toSave.InsertRange(0, new byte[] { 0x00, 0x00, 0x00, (byte)slotNotTaken, 0x00, 0x00, 0x00, 0x00 });

                                        //Combine save into the final product.
                                        combinedSave.AddRange(save.Take(0x6008 * slotIndex));
                                        combinedSave.AddRange(toSave);
                                        combinedSave.AddRange(save.Skip(0x6008 * (slotIndex + 1)));
                                        break;
                                    }
                                    slotIndex++;
                                }
                                string consoleFileName = Path.GetDirectoryName(loadToAppend.FileName) + @"\SA2SAVE";
                                int index = 1;
                                while (true)
                                {
                                    if (!File.Exists(consoleFileName))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        consoleFileName = Path.GetDirectoryName(loadToAppend.FileName) + @"\SA2SAVE" + index;
                                        index++;
                                    }
                                }
                                File.WriteAllBytes(consoleFileName, combinedSave.ToArray());
                                MetroMessageBox.Show(this, "Save file has been saved to " + consoleFileName + ", in slot " + slotNotTaken + ".", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                            }
                            else
                            {
                                MetroMessageBox.Show(this, "Couldn't find a slot to save to!", "Error writing save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "That doesn't appear to be a PS3 main save.", "Error writing save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else //Chao Save
            {
                List<byte> byteList = new List<byte>();
                //Add header
                byteList.AddRange(loadedSave.Take(0x3AA4).ToArray());
                foreach (byte[] chao in SplitByteArray(loadedSave.Skip(0x3AA4).Take(0xC000).ToArray(), 0x800))
                {
                    //Byte swap the chao if we're converting from PC
                    if (saveConsole == 0) { byteList.AddRange(ChaoSave.ByteSwapChao(chao)); }
                    else { byteList.AddRange(chao); }
                }
                //Add remaining data
                byteList.AddRange(loadedSave.Skip(0xFAA4).Take(0x55C).ToArray());

                //Byte swap chao world data if we're converting from PC
                if (saveConsole == 0) { byteList = ChaoSave.ByteSwapChaoWorld(byteList.ToArray()).ToList(); }

                byte[] chaoToSave = byteList.ToArray();

                //Correct checksum
                byte[] splitForChecksum = chaoToSave.Skip(0x3040).ToArray();
                Checksum.WriteChaoChecksum(splitForChecksum, true);
                List<byte> byteArray = new List<byte>();
                byteArray.AddRange(chaoToSave.Take(0x3040).ToArray());
                byteArray.AddRange(splitForChecksum);
                chaoToSave = byteArray.ToArray();
                //Get a valid filename
                string consoleFileName = Path.GetDirectoryName(loadedFile) + @"\CHAOSAVE";
                int index = 1;
                while (true)
                {
                    if (!File.Exists(consoleFileName))
                    {
                        break;
                    }
                    else
                    {
                        consoleFileName = Path.GetDirectoryName(loadedFile) + @"\CHAOSAVE" + index;
                        index++;
                    }
                }
                //Write to filename
                File.WriteAllBytes(consoleFileName, chaoToSave);
                MetroMessageBox.Show(this, "Chao save file has been saved to " + consoleFileName + "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void btn_GitHub_Click(object sender, EventArgs e)
        {
            DialogResult result = MetroMessageBox.Show(this, "Would you like to visit the GitHub page?", "GitHub", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("https://github.com/dfrood/SA2-Save-Converter");
            }
        }
    }
}
