using ClipboardToolForBakin;
using System.Windows.Forms;

namespace ClipboardToolForBakin2
{
    public partial class FormPreviewEditor : Form
    {
        public delegate void RowChangeRequestedHandler(object sender, PreviewEditorEventArgs e);
        public event RowChangeRequestedHandler RowChangeRequested;
        public delegate void RowAddRequestedHandler(object sender, AddRowEventArgs e);
        public event RowAddRequestedHandler RowAddRequested;
        public delegate void RowSwapRequestedHandler(object sender, SwapRowEventArgs e);
        public event RowSwapRequestedHandler RowSwapRequested;
        public delegate void CSVSaveRequestHandler(object sender, SaveCSVEventArgs e);
        public event CSVSaveRequestHandler CSVSaveRequested;
        public event EventHandler<BakinPanelData.RowData> DataChanged;
        private BakinPanelData.RowData _data;
        private List<ResourceItem> _comboBoxItems;
        private Dictionary<string, Image> imageCache = new Dictionary<string, Image>();
        private CustomRichTextBox prevCustomRichTextBoxText = new CustomRichTextBox
        {
            Font = new Font("Arial", 10, FontStyle.Regular),
            ForeColor = Color.Black,
            BackColor = SystemColors.Info,
            SelectionCharOffset = 0,
            ReadOnly = true,
            TabStop = false,
            Visible = true
        };
        private CancellationTokenSource? cancelTokenSource;
        private CancellationTokenSource? skipTokenSource;
        private bool mirrorPictureBoxCast1 = false;
        private bool mirrorPictureBoxCast2 = false;
        private ToolTip toolTipKeys = new ToolTip();

        public FormPreviewEditor(BakinPanelData.RowData rowData, List<ResourceItem> comboBoxItems)
        {
            InitializeComponent();
            CreateInputHelperContextMenu();
            _data = rowData;
            _comboBoxItems = comboBoxItems;
            UpdateComboBoxes();
            panelPreview.Controls.Add(prevCustomRichTextBoxText);
            PopulateFieldsWithData();

            toolTipKeys.SetToolTip(this.buttonDuplicateRow, "Duplicate row (Ctrl+Shift+D)");
            toolTipKeys.SetToolTip(this.buttonPreview, "Preview (Ctrl+Shift+P)");
            toolTipKeys.SetToolTip(this.buttonReload, "Reload (Ctrl+Shift+R)");
            toolTipKeys.SetToolTip(this.buttonStreamSkip, "Skip Stream (Ctrl+S)");
            toolTipKeys.SetToolTip(this.buttonNextRow, "Next row (Ctrl+→) or (Ctrl+↓)");
            toolTipKeys.SetToolTip(this.buttonPreviousRow, "Previous row (Ctrl+←) or (Ctrl+↑)");
            toolTipKeys.SetToolTip(this.buttonMoveUp, "Move up (Ctrl+Shift+↑)");
            toolTipKeys.SetToolTip(this.buttonMoveDown, "Move down (Ctrl+Shift+↓)");
            toolTipKeys.SetToolTip(this.radioButtonTalkCast1, "Talk cast1 (Ctrl+Shift+←)");
            toolTipKeys.SetToolTip(this.radioButtonTalkCast2, "Talk cast2 (Ctrl+Shift+→)");
            toolTipKeys.SetToolTip(this.buttonApply, "Apply (Ctrl+Enter)");
        }

        public void UpdateComboBoxItems(List<ResourceItem> comboBoxItems)
        {
            _comboBoxItems = comboBoxItems;
            UpdateComboBoxes();
        }

        private void UpdateComboBoxes()
        {
            UpdateComboBox(comboBoxCast1);
            UpdateComboBox(comboBoxCast2);
            UpdateComboBox(comboBoxSpeechBubble);
            AdjustComboBoxDropdownWidth(comboBoxCast1);
            AdjustComboBoxDropdownWidth(comboBoxCast2);
            AdjustComboBoxDropdownWidth(comboBoxSpeechBubble);
        }

        private void UpdateComboBox(ComboBox comboBox)
        {
            int cnt = 0;
            comboBox.Items.Clear();
            foreach (var item in _comboBoxItems)
            {
                if ((!string.IsNullOrEmpty(item.Key) && !string.IsNullOrEmpty(item.Value)) || cnt == 0 || cnt == 1)
                {
                    comboBox.Items.Add($"[{_comboBoxItems.IndexOf(item)}]:{item.Key}");
                    CacheImage(item.ImagePath);
                }
                cnt++;
            }
        }

        private void CacheImage(string filePath)
        {
            if (imageCache.ContainsKey(filePath)) return;

            if (!File.Exists(filePath)) return;

            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    imageCache[filePath] = new Bitmap(stream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error loading image from {filePath}: {e.Message}");
            }
        }

        public Image GetImageFromCache(string filePath)
        {
            if (imageCache.ContainsKey(filePath))
            {
                return imageCache[filePath];
            }
            return null;
        }

        public void UpdateSelectedData(BakinPanelData.RowData data)
        {
            _data = data;
        }

        public void UpdateData(BakinPanelData.RowData data)
        {
            int selectedCast1Index = comboBoxCast1.SelectedIndex;
            int selectedCast2Index = comboBoxCast2.SelectedIndex;
            int selectedSpeechBubbleIndex = comboBoxSpeechBubble.SelectedIndex;

            data.Tag = comboBoxTagType.SelectedItem?.ToString();
            data.Text = textBoxText.Text.Replace(Environment.NewLine, "\\n");
            data.NPL = textBoxNPL.Text;
            data.NPC = textBoxNPC.Text;
            data.NPR = textBoxNPR.Text;
            data.blspd = (int)numericUpDownBlspd.Value;
            data.blrate = (int)numericUpDownBlrate.Value;
            data.lipspd = (float)numericUpDownLipspd.Value;
            //data.Cast1 = comboBoxCast1.SelectedItem?.ToString();
            data.ActCast1 = textBoxActCast1.Text;
            //data.Cast2 = comboBoxCast2.SelectedItem?.ToString();
            data.ActCast2 = textBoxActCast2.Text;
            if (radioButtonTalkCast2.Checked)
            {
                data.TalkCast = "2";
            }
            else
            {
                data.TalkCast = "1";
            }
            data.MirrorCast1 = checkBoxMirrorCast1.Checked;
            data.MirrorCast2 = checkBoxMirrorCast2.Checked;
            data.Billboard1 = checkBoxBillboard1.Checked;
            data.Billboard2 = checkBoxBillboard2.Checked;
            data.WindowVisible = checkBoxWindowHidden.Checked;
            data.WindowPosition = comboBoxWindowPosition.SelectedItem?.ToString();
            //data.SpeechBubble = comboBoxSpeechBubble.SelectedItem?.ToString();
            data.UseMapLight = checkBoxUseMapLight.Checked;
            data.Memo = textBoxMemo.Text.Replace(Environment.NewLine, "\\n");

            if (selectedCast1Index > 1)
            {
                data.Cast1 = _comboBoxItems[selectedCast1Index].Value;
            }
            if (selectedCast2Index > 1)
            {
                data.Cast2 = _comboBoxItems[selectedCast2Index].Value;
            }
            if (selectedSpeechBubbleIndex > 1)
            {
                data.SpeechBubble = _comboBoxItems[selectedSpeechBubbleIndex].Value;
            }
        }

        public bool IsDataChanged(BakinPanelData.RowData originalData)
        {
            int selectedCast1Index = comboBoxCast1.SelectedIndex;
            int selectedCast2Index = comboBoxCast2.SelectedIndex;
            int selectedSpeechBubbleIndex = comboBoxSpeechBubble.SelectedIndex;

            if (originalData.Tag != comboBoxTagType.SelectedItem?.ToString() ||
                originalData.Text != textBoxText.Text.Replace(Environment.NewLine, "\\n") ||
                originalData.NPL != textBoxNPL.Text ||
                originalData.NPC != textBoxNPC.Text ||
                originalData.NPR != textBoxNPR.Text ||
                originalData.blspd != (int)numericUpDownBlspd.Value ||
                originalData.blrate != (int)numericUpDownBlrate.Value ||
                originalData.lipspd != (float)numericUpDownLipspd.Value ||
                originalData.ActCast1 != textBoxActCast1.Text ||
                originalData.ActCast2 != textBoxActCast2.Text ||
                originalData.TalkCast != (radioButtonTalkCast2.Checked ? "2" : "1") ||
                originalData.MirrorCast1 != checkBoxMirrorCast1.Checked ||
                originalData.MirrorCast2 != checkBoxMirrorCast2.Checked ||
                originalData.Billboard1 != checkBoxBillboard1.Checked ||
                originalData.Billboard2 != checkBoxBillboard2.Checked ||
                originalData.WindowVisible != checkBoxWindowHidden.Checked ||
                originalData.WindowPosition != comboBoxWindowPosition.SelectedItem?.ToString() ||
                originalData.UseMapLight != checkBoxUseMapLight.Checked ||
                originalData.Memo != textBoxMemo.Text.Replace(Environment.NewLine, "\\n") ||
                (selectedCast1Index > 1 && originalData.Cast1 != _comboBoxItems[selectedCast1Index].Value) ||
                (selectedCast2Index > 1 && originalData.Cast2 != _comboBoxItems[selectedCast2Index].Value) ||
                (selectedSpeechBubbleIndex > 1 && originalData.SpeechBubble != _comboBoxItems[selectedSpeechBubbleIndex].Value)
                )
            {
                return true;
            }

            return false;
        }

        public void PopulateFieldsWithData()
        {
            comboBoxTagType.SelectedItem = _data.Tag;
            textBoxText.Text = _data.Text.Replace("\\n", Environment.NewLine);
            textBoxNPL.Text = _data.NPL;
            textBoxNPC.Text = _data.NPC;
            textBoxNPR.Text = _data.NPR;
            numericUpDownBlspd.Value = _data.blspd;
            numericUpDownBlrate.Value = _data.blrate;
            numericUpDownLipspd.Value = (decimal)_data.lipspd;
            //comboBoxCast1.SelectedItem = _data.Cast1;
            textBoxActCast1.Text = _data.ActCast1;
            //comboBoxCast2.SelectedItem = _data.Cast2;
            textBoxActCast2.Text = _data.ActCast2;
            if (_data.TalkCast == "1")
            {
                radioButtonTalkCast1.Checked = true;
                radioButtonTalkCast2.Checked = false;
            }
            else if (_data.TalkCast == "2")
            {
                radioButtonTalkCast1.Checked = false;
                radioButtonTalkCast2.Checked = true;
            }
            else
            {
                radioButtonTalkCast1.Checked = false;
                radioButtonTalkCast2.Checked = false;
            }
            checkBoxMirrorCast1.Checked = _data.MirrorCast1;
            checkBoxMirrorCast2.Checked = _data.MirrorCast2;
            checkBoxBillboard1.Checked = _data.Billboard1;
            checkBoxBillboard2.Checked = _data.Billboard2;
            checkBoxWindowHidden.Checked = _data.WindowVisible;
            comboBoxWindowPosition.SelectedItem = _data.WindowPosition;
            //comboBoxSpeechBubble.SelectedItem = _data.SpeechBubble;
            checkBoxUseMapLight.Checked = _data.UseMapLight;
            textBoxMemo.Text = _data.Memo.Replace("\\n", Environment.NewLine);

            int matchingCast1Index = _comboBoxItems.FindIndex(i => i.Value == _data.Cast1);
            comboBoxCast1.SelectedIndex = matchingCast1Index != -1 ? matchingCast1Index : 1;
            if (_data.Cast1 == string.Empty)
            {
                comboBoxCast1.SelectedIndex = 0;
            }

            int matchingCast2Index = _comboBoxItems.FindIndex(i => i.Value == _data.Cast2);
            comboBoxCast2.SelectedIndex = matchingCast2Index != -1 ? matchingCast2Index : 1;
            if (_data.Cast2 == string.Empty)
            {
                comboBoxCast2.SelectedIndex = 0;
            }

            mirrorPictureBoxCast1 = false;
            setPictureBoxCast1();
            mirrorPictureBoxCast2 = false;
            setPictureBoxCast2();

            int matchingSpeechBubbleIndex = _comboBoxItems.FindIndex(i => i.Value == _data.SpeechBubble);
            comboBoxSpeechBubble.SelectedIndex = matchingSpeechBubbleIndex != -1 ? matchingSpeechBubbleIndex : 1;
            if (_data.SpeechBubble == string.Empty)
            {
                comboBoxSpeechBubble.SelectedIndex = 0;
            }
        }

        private void UpdateDataFromFields()
        {
            UpdateData(_data);
        }

        private void comboBoxTagType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTag = comboBoxTagType.SelectedItem.ToString();

            switch (selectedTag)
            {
                case "Talk":
                    comboBoxTagType.Enabled = true;
                    textBoxText.Enabled = true;
                    textBoxNPL.Enabled = true;
                    textBoxNPC.Enabled = true;
                    textBoxNPR.Enabled = true;
                    numericUpDownBlspd.Enabled = true;
                    numericUpDownBlrate.Enabled = true;
                    numericUpDownLipspd.Enabled = true;
                    comboBoxCast1.Enabled = true;
                    textBoxActCast1.Enabled = true;
                    comboBoxCast2.Enabled = true;
                    textBoxActCast2.Enabled = true;
                    radioButtonTalkCast1.Enabled = true;
                    radioButtonTalkCast2.Enabled = true;
                    checkBoxMirrorCast1.Enabled = true;
                    checkBoxMirrorCast2.Enabled = true;
                    checkBoxBillboard1.Enabled = true;
                    checkBoxBillboard2.Enabled = true;
                    checkBoxWindowHidden.Enabled = false;
                    comboBoxWindowPosition.Enabled = true;
                    comboBoxSpeechBubble.Enabled = true;
                    checkBoxUseMapLight.Enabled = true;
                    textBoxMemo.Enabled = true;
                    break;
                case "Message":
                    comboBoxTagType.Enabled = true;
                    textBoxText.Enabled = true;
                    textBoxNPL.Enabled = false;
                    textBoxNPC.Enabled = false;
                    textBoxNPR.Enabled = false;
                    numericUpDownBlspd.Enabled = false;
                    numericUpDownBlrate.Enabled = false;
                    numericUpDownLipspd.Enabled = false;
                    comboBoxCast1.Enabled = false;
                    textBoxActCast1.Enabled = false;
                    comboBoxCast2.Enabled = false;
                    textBoxActCast2.Enabled = false;
                    radioButtonTalkCast1.Enabled = false;
                    radioButtonTalkCast2.Enabled = false;
                    checkBoxMirrorCast1.Enabled = false;
                    checkBoxMirrorCast2.Enabled = false;
                    checkBoxBillboard1.Enabled = false;
                    checkBoxBillboard2.Enabled = false;
                    checkBoxWindowHidden.Enabled = true;
                    comboBoxWindowPosition.Enabled = true;
                    comboBoxSpeechBubble.Enabled = true;
                    checkBoxUseMapLight.Enabled = false;
                    textBoxMemo.Enabled = true;
                    break;
                case "Notes":
                    comboBoxTagType.Enabled = true;
                    textBoxText.Enabled = true;
                    textBoxNPL.Enabled = false;
                    textBoxNPC.Enabled = false;
                    textBoxNPR.Enabled = false;
                    numericUpDownBlspd.Enabled = false;
                    numericUpDownBlrate.Enabled = false;
                    numericUpDownLipspd.Enabled = false;
                    comboBoxCast1.Enabled = false;
                    textBoxActCast1.Enabled = false;
                    comboBoxCast2.Enabled = false;
                    textBoxActCast2.Enabled = false;
                    radioButtonTalkCast1.Enabled = false;
                    radioButtonTalkCast2.Enabled = false;
                    checkBoxMirrorCast1.Enabled = false;
                    checkBoxMirrorCast2.Enabled = false;
                    checkBoxBillboard1.Enabled = false;
                    checkBoxBillboard2.Enabled = false;
                    checkBoxWindowHidden.Enabled = false;
                    comboBoxWindowPosition.Enabled = false;
                    comboBoxSpeechBubble.Enabled = false;
                    checkBoxUseMapLight.Enabled = false;
                    textBoxMemo.Enabled = true;
                    break;
            }
        }

        private void InsertTextAtCaret(string text)
        {
            Control focusedControl = this.ActiveControl;

            if (focusedControl is TextBox textBox)
            {
                int caretIndex = textBox.SelectionStart;
                textBox.Text = textBox.Text.Insert(caretIndex, text);

                textBox.SelectionStart = caretIndex + text.Length;
            }
        }

        private void CreateInputHelperContextMenu()
        {
            ToolStripMenuItem boldMenuItem = new ToolStripMenuItem("Bold \\b");
            ToolStripMenuItem italicMenuItem = new ToolStripMenuItem("Italic \\i");
            ToolStripMenuItem underlineMenuItem = new ToolStripMenuItem("Underline \\u");
            ToolStripMenuItem fontSizeMenuItem = new ToolStripMenuItem("Font Size \\z[100]");
            ToolStripMenuItem fontColorMenuItem = new ToolStripMenuItem("Font Color \\c[FFFFFF]");
            ToolStripMenuItem charRubyMenuItem = new ToolStripMenuItem("Ruby on the Single Letter Immediately After \\r[Ruby]");
            ToolStripMenuItem strRubyMenuItem = new ToolStripMenuItem("Ruby for Specified Strings \\r[string,Ruby]");
            ToolStripMenuItem halfSecWaitMenuItem = new ToolStripMenuItem("0.5 Second Wait \\w");
            ToolStripMenuItem waitSecMenuItem = new ToolStripMenuItem("Specified Seconds Wait \\w[sec]");
            ToolStripMenuItem startInstantDisplayMenuItem = new ToolStripMenuItem("Instant Display(start) \\>");
            ToolStripMenuItem endInstantDisplayMenuItem = new ToolStripMenuItem("Instant display(end) \\<");
            ToolStripMenuItem waitForKeyMenuItem = new ToolStripMenuItem("🖱️ Waiting for Input \\!");
            ToolStripMenuItem autoCloseMenuItem = new ToolStripMenuItem("🔚 Close Automatically \\^");
            ToolStripMenuItem viewVariableMenuItem = new ToolStripMenuItem("Display the Value of a Variable \\$[Variable Name]");
            ToolStripMenuItem viewArrayMenuItem = new ToolStripMenuItem("Display the Value of a Array Variable \\$[Variable Name][Index]");
            ToolStripMenuItem viewLocalVariableMenuItem = new ToolStripMenuItem("Display the Value of a Local Variable \\L[Variable Name]");
            ToolStripMenuItem viewCastNameMenuItem = new ToolStripMenuItem("Display Cast's Name \\H[Name on Database]");

            boldMenuItem.Click += (s, e) => InsertTextAtCaret("\\b");
            italicMenuItem.Click += (s, e) => InsertTextAtCaret("\\i");
            underlineMenuItem.Click += (s, e) => InsertTextAtCaret("\\u");
            fontSizeMenuItem.Click += (s, e) => InsertTextAtCaret("\\z[100]");
            fontColorMenuItem.Click += (s, e) => InsertTextAtCaret("\\c[FFFFFF]");
            charRubyMenuItem.Click += (s, e) => InsertTextAtCaret("\\r[Ruby]");
            strRubyMenuItem.Click += (s, e) => InsertTextAtCaret("\\r[string,Ruby]");
            halfSecWaitMenuItem.Click += (s, e) => InsertTextAtCaret("\\w");
            waitSecMenuItem.Click += (s, e) => InsertTextAtCaret("\\w[sec]");
            startInstantDisplayMenuItem.Click += (s, e) => InsertTextAtCaret("\\>");
            endInstantDisplayMenuItem.Click += (s, e) => InsertTextAtCaret("\\<");
            waitForKeyMenuItem.Click += (s, e) => InsertTextAtCaret("\\!");
            autoCloseMenuItem.Click += (s, e) => InsertTextAtCaret("\\^");
            viewVariableMenuItem.Click += (s, e) => InsertTextAtCaret("\\$[Variable Name]");
            viewArrayMenuItem.Click += (s, e) => InsertTextAtCaret("\\$[Variable Name][Index]");
            viewLocalVariableMenuItem.Click += (s, e) => InsertTextAtCaret("\\L[Variable Name]");
            viewCastNameMenuItem.Click += (s, e) => InsertTextAtCaret("\\H[Name on Database]");

            contextMenuStripInputHelper.Items.AddRange(new ToolStripMenuItem[]
            {
                boldMenuItem, italicMenuItem, underlineMenuItem, fontSizeMenuItem, fontColorMenuItem, charRubyMenuItem, strRubyMenuItem,
                halfSecWaitMenuItem, waitSecMenuItem, startInstantDisplayMenuItem, endInstantDisplayMenuItem, waitForKeyMenuItem, autoCloseMenuItem,
                viewVariableMenuItem, viewArrayMenuItem, viewLocalVariableMenuItem, viewCastNameMenuItem
            });

            textBoxText.ContextMenuStrip = contextMenuStripInputHelper;
        }

        public void AdjustComboBoxDropdownWidth(ComboBox comboBox)
        {
            int maxWidth = 0;

            foreach (var obj in comboBox.Items)
            {
                string item = comboBox.GetItemText(obj);
                int temp = TextRenderer.MeasureText(item, comboBox.Font).Width;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }

            comboBox.DropDownWidth = maxWidth;
        }

        private void comboBoxCast1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            setPictureBoxCast1();
        }

        private void setPictureBoxCast1()
        {
            var selectedItemString = comboBoxCast1.SelectedItem as string;
            if (selectedItemString == null)
            {
                pictureBoxCast1.Image = null;
                return;
            }
            var selectedIndexStr = selectedItemString.Split(':')[0].TrimStart('[').TrimEnd(']');
            if (!int.TryParse(selectedIndexStr, out int selectedIndex))
            {
                pictureBoxCast1.Image = null;
                return;
            }

            if (selectedIndex < 0 || selectedIndex >= _comboBoxItems.Count)
            {
                pictureBoxCast1.Image = null;
                return;
            }

            var selectedItem = _comboBoxItems[selectedIndex];
            if (selectedIndex == 1)
            {
                pictureBoxCast1.Image = Properties.Resources.prev_dumy;
                Rotate_pictureBoxCast1(checkBoxMirrorCast1.Checked);
            }
            else if (string.IsNullOrEmpty(selectedItem.ImagePath))
            {
                pictureBoxCast1.Image = null;
                return;
            }
            else
            {
                var oldImage = pictureBoxCast1.Image;
                var image = GetImageFromCache(selectedItem.ImagePath);
                if (image != null)
                {
                    var bitmap = new Bitmap(image);
                    pictureBoxCast1.Image = bitmap;
                    Rotate_pictureBoxCast1(checkBoxMirrorCast1.Checked);
                }
                else
                {
                    pictureBoxCast1.Image = null;
                }
                oldImage?.Dispose();
            }
        }

        private void comboBoxCast2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            setPictureBoxCast2();
        }

        private void setPictureBoxCast2()
        {
            var selectedItemString = comboBoxCast2.SelectedItem as string;
            if (selectedItemString == null)
            {
                pictureBoxCast2.Image = null;
                return;
            }
            var selectedIndexStr = selectedItemString.Split(':')[0].TrimStart('[').TrimEnd(']');
            if (!int.TryParse(selectedIndexStr, out int selectedIndex))
            {
                pictureBoxCast2.Image = null;
                return;
            }

            if (selectedIndex < 0 || selectedIndex >= _comboBoxItems.Count)
            {
                pictureBoxCast2.Image = null;
                return;
            }

            var selectedItem = _comboBoxItems[selectedIndex];
            if (selectedIndex == 1)
            {
                pictureBoxCast2.Image = Properties.Resources.prev_dumy2;
                Rotate_pictureBoxCast2(checkBoxMirrorCast2.Checked);
            }
            else if (string.IsNullOrEmpty(selectedItem.ImagePath))
            {
                pictureBoxCast2.Image = null;
                return;
            }
            else
            {
                var oldImage = pictureBoxCast2.Image;
                var image = GetImageFromCache(selectedItem.ImagePath);
                if (image != null)
                {
                    var bitmap = new Bitmap(image);
                    pictureBoxCast2.Image = bitmap;
                    Rotate_pictureBoxCast2(checkBoxMirrorCast2.Checked);
                }
                else
                {
                    pictureBoxCast2.Image = null;
                }
                oldImage?.Dispose();
            }
        }

        private void checkBoxMirrorCast1_CheckedChanged(object sender, EventArgs e)
        {
            Rotate_pictureBoxCast1(checkBoxMirrorCast1.Checked);
        }

        private void checkBoxMirrorCast2_CheckedChanged(object sender, EventArgs e)
        {
            Rotate_pictureBoxCast2(checkBoxMirrorCast2.Checked);
        }

        private void Rotate_pictureBoxCast1(bool rotate)
        {
            if (pictureBoxCast1.Image == null) return;
            if (mirrorPictureBoxCast1 != rotate)
            {
                mirrorPictureBoxCast1 = rotate;
                pictureBoxCast1.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBoxCast1.Refresh();
            }
        }

        private void Rotate_pictureBoxCast2(bool rotate)
        {
            if (pictureBoxCast2.Image == null) return;
            if (mirrorPictureBoxCast2 != rotate)
            {
                mirrorPictureBoxCast2 = rotate;
                pictureBoxCast2.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBoxCast2.Refresh();
            }
        }

        private void doUpdatePreview()
        {
            PrevPictureBoxUpdate();
            PositionAbsoluteControls();
            PositionVariableControls();
            panelPreview.Refresh();
        }

        private void PositionAbsoluteControls()
        {

        }

        private void PositionVariableControls()
        {
            prevTextBoxNPLCR.Visible = false;
            prevTextBoxActCast1.Visible = false;
            prevTextBoxActCast2.Visible = false;
            prevTableLayoutPanelBubble.Visible = false;
            if (comboBoxTagType.SelectedItem?.ToString() == "Talk")
            {
                if (textBoxNPL.Text != String.Empty || textBoxNPC.Text != String.Empty || textBoxNPR.Text != String.Empty)
                {
                    prevTextBoxNPLCR.Visible = true;
                }
                if (comboBoxCast1.SelectedIndex != 0)
                {
                    prevTextBoxActCast1.Visible = true;
                }
                if (comboBoxCast1.SelectedIndex != 0)
                {
                    prevTextBoxActCast2.Visible = true;
                }
            }

            if ((comboBoxTagType.SelectedItem?.ToString() == "Message" && checkBoxWindowHidden.Checked == true) ||
                comboBoxTagType.SelectedItem?.ToString() == "Notes")
            {
                prevCustomRichTextBoxText.BorderStyle = BorderStyle.None;
                prevCustomRichTextBoxText.ChangeTransparent(true);
            }
            else
            {
                prevCustomRichTextBoxText.BorderStyle = BorderStyle.Fixed3D;
                prevCustomRichTextBoxText.ChangeTransparent(false);
            }

            float relativeWidth = 610.0f / 960.0f;
            float relativeHeight = 120.0f / 540.0f;
            int actualWidth = (int)(panelPreview.Width * relativeWidth);
            int actualHeight = (int)(panelPreview.Height * relativeHeight);
            prevCustomRichTextBoxText.Size = new Size(actualWidth, actualHeight);
            prevTableLayoutPanelBubble.Size = new Size(panelPreview.Width, panelPreview.Width / 5);

            float relativeX = 175.0f / 960.0f;
            float relativeY = 400.0f / 540.0f;

            float relativeX2 = 175.0f / 960.0f;
            float relativeY2 = 365.0f / 540.0f;

            string windowPosition = comboBoxWindowPosition.SelectedItem?.ToString();
            prevTableLayoutPanelBubble.Visible = true;
            prevPictureBoxPc1.Image?.Dispose();
            prevPictureBoxPc2.Image?.Dispose();
            prevPictureBoxPc3.Image?.Dispose();
            prevPictureBoxPc4.Image?.Dispose();
            prevPictureBoxEvent.Image?.Dispose();
            switch (windowPosition)
            {
                case "Bubble(Player)":
                    prevPictureBoxPc1.Image = Properties.Resources.prev_pc11;
                    prevPictureBoxPc2.Image = Properties.Resources.prev_pc02;
                    prevPictureBoxPc3.Image = Properties.Resources.prev_pc03;
                    prevPictureBoxPc4.Image = Properties.Resources.prev_pc04;
                    prevPictureBoxEvent.Image = Properties.Resources.prev_event;
                    break;
                case "Bubble(ThisEvent)":
                    prevPictureBoxPc1.Image = Properties.Resources.prev_pc01;
                    prevPictureBoxPc2.Image = Properties.Resources.prev_pc02;
                    prevPictureBoxPc3.Image = Properties.Resources.prev_pc03;
                    prevPictureBoxPc4.Image = Properties.Resources.prev_pc04;
                    prevPictureBoxEvent.Image = Properties.Resources.prev_event2;
                    break;
                case "Bubble(Member2)":
                    prevPictureBoxPc1.Image = Properties.Resources.prev_pc01;
                    prevPictureBoxPc2.Image = Properties.Resources.prev_pc12;
                    prevPictureBoxPc3.Image = Properties.Resources.prev_pc03;
                    prevPictureBoxPc4.Image = Properties.Resources.prev_pc04;
                    prevPictureBoxEvent.Image = Properties.Resources.prev_event;
                    break;
                case "Bubble(Member3)":
                    prevPictureBoxPc1.Image = Properties.Resources.prev_pc01;
                    prevPictureBoxPc2.Image = Properties.Resources.prev_pc02;
                    prevPictureBoxPc3.Image = Properties.Resources.prev_pc13;
                    prevPictureBoxPc4.Image = Properties.Resources.prev_pc04;
                    prevPictureBoxEvent.Image = Properties.Resources.prev_event;
                    break;
                case "Bubble(Member4)":
                    prevPictureBoxPc1.Image = Properties.Resources.prev_pc01;
                    prevPictureBoxPc2.Image = Properties.Resources.prev_pc02;
                    prevPictureBoxPc3.Image = Properties.Resources.prev_pc03;
                    prevPictureBoxPc4.Image = Properties.Resources.prev_pc14;
                    prevPictureBoxEvent.Image = Properties.Resources.prev_event;
                    break;
                case "Bubble(Event)":
                    prevPictureBoxPc1.Image = null;
                    prevPictureBoxPc2.Image = null;
                    prevPictureBoxPc3.Image = getBubbleEventGraphic();
                    prevPictureBoxPc4.Image = null;
                    prevPictureBoxEvent.Image = null;
                    break;
                default:
                    prevTableLayoutPanelBubble.Visible = false;
                    break;
            }

            switch (windowPosition)
            {
                case "Up":
                    relativeY = 15.0f / 540.0f;
                    relativeY2 = 140.0f / 540.0f;
                    break;
                case "Bubble(Player)":
                case "Bubble(ThisEvent)":
                case "Bubble(Member2)":
                case "Bubble(Member3)":
                case "Bubble(Member4)":
                case "Bubble(Event)":
                case "Center":
                    relativeY = 210.0f / 540.0f;
                    relativeY2 = 170.0f / 540.0f;
                    break;
                case "Down":
                default:
                    relativeY = 400.0f / 540.0f;
                    relativeY2 = 365.0f / 540.0f;
                    break;
            }

            if (textBoxNPL.Text != string.Empty)
            {
                relativeX2 = 175.0f / 960.0f;
            }
            else if (textBoxNPC.Text != string.Empty)
            {
                relativeX2 = 390.0f / 960.0f;
            }
            else if (textBoxNPR.Text != string.Empty)
            {
                relativeX2 = 605.0f / 960.0f;
            }

            int actualX = (int)(panelPreview.Width * relativeX);
            int actualY = (int)(panelPreview.Height * relativeY);
            int actualX2 = (int)(panelPreview.Width * relativeX2);
            int actualY2 = (int)(panelPreview.Height * relativeY2);
            prevTextBoxNPLCR.Location = new Point(actualX2, actualY2);
            prevCustomRichTextBoxText.Location = new Point(actualX, actualY);
            UpdateText();
        }

        private Image getBubbleEventGraphic()
        {
            var selectedItemString = comboBoxSpeechBubble.SelectedItem as string;
            if (selectedItemString == null)
            {
                return Properties.Resources.prev_event3;
            }
            var selectedIndexStr = selectedItemString.Split(':')[0].TrimStart('[').TrimEnd(']');
            if (!int.TryParse(selectedIndexStr, out int selectedIndex))
            {
                return Properties.Resources.prev_event3;
            }

            if (selectedIndex < 0 || selectedIndex >= _comboBoxItems.Count)
            {
                return Properties.Resources.prev_event3;
            }

            var selectedItem = _comboBoxItems[selectedIndex];
            if (string.IsNullOrEmpty(selectedItem.ImagePath))
            {
                return Properties.Resources.prev_event3;
            }

            return new Bitmap(GetImageFromCache(selectedItem.ImagePath));
        }

        private async void UpdateText()
        {
            prevCustomRichTextBoxText.BringToFront();
            prevTextBoxNotes.Text = String.Empty;
            prevCustomRichTextBoxText.Text = string.Empty;

            if (comboBoxTagType.SelectedItem?.ToString() == "Notes")
            {
                prevTextBoxNotes.Text = textBoxText.Text;
                return;
            }
            else
            {
                prevTextBoxNotes.Text = textBoxMemo.Text;
            }

            if (comboBoxTagType.SelectedItem?.ToString() == "Talk")
            {
                if (textBoxNPL.Text != string.Empty)
                {
                    prevTextBoxNPLCR.Text = textBoxNPL.Text;
                }
                else if (textBoxNPC.Text != string.Empty)
                {
                    prevTextBoxNPLCR.Text = textBoxNPC.Text;
                }
                else if (textBoxNPR.Text != string.Empty)
                {
                    prevTextBoxNPLCR.Text = textBoxNPR.Text;
                }
                if (comboBoxCast1.SelectedIndex != 0)
                {
                    prevTextBoxActCast1.Text = textBoxActCast1.Text;
                }
                if (comboBoxCast2.SelectedIndex != 0)
                {
                    prevTextBoxActCast2.Text = textBoxActCast2.Text;
                }
            }

            cancelTokenSource = new CancellationTokenSource();
            skipTokenSource = new CancellationTokenSource();
            try
            {
                await prevCustomRichTextBoxText.StreamText(textBoxText.Text, 100, checkBoxAutoScroll.Checked, cancelTokenSource.Token, skipTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                ;
            }
            finally
            {
                ;
            }
        }

        private void PrevPictureBoxUpdate()
        {
            if (comboBoxTagType.SelectedItem?.ToString() != "Talk")
            {
                prevPictureBoxCast1.Visible = false;
                prevPictureBoxCast2.Visible = false;
                return;
            }

            float brightnessCast1 = 1.0f;
            float brightnessCast2 = 1.0f;

            if (radioButtonTalkCast1.Checked == false) brightnessCast1 = 0.5f;
            if (radioButtonTalkCast2.Checked == false) brightnessCast2 = 0.5f;

            if (pictureBoxCast1.Image != null)
            {
                Bitmap originalBitmap = new Bitmap(pictureBoxCast1.Image);
                Bitmap adjustedBitmap = ChangeBrightness(originalBitmap, brightnessCast1);
                prevPictureBoxCast1.Image?.Dispose();
                prevPictureBoxCast1.Image = adjustedBitmap;
                originalBitmap.Dispose();
                prevPictureBoxCast1.Visible = true;
            }
            else
            {
                prevPictureBoxCast1.Visible = false;
            }
            if (pictureBoxCast2.Image != null)
            {
                Bitmap originalBitmap = new Bitmap(pictureBoxCast2.Image);
                Bitmap adjustedBitmap = ChangeBrightness(originalBitmap, brightnessCast2);
                prevPictureBoxCast2.Image?.Dispose();
                prevPictureBoxCast2.Image = adjustedBitmap;
                originalBitmap.Dispose();
                prevPictureBoxCast2.Visible = true;
            }
            else
            {
                prevPictureBoxCast2.Visible = false;
            }

        }

        private Bitmap ChangeBrightness(Bitmap original, float brightness)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(newBitmap);

            brightness = brightness < 0 ? 0 : brightness;
            brightness = brightness > 1 ? 1 : brightness;

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    Color originalColor = original.GetPixel(i, j);
                    int red = (int)(originalColor.R * brightness);
                    int green = (int)(originalColor.G * brightness);
                    int blue = (int)(originalColor.B * brightness);
                    Color newColor = Color.FromArgb(originalColor.A, red, green, blue);
                    newBitmap.SetPixel(i, j, newColor);
                }
            }

            g.Dispose();
            return newBitmap;
        }

        private void panelPreview_SizeChanged(object sender, EventArgs e)
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();
            }
            PositionVariableControls();
            doUpdatePreview();
        }

        private void UpdatePreview(object sender, EventArgs e)
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();
            }
            doUpdatePreview();
        }

        private void comboBoxWindowPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxWindowPosition.SelectedItem.ToString() == "Bubble(Event)")
            {
                comboBoxSpeechBubble.Enabled = true;
            }
            else
            {
                comboBoxSpeechBubble.Enabled = false;
            }
        }
        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();
            }
            UpdateDataFromFields();
            DataChanged?.Invoke(this, _data);
            doUpdatePreview();
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            UpdatePreview(sender, e);
        }

        private void buttonNextRow_Click(object sender, EventArgs e)
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();
            }
            UpdateDataFromFields();
            DataChanged?.Invoke(this, _data);
            RowChangeRequested?.Invoke(this, new PreviewEditorEventArgs(1));
            doUpdatePreview();
        }

        private void buttonPreviousRow_Click(object sender, EventArgs e)
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();
            }
            UpdateDataFromFields();
            DataChanged?.Invoke(this, _data);
            RowChangeRequested?.Invoke(this, new PreviewEditorEventArgs(-1));
            doUpdatePreview();
        }

        private void buttonMoveUpRow_Click(object sender, EventArgs e)
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();
            }
            UpdateDataFromFields();
            DataChanged?.Invoke(this, _data);
            RowSwapRequested?.Invoke(this, new SwapRowEventArgs(-1));
            doUpdatePreview();
        }

        private void buttonMoveDownRow_Click(object sender, EventArgs e)
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();
            }
            UpdateDataFromFields();
            DataChanged?.Invoke(this, _data);
            RowSwapRequested?.Invoke(this, new SwapRowEventArgs(1));
            doUpdatePreview();
        }

        private void buttonDuplicateRow_Click(object sender, EventArgs e)
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();
            }
            UpdateDataFromFields();
            DataChanged?.Invoke(this, _data);
            RowAddRequested?.Invoke(this, new AddRowEventArgs(1));
            doUpdatePreview();
            DataChanged?.Invoke(this, _data);
        }

        private void buttonStreamSkip_Click(object sender, EventArgs e)
        {
            if (skipTokenSource != null)
            {
                skipTokenSource.Cancel();
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();
            }
            PopulateFieldsWithData();
            doUpdatePreview();
        }

        private void FormPreviewEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();
            }
        }

        private void FormPreviewEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsDataChanged(_data))
            {
                var result = MessageBox.Show("Do you want to close the Preview Editor without saving the current scene?\r\nIf you want to save the scene, please cancel and press Apply.", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void FormPreviewEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.D:
                        if (e.Shift)
                        {
                            buttonDuplicateRow.PerformClick();
                        }
                        break;
                    case Keys.P:
                        if (e.Shift)
                        {
                            buttonPreview.PerformClick();
                        }
                        break;
                    case Keys.R:
                        if (e.Shift)
                        {
                            buttonReload.PerformClick();
                        }
                        break;
                    case Keys.S:
                        if (e.Shift)
                        {
                            buttonStreamSkip.PerformClick();
                        }
                        else
                        {
                            if (cancelTokenSource != null)
                            {
                                cancelTokenSource.Cancel();
                            }
                            UpdateDataFromFields();
                            DataChanged?.Invoke(this, _data);
                            CSVSaveRequested?.Invoke(this, new SaveCSVEventArgs());
                        }
                        break;
                    case Keys.Right:
                        if (e.Shift)
                        {
                            radioButtonTalkCast2.PerformClick();
                        }
                        else
                        {
                            buttonPreviousRow.PerformClick();
                        }
                        break;
                    case Keys.Left:
                        if (e.Shift)
                        {
                            radioButtonTalkCast1.PerformClick();
                        }
                        else
                        {
                            buttonPreviousRow.PerformClick();
                        }
                        break;
                    case Keys.Up:
                        if (e.Shift)
                        {
                            buttonMoveUp.PerformClick();
                        }
                        else
                        {
                            buttonPreviousRow.PerformClick();
                        }
                        break;
                    case Keys.Down:
                        if (e.Shift)
                        {
                            buttonMoveDown.PerformClick();
                        }
                        else
                        {
                            buttonNextRow.PerformClick();
                        }
                        break;
                    case Keys.Enter:
                        if (e.Shift)
                        {
                            buttonApply.PerformClick();
                        }
                        break;
                }
                e.SuppressKeyPress = true;
            }
        }
    }
}
