using System.Text;

namespace ClipboardToolForBakin
{
    public class BakinPanelData
    {
        public class RowData
        {
            public RowData()
            {
                this.Tag = "Talk";
                this.Text = "";
                this.NPL = "";
                this.NPC = "";
                this.NPR = "";
                this.blspd = 0;
                this.blrate = 0;
                this.lipspd = 0f;
                this.Cast1 = "";
                this.ActCast1 = "";
                this.Cast2 = "";
                this.ActCast2 = "";
                this.TalkCast = "1";
                this.MirrorCast1 = false;
                this.MirrorCast2 = false;
                this.Billboard1 = false;
                this.Billboard2 = false;
                this.WindowVisible = false;
                this.WindowPosition = "Down";
                this.SpeechBubble = "";
                this.UseMapLight = false;
                this.Memo = "";
            }

            public string Tag { get; set; }
            public string Text { get; set; }
            public string NPL { get; set; }
            public string NPC { get; set; }
            public string NPR { get; set; }
            public int blspd { get; set; }
            public int blrate { get; set; }
            public float lipspd { get; set; }
            public string Cast1 { get; set; }
            public string ActCast1 { get; set; }
            public string Cast2 { get; set; }
            public string ActCast2 { get; set; }
            public string TalkCast { get; set; }
            public bool MirrorCast1 { get; set; }
            public bool MirrorCast2 { get; set; }
            public bool Billboard1 { get; set; }
            public bool Billboard2 { get; set; }
            public bool WindowVisible { get; set; }
            public string WindowPosition { get; set; }
            public string SpeechBubble { get; set; }
            public bool UseMapLight { get; set; }
            public string Memo { get; set; }
        }

        public static void SetClipboardDataWithCustomFormat(IEnumerable<byte[]> formattedDataList)
        {
            var dataObject = new DataObject();
            foreach (var formattedData in formattedDataList)
            {
                var stream = new MemoryStream(formattedData);
                dataObject.SetData("Yukar2ScriptCommands", stream);
            }
            Clipboard.SetDataObject(dataObject, true);
        }

        public static void SetClipBoard(IEnumerable<BakinPanelData.RowData> data)
        {
            MemoryStream memoryStream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(memoryStream))
            {
                writer.Write((uint)data.Count());
                writer.Write(0x00000000);

                foreach (var rowData in data)
                {
                    switch (rowData.Tag)
                    {
                        case "Talk":
                            writer.Write(0x0000002b);
                            writer.Write(0x00000000);
                            if (rowData.WindowPosition == "Bubble(Event)")
                            {
                                writer.Write(0x02010203);
                            }
                            else
                            {
                                writer.Write(0x02010103);
                            }
                            writer.Write(0x01030203);
                            writer.Write(0x01010101);
                            writer.Write((byte)0x01);
                            writer.Write((byte)0x00);

                            int textLength = Encoding.UTF8.GetByteCount(rowData.Text);
                            int multiple = textLength / 0x80;
                            if (multiple == 0)
                            {
                                writer.Write((byte)textLength);
                            }
                            else
                            {
                                writer.Write((byte)(textLength % 0x80 + 0x80));
                                writer.Write((byte)multiple);
                            }

                            writer.Write(Encoding.UTF8.GetBytes(rowData.Text));

                            switch (rowData.WindowPosition)
                            {
                                case "Up":
                                    writer.Write(0x00000000);
                                    break;
                                case "Center":
                                    writer.Write(0x00000001);
                                    break;
                                case "Bubble(Player)":
                                    writer.Write(0x00001000);
                                    break;
                                case "Bubble(ThisEvent)":
                                    writer.Write(0x00001001);
                                    break;
                                case "Bubble(Member2)":
                                    writer.Write(0x00001002);
                                    break;
                                case "Bubble(Member3)":
                                    writer.Write(0x00001003);
                                    break;
                                case "Bubble(Member4)":
                                    writer.Write(0x00001004);
                                    break;
                                case "Bubble(Event)":
                                    byte[] bubbleBytes = FormattedHexStringToBinary(rowData.SpeechBubble);
                                    writer.Write(bubbleBytes);
                                    break;
                                case "Down":
                                default:
                                    writer.Write(0x00000002);
                                    break;
                            }
                            writer.Write(0x00000000);
                           
                            if (rowData.Cast1 == "00000000-00000000-00000000-00000000")
                            {
                                writer.Write(0x00000000);
                                writer.Write(0x00000000);
                                writer.Write(0x00000000);
                                writer.Write(0x00000000);
                                writer.Write((byte)0x00);
                            }
                            else
                            {
                                byte[] castBytes = FormattedHexStringToBinary(rowData.Cast1);
                                writer.Write(castBytes);
                                byte[] actCastBytes = Encoding.ASCII.GetBytes(rowData.ActCast1);
                                writer.Write((byte)actCastBytes.Length);
                                writer.Write(actCastBytes);
                            }
                            if (rowData.Cast2 == "00000000-00000000-00000000-00000000")
                            {
                                writer.Write(0x00000000);
                                writer.Write(0x00000000);
                                writer.Write(0x00000000);
                                writer.Write(0x00000000);
                                writer.Write((byte)0x00);
                            }
                            else
                            {
                                byte[] cast2Bytes = FormattedHexStringToBinary(rowData.Cast2);
                                writer.Write(cast2Bytes);
                                byte[] actCast2Bytes = Encoding.ASCII.GetBytes(rowData.ActCast2);
                                writer.Write((byte)actCast2Bytes.Length);
                                writer.Write(actCast2Bytes);
                            }
                            if (rowData.TalkCast == "1")
                            {
                                writer.Write(0x00000000);
                            }
                            else
                            {
                                writer.Write(0x00000001);
                            }
                            if (rowData.MirrorCast1 == true)
                            {
                                writer.Write(0x00000001);
                            }
                            else
                            {
                                writer.Write(0x00000000);
                            }
                            if (rowData.MirrorCast2 == true)
                            {
                                writer.Write(0x00000001);
                            }
                            else
                            {
                                writer.Write(0x00000000);
                            }
                            if (rowData.UseMapLight == true)
                            {
                                writer.Write(0x00000001);
                            }
                            else
                            {
                                writer.Write(0x00000000);
                            }
                            writer.Write(0x00000000);
                            writer.Write(0x00000000);
                            break;

                        case "Message":
                            writer.Write(0x0000001D);
                            writer.Write(0x00000000);
                            if (rowData.WindowPosition == "Bubble(Event)")
                            {
                                writer.Write(0x00010203);
                            }
                            else
                            {
                                writer.Write(0x00010103);
                            }

                            textLength = Encoding.UTF8.GetByteCount(rowData.Text);
                            multiple = textLength / 0x80;
                            if (multiple == 0)
                            {
                                writer.Write((byte)textLength);
                            }
                            else
                            {
                                writer.Write((byte)(textLength % 0x80 + 0x80));
                                writer.Write((byte)multiple);
                            }
                            writer.Write(Encoding.UTF8.GetBytes(rowData.Text));

                            switch (rowData.WindowPosition)
                            {
                                case "Up":
                                    writer.Write(0x00000000);
                                    break;
                                case "Center":
                                    writer.Write(0x00000001);
                                    break;
                                case "Bubble(Player)":
                                    writer.Write(0x00001000);
                                    break;
                                case "Bubble(ThisEvent)":
                                    writer.Write(0x00001001);
                                    break;
                                case "Bubble(Member2)":
                                    writer.Write(0x00001002);
                                    break;
                                case "Bubble(Member3)":
                                    writer.Write(0x00001003);
                                    break;
                                case "Bubble(Member4)":
                                    writer.Write(0x00001004);
                                    break;
                                case "Bubble(Event)":
                                    byte[] bubbleBytes = FormattedHexStringToBinary(rowData.SpeechBubble);
                                    writer.Write(bubbleBytes);
                                    break;
                                case "Down":
                                default:
                                    writer.Write(0x00000002);
                                    break;
                            }
                            if (rowData.WindowVisible == true)
                            {
                                writer.Write(0x00000001);
                            }
                            else
                            {
                                writer.Write(0x00000000);
                            }
                            break;

                        case "Notes":
                        default:
                            writer.Write(0x0000007E);
                            writer.Write(0x00000000);
                            writer.Write((byte)0x03);
                            writer.Write((byte)0x00);
                            textLength = Encoding.UTF8.GetByteCount(rowData.Text);
                            multiple = textLength / 0x80;
                            if (multiple == 0)
                            {
                                writer.Write((byte)textLength);
                            }
                            else
                            {
                                writer.Write((byte)(textLength % 0x80 + 0x80));
                                writer.Write((byte)multiple);
                            }
                            writer.Write(Encoding.UTF8.GetBytes(rowData.Text));
                            break;
                    }
                }
            }
            byte[] formattedData = memoryStream.ToArray();
            SetClipboardDataWithCustomFormat(new List<byte[]> { formattedData });
        }

        public static IEnumerable<BakinPanelData.RowData> GetClipBoardData()
        {
            int textLength;
            uint windowPositionId;
            byte[] clipboardData = GetClipboardDataWithCustomFormat();
            List<BakinPanelData.RowData> data = new List<BakinPanelData.RowData>();

            using (MemoryStream memoryStream = new MemoryStream(clipboardData))
            {
                using (BinaryReader reader = new BinaryReader(memoryStream))
                {
                    uint count = reader.ReadUInt32();
                    uint zero = reader.ReadUInt32();  // Not used

                    for (int i = 0; i < count; i++)
                    {
                        if (reader.BaseStream.Position >= reader.BaseStream.Length)
                        {
                            break;
                        }

                        BakinPanelData.RowData rowData = new BakinPanelData.RowData();
                        uint tagId = reader.ReadUInt32();

                        switch (tagId)
                        {
                            case 0x0000002B:
                            case 0x0000001D:
                                if (tagId == 0x0000002B)
                                {
                                    rowData.Tag = "Talk";
                                    reader.ReadUInt32();
                                    reader.ReadUInt32();
                                    reader.ReadUInt32();
                                    reader.ReadUInt32();
                                    reader.ReadByte();
                                    reader.ReadByte();

                                    textLength = reader.ReadByte();
                                    if (textLength > 0x7F)
                                    {
                                        byte temp = reader.ReadByte();
                                        textLength = textLength + 128 * (temp - 1);
                                    }
                                    rowData.Text = Encoding.UTF8.GetString(reader.ReadBytes(textLength));
                                }
                                else
                                {
                                    rowData.Tag = "Message";
                                    reader.ReadUInt32();
                                    reader.ReadUInt32();

                                    textLength = reader.ReadByte();
                                    if (textLength > 0x7F)
                                    {
                                        byte temp = reader.ReadByte();
                                        textLength = textLength + 128 * (temp - 1);
                                    }
                                    rowData.Text = Encoding.UTF8.GetString(reader.ReadBytes(textLength));
                                }

                                windowPositionId = reader.ReadUInt32();
                                rowData.WindowPosition = windowPositionId switch
                                {
                                    0x00000000 => "Up",
                                    0x00000001 => "Center",
                                    0x00000002 => "Down",
                                    0x00001000 => "Bubble(Player)",
                                    0x00001001 => "Bubble(ThisEvent)",
                                    0x00001002 => "Bubble(Member2)",
                                    0x00001003 => "Bubble(Member3)",
                                    0x00001004 => "Bubble(Member4)",
                                    _ => "Bubble(Event)"
                                };
                                if (rowData.WindowPosition == "Bubble(Event)")
                                {
                                    byte[] windowPositionBytes = BitConverter.GetBytes(windowPositionId);
                                    byte[] bubbleBytes = reader.ReadBytes(12);
                                    byte[] combinedBytes = new byte[windowPositionBytes.Length + bubbleBytes.Length];
                                    Buffer.BlockCopy(windowPositionBytes, 0, combinedBytes, 0, windowPositionBytes.Length);
                                    Buffer.BlockCopy(bubbleBytes, 0, combinedBytes, windowPositionBytes.Length, bubbleBytes.Length);
                                    string hexData = BinaryToFormattedHexString(combinedBytes);
                                    rowData.SpeechBubble = hexData;
                                }

                                uint windowVisible = reader.ReadUInt32();
                                if (windowVisible == 0x00000001)
                                {
                                    rowData.WindowVisible = true;
                                }
                                else
                                {
                                    rowData.WindowVisible = false;
                                }

                                if (tagId == 0x0000002B)
                                {
                                    byte[] cast1Bytes = reader.ReadBytes(16);
                                    string cast1 = Encoding.ASCII.GetString(cast1Bytes).TrimEnd('\0');
                                    if (cast1.Length == 0)
                                    {
                                        rowData.Cast1 = "00000000-00000000-00000000-00000000";
                                        rowData.ActCast1 = "----";
                                        byte actCast1Length = reader.ReadByte();
                                    }
                                    else
                                    {
                                        string hexData = BinaryToFormattedHexString(cast1Bytes);
                                        rowData.Cast1 = hexData;

                                        byte actCast1Length = reader.ReadByte();
                                        rowData.ActCast1 = Encoding.ASCII.GetString(reader.ReadBytes(actCast1Length));
                                    }

                                    byte[] cast2Bytes = reader.ReadBytes(16);
                                    string cast2 = Encoding.ASCII.GetString(cast2Bytes).TrimEnd('\0');
                                    if (cast2.Length == 0)
                                    {
                                        rowData.Cast2 = "00000000-00000000-00000000-00000000";
                                        rowData.ActCast2 = "----";
                                        byte actCast1Length = reader.ReadByte();
                                    }
                                    else
                                    {
                                        string hexData = BinaryToFormattedHexString(cast2Bytes);
                                        rowData.Cast2 = hexData;

                                        byte actCast2Length = reader.ReadByte();
                                        rowData.ActCast2 = Encoding.ASCII.GetString(reader.ReadBytes(actCast2Length));
                                    }

                                    rowData.TalkCast = reader.ReadUInt32() == 0x00000000 ? "1" : "2";
                                    rowData.MirrorCast1 = reader.ReadUInt32() == 0x00000001;
                                    rowData.MirrorCast2 = reader.ReadUInt32() == 0x00000001;
                                    rowData.UseMapLight = reader.ReadUInt32() == 0x00000001;

                                    reader.ReadUInt32();
                                    reader.ReadUInt32();
                                }
                                break;

                            case 0x0000007E:
                                rowData.Tag = "Notes";
                                reader.ReadUInt32();
                                reader.ReadByte();
                                reader.ReadByte();

                                textLength = reader.ReadByte();
                                if (textLength > 0x7F)
                                {
                                    byte temp = reader.ReadByte();
                                    textLength = textLength + 128 * (temp - 1);
                                }
                                rowData.Text = Encoding.UTF8.GetString(reader.ReadBytes(textLength));
                                break;

                            default:
                                // not for support
                                //return data;
                                // This is only a temporary process. Cases have been confirmed where it does not work.
                                // Read bytes until we encounter a pattern that begins with 0x000000XX
                                List<byte> buffer = new List<byte>();
                                byte[] expectedPattern2B = new byte[] { 0x2B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                                byte[] expectedPattern1D = new byte[] { 0x1D, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                                byte[] expectedPattern7E = new byte[] { 0x7E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                                while (reader.BaseStream.Position < reader.BaseStream.Length)
                                {
                                    buffer.Add(reader.ReadByte());
                                    if (buffer.Count >= 8)
                                    {
                                        byte[] lastEightBytes = buffer.Skip(buffer.Count - 8).Take(8).ToArray();
                                        if (lastEightBytes.SequenceEqual(expectedPattern2B) || lastEightBytes.SequenceEqual(expectedPattern1D) || lastEightBytes.SequenceEqual(expectedPattern7E))
                                        {
                                            reader.BaseStream.Position -= 8;
                                            break;
                                        }
                                    }
                                }
                                continue;
                        }
                        data.Add(rowData);
                    }
                }
            }
            return data;
        }

        private static byte[] GetClipboardDataWithCustomFormat()
        {
            if (Clipboard.ContainsData("Yukar2ScriptCommands"))
            {
                var dataObject = Clipboard.GetDataObject();
                if (dataObject != null)
                {
                    var stream = (MemoryStream)dataObject.GetData("Yukar2ScriptCommands");
                    if (stream != null)
                    {
                        return stream.ToArray();
                    }
                }
            }
            return new byte[0];
        }

        public static string BinaryToHexString(byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", "");
        }

        public static byte[] HexStringToBinary(string hex)
        {
            int len = hex.Length;
            byte[] bytes = new byte[len / 2];
            for (int i = 0; i < len; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        public static string BinaryToFormattedHexString(byte[] data)
        {
            string hex = BinaryToHexString(data);
            return hex.Insert(8, "-").Insert(17, "-").Insert(26, "-");
        }

        public static byte[] FormattedHexStringToBinary(string formattedHex)
        {
            string hex = formattedHex.Replace("-", "");
            return HexStringToBinary(hex);
        }
    }
}
