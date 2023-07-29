using AsImageProcessingLibrary;
using ClipboardToolForBakin;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using static ClipboardToolForBakin2.FormKeysSetting;
using Size = System.Drawing.Size;
using Image = System.Drawing.Image;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;
using static AsImageProcessingLibrary.ImageProcessor;
using System.Windows.Forms;
using System.Reflection;

namespace ClipboardToolForBakin2
{
    public partial class FormPreviewEditor : Form
    {
        public delegate void RowChangeRequestedHandler(object sender, PreviewEditorEventArgs e);
        public event RowChangeRequestedHandler? RowChangeRequested;
        public delegate void RowAddRequestedHandler(object sender, AddRowEventArgs e);
        public event RowAddRequestedHandler? RowAddRequested;
        public delegate void RowSwapRequestedHandler(object sender, SwapRowEventArgs e);
        public event RowSwapRequestedHandler? RowSwapRequested;
        public delegate void CSVSaveRequestHandler(object sender, SaveCSVEventArgs e);
        public event CSVSaveRequestHandler? CSVSaveRequested;
        public event EventHandler<BakinPanelData.RowData>? DataChanged;
        private BakinPanelData.RowData _data;
        private List<ResourceItem> _comboBoxItems;
        private List<ShortcutKeyBinding> _shortcutKeyBindings;
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
        private ToolTip toolTipKeys = new ToolTip();
        private ImageProcessor _imageProcessor = new ImageProcessor();
        private System.Drawing.Image? _drawingMergedImage;
        private System.Drawing.Image? _drawingCast1Image;
        private System.Drawing.Image? _drawingCast2Image;
        private bool enableFileCache = false;

        public FormPreviewEditor(BakinPanelData.RowData rowData, List<ResourceItem> comboBoxItems, List<ShortcutKeyBinding> shortcutKeyBindings)
        {
            InitializeComponent();
            panelCast1.EnableDoubleBuffering();
            panelCast2.EnableDoubleBuffering();
            panelPreview.EnableDoubleBuffering();
            InitializePreview();
            CreateInputHelperContextMenu();
            _data = rowData;
            _comboBoxItems = comboBoxItems;
            _shortcutKeyBindings = shortcutKeyBindings;
            UpdateComboBoxes();
            panelPreview.Controls.Add(prevCustomRichTextBoxText);
            PopulateFieldsWithData();
            UpdateToolTipKeys();
        }

        public void InitializePreview()
        {
            panelCast1.Paint += (s, e) =>
            {
                if (_drawingCast1Image != null)
                {
                    e.Graphics.DrawImage(_drawingCast1Image, 0, 0, panelCast1.Width, panelCast1.Height);
                }
            };
            panelCast2.Paint += (s, e) =>
            {
                if (_drawingCast2Image != null)
                {
                    e.Graphics.DrawImage(_drawingCast2Image, 0, 0, panelCast2.Width, panelCast2.Height);
                }
            };
            panelPreview.Paint += (s, e) =>
            {
                if (_drawingMergedImage != null)
                {
                    float srcWidth = _drawingMergedImage.Width;
                    float srcHeight = _drawingMergedImage.Height;
                    float dstWidth = panelPreview.Width;
                    float dstHeight = panelPreview.Height;
                    float srcAspect = srcWidth / srcHeight;
                    float dstAspect = dstWidth / dstHeight;
                    float scaledWidth;
                    float scaledHeight;
                    if (srcAspect > dstAspect)
                    {
                        scaledWidth = dstWidth;
                        scaledHeight = scaledWidth / srcAspect;
                    }
                    else
                    {
                        scaledHeight = dstHeight;
                        scaledWidth = scaledHeight * srcAspect;
                    }
                    float xPosition = (dstWidth - scaledWidth) / 2;
                    float yPosition = (dstHeight - scaledHeight) / 2;
                    e.Graphics.DrawImage(_drawingMergedImage, xPosition, yPosition, scaledWidth, scaledHeight);
                }
            };
        }

        public void SetGraphicCache(bool enable)
        {
            enableFileCache = enable;
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
            GenerateCacheImage();
        }

        private void UpdateComboBox(ComboBox comboBox)
        {
            int cnt = 0;
            _imageProcessor.ClearProcessedImageCache();
            comboBox.Items.Clear();
            foreach (var item in _comboBoxItems)
            {
                if ((!string.IsNullOrEmpty(item.Key) && !string.IsNullOrEmpty(item.Value)) || cnt == 0 || cnt == 1)
                {
                    comboBox.Items.Add($"[{_comboBoxItems.IndexOf(item)}]:{item.Key}");
                }
                cnt++;
            }
        }

        void GenerateCacheImage()
        {
            _imageProcessor.Dispose();
            _imageProcessor = new ImageProcessor();
            if (enableFileCache == true)
            {
                foreach (var item in _comboBoxItems)
                {
                    if ((!string.IsNullOrEmpty(item.Key) && !string.IsNullOrEmpty(item.Value)) && !string.IsNullOrEmpty(item.ImagePath))
                    {
                        _imageProcessor.CacheImage(item.ImagePath);
                    }
                }
            }
            var resourceNames = new List<string>
            {
                "prev_dummy",
                "prev_dummy2",
                "prev_event",
                "prev_event2",
                "prev_event3",
                "prev_pc01",
                "prev_pc02",
                "prev_pc03",
                "prev_pc04",
                "prev_pc11",
                "prev_pc12",
                "prev_pc13",
                "prev_pc14"
            };
            foreach (var resourceName in resourceNames)
            {
                object? resource = Properties.Resources.ResourceManager.GetObject(resourceName);
                if (resource is Bitmap bitmap)
                {
                    _imageProcessor.CacheImageFromBitmap(resourceName, bitmap);
                }
                else
                {
                    Console.WriteLine($"Resource {resourceName} not found or could not be loaded.");
                }
            }
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

            data.Tag = comboBoxTagType.SelectedItem?.ToString() ?? "Notes";
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
            data.WindowPosition = comboBoxWindowPosition.SelectedItem?.ToString() ?? "Down";
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

            updatePictureBoxCast1();
            updatePictureBoxCast2();

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
            string selectedTag = comboBoxTagType.SelectedItem?.ToString() ?? "Notes";

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
            updatePictureBoxCast1();
            updatePictureBoxCast2();
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

        private void comboBoxCast1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatePictureBoxCast1();
            if (String.IsNullOrEmpty(textBoxActCast1.Text) || String.IsNullOrWhiteSpace(textBoxActCast1.Text))
            {
                textBoxActCast1.Text = "normal";
            }
        }

        private void updatePictureBoxCast1()
        {
            Image<Rgba32>? imageCast = null;
            try
            {
                if (comboBoxCast1.SelectedIndex > 0 && comboBoxTagType.SelectedItem?.ToString() == "Talk")
                {
                    if (!String.IsNullOrEmpty(_comboBoxItems[comboBoxCast1.SelectedIndex].ImagePath))
                    {
                        imageCast = _imageProcessor.GetImage(_comboBoxItems[comboBoxCast1.SelectedIndex].ImagePath, checkBoxMirrorCast1.Checked, 1.0f);
                    }
                    else
                    {
                        imageCast = _imageProcessor.GetImage("prev_dummy", checkBoxMirrorCast1.Checked, 1.0f);
                    }
                }
                var imagesToMerge = new List<(Image<Rgba32>, ImagePositionAndSize)>();
                if (imageCast != null)
                {
                    imagesToMerge.Add((imageCast, new ImagePositionAndSize { Position = new SixLabors.ImageSharp.Point(0, 0), Size = new SixLabors.ImageSharp.Size(52, 52) }));
                }
                using (var mergedImage = _imageProcessor.MergePictureBoxImages(imagesToMerge))
                {
                    _drawingCast1Image?.Dispose();
                    _drawingCast1Image = _imageProcessor.GetAsDrawingImage(mergedImage);
                }
                panelCast1.Invalidate();
            }
            finally
            {
                imageCast?.Dispose();
            }
        }

        private void comboBoxCast2_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatePictureBoxCast2();
            if (String.IsNullOrEmpty(textBoxActCast2.Text) || String.IsNullOrWhiteSpace(textBoxActCast2.Text))
            {
                textBoxActCast2.Text = "normal";
            }
        }

        private void updatePictureBoxCast2()
        {
            Image<Rgba32>? imageCast = null;
            try
            {
                if (comboBoxCast2.SelectedIndex > 0 && comboBoxTagType.SelectedItem?.ToString() == "Talk")
                {
                    if (!String.IsNullOrEmpty(_comboBoxItems[comboBoxCast2.SelectedIndex].ImagePath))
                    {
                        imageCast = _imageProcessor.GetImage(_comboBoxItems[comboBoxCast2.SelectedIndex].ImagePath, checkBoxMirrorCast2.Checked, 1.0f);
                    }
                    else
                    {
                        imageCast = _imageProcessor.GetImage("prev_dummy2", checkBoxMirrorCast2.Checked, 1.0f);
                    }
                }

                var imagesToMerge = new List<(Image<Rgba32>, ImagePositionAndSize)>();
                if (imageCast != null)
                {
                    imagesToMerge.Add((imageCast, new ImagePositionAndSize { Position = new SixLabors.ImageSharp.Point(0, 0), Size = new SixLabors.ImageSharp.Size(52, 52) }));
                }
                using (var mergedImage = _imageProcessor.MergePictureBoxImages(imagesToMerge))
                {
                    _drawingCast2Image?.Dispose();
                    _drawingCast2Image = _imageProcessor.GetAsDrawingImage(mergedImage);
                }
                panelCast2.Invalidate();
            }
            finally
            {
                imageCast?.Dispose();
            }
        }

        private void checkBoxMirrorCast1_CheckedChanged(object sender, EventArgs e)
        {
            updatePictureBoxCast1();
        }

        private void checkBoxMirrorCast2_CheckedChanged(object sender, EventArgs e)
        {
            updatePictureBoxCast2();
        }

        private void doUpdatePreview()
        {
            PositionVariableControls();
            UpdateText();
        }

        private void PositionVariableControls()
        {
            Image<Rgba32>? imageCast1 = null;
            Image<Rgba32>? imageCast2 = null;
            Image<Rgba32>? imageBubble1 = null;
            Image<Rgba32>? imageBubble2 = null;
            Image<Rgba32>? imageBubble3 = null;
            Image<Rgba32>? imageBubble4 = null;
            Image<Rgba32>? imageBubbleThis = null;
            Image<Rgba32>? imageBubbleEvent = null;
            Image<Rgba32>? imageBubble3B = null;
            try
            {
                if (comboBoxTagType.SelectedItem?.ToString() == "Talk")
                {
                    if (comboBoxCast1.SelectedIndex <= 0)
                    {
                        ;
                    }
                    else if (!String.IsNullOrEmpty(_comboBoxItems[comboBoxCast1.SelectedIndex].ImagePath))
                    {
                        imageCast1 = _imageProcessor.GetImage(_comboBoxItems[comboBoxCast1.SelectedIndex].ImagePath, checkBoxMirrorCast1.Checked, radioButtonTalkCast1.Checked ? 1.0f : 0.5f);
                    }
                    else
                    {
                        imageCast1 = _imageProcessor.GetImage("prev_dummy", checkBoxMirrorCast1.Checked, radioButtonTalkCast1.Checked ? 1.0f : 0.5f);
                    }

                    if (comboBoxCast2.SelectedIndex <= 0)
                    {
                        ;
                    }
                    else if (!String.IsNullOrEmpty(_comboBoxItems[comboBoxCast2.SelectedIndex].ImagePath))
                    {
                        imageCast2 = _imageProcessor.GetImage(_comboBoxItems[comboBoxCast2.SelectedIndex].ImagePath, checkBoxMirrorCast2.Checked, radioButtonTalkCast2.Checked ? 1.0f : 0.5f);
                    }
                    else
                    {
                        imageCast2 = _imageProcessor.GetImage("prev_dummy2", checkBoxMirrorCast2.Checked, radioButtonTalkCast2.Checked ? 1.0f : 0.5f);
                    }
                }
                if (comboBoxTagType.SelectedItem?.ToString() == "Talk" || comboBoxTagType.SelectedItem?.ToString() == "Message")
                {
                    switch (comboBoxWindowPosition.SelectedItem?.ToString())
                    {
                        case "Bubble(Player)":
                            imageBubble1 = _imageProcessor.GetImage("prev_pc11", false, 1.0f);
                            imageBubble2 = _imageProcessor.GetImage("prev_pc02", false, 1.0f);
                            imageBubble3 = _imageProcessor.GetImage("prev_pc03", false, 1.0f);
                            imageBubble4 = _imageProcessor.GetImage("prev_pc04", false, 1.0f);
                            imageBubbleThis = _imageProcessor.GetImage("prev_event", false, 1.0f);
                            break;
                        case "Bubble(ThisEvent)":
                            imageBubble1 = _imageProcessor.GetImage("prev_pc01", false, 1.0f);
                            imageBubble2 = _imageProcessor.GetImage("prev_pc02", false, 1.0f);
                            imageBubble3 = _imageProcessor.GetImage("prev_pc03", false, 1.0f);
                            imageBubble4 = _imageProcessor.GetImage("prev_pc04", false, 1.0f);
                            imageBubbleThis = _imageProcessor.GetImage("prev_event2", false, 1.0f);
                            break;
                        case "Bubble(Member2)":
                            imageBubble1 = _imageProcessor.GetImage("prev_pc01", false, 1.0f);
                            imageBubble2 = _imageProcessor.GetImage("prev_pc12", false, 1.0f);
                            imageBubble3 = _imageProcessor.GetImage("prev_pc03", false, 1.0f);
                            imageBubble4 = _imageProcessor.GetImage("prev_pc04", false, 1.0f);
                            imageBubbleThis = _imageProcessor.GetImage("prev_event", false, 1.0f);
                            break;
                        case "Bubble(Member3)":
                            imageBubble1 = _imageProcessor.GetImage("prev_pc01", false, 1.0f);
                            imageBubble2 = _imageProcessor.GetImage("prev_pc02", false, 1.0f);
                            imageBubble3 = _imageProcessor.GetImage("prev_pc13", false, 1.0f);
                            imageBubble4 = _imageProcessor.GetImage("prev_pc04", false, 1.0f);
                            imageBubbleThis = _imageProcessor.GetImage("prev_event", false, 1.0f);
                            break;
                        case "Bubble(Member4)":
                            imageBubble1 = _imageProcessor.GetImage("prev_pc01", false, 1.0f);
                            imageBubble2 = _imageProcessor.GetImage("prev_pc02", false, 1.0f);
                            imageBubble3 = _imageProcessor.GetImage("prev_pc03", false, 1.0f);
                            imageBubble4 = _imageProcessor.GetImage("prev_pc14", false, 1.0f);
                            imageBubbleThis = _imageProcessor.GetImage("prev_event", false, 1.0f);
                            break;
                        case "Bubble(Event)":
                            imageBubble3 = _imageProcessor.GetImage("prev_event3", false, 1.0f);
                            if (comboBoxSpeechBubble.SelectedIndex <= 0)
                            {
                                ;
                            }
                            else if (!String.IsNullOrEmpty(_comboBoxItems[comboBoxSpeechBubble.SelectedIndex].ImagePath))
                            {
                                imageBubble3B = _imageProcessor.GetImage(_comboBoxItems[comboBoxSpeechBubble.SelectedIndex].ImagePath, false, 1.0f);
                            }
                            else
                            {
                                imageBubble3B = _imageProcessor.GetImage("prev_event", false, 1.0f);
                            }
                            break;
                    }
                }
                    var imagesToMerge = new List<(Image<Rgba32>, ImagePositionAndSize)>();
                if (imageCast1 != null)
                {
                    imagesToMerge.Add((imageCast1, new ImagePositionAndSize { Position = new SixLabors.ImageSharp.Point(0, 256), Size = new SixLabors.ImageSharp.Size(512, 512) }));
                }
                if (imageCast2 != null)
                {
                    imagesToMerge.Add((imageCast2, new ImagePositionAndSize { Position = new SixLabors.ImageSharp.Point(768, 256), Size = new SixLabors.ImageSharp.Size(512, 512) }));
                }
                if (imageBubble1 != null)
                {
                    imagesToMerge.Add((imageBubble1, new ImagePositionAndSize { Position = new SixLabors.ImageSharp.Point(0, 512), Size = new SixLabors.ImageSharp.Size(256, 256) }));
                }
                if (imageBubble2 != null)
                {
                    imagesToMerge.Add((imageBubble2, new ImagePositionAndSize { Position = new SixLabors.ImageSharp.Point(256, 512), Size = new SixLabors.ImageSharp.Size(256, 256) }));
                }
                if (imageBubble3B != null)
                {
                    imagesToMerge.Add((imageBubble3B, new ImagePositionAndSize { Position = new SixLabors.ImageSharp.Point(512, 512), Size = new SixLabors.ImageSharp.Size(256, 256) }));
                }
                if (imageBubble3 != null)
                {
                    imagesToMerge.Add((imageBubble3, new ImagePositionAndSize { Position = new SixLabors.ImageSharp.Point(512, 512), Size = new SixLabors.ImageSharp.Size(256, 256) }));
                }
                if (imageBubble4 != null)
                {
                    imagesToMerge.Add((imageBubble4, new ImagePositionAndSize { Position = new SixLabors.ImageSharp.Point(768, 512), Size = new SixLabors.ImageSharp.Size(256, 256) }));
                }
                if (imageBubbleThis != null)
                {
                    imagesToMerge.Add((imageBubbleThis, new ImagePositionAndSize { Position = new SixLabors.ImageSharp.Point(1024, 512), Size = new SixLabors.ImageSharp.Size(256, 256) }));
                }
                if (imageBubbleEvent != null)
                {
                    imagesToMerge.Add((imageBubbleEvent, new ImagePositionAndSize { Position = new SixLabors.ImageSharp.Point(512, 512), Size = new SixLabors.ImageSharp.Size(256, 256) }));
                }

                using (var mergedImage = _imageProcessor.MergeImages(imagesToMerge))
                {
                    _drawingMergedImage?.Dispose();
                    _drawingMergedImage = _imageProcessor.GetAsDrawingImage(mergedImage);
                }
                panelPreview.Invalidate();
            }
            finally
            {
                imageCast1?.Dispose();
                imageCast2?.Dispose();
                imageBubble1?.Dispose();
                imageBubble2?.Dispose();
                imageBubble3?.Dispose();
                imageBubble4?.Dispose();
                imageBubbleThis?.Dispose();
                imageBubbleEvent?.Dispose();
                imageBubble3B?.Dispose();
            }

            prevTextBoxNPLCR.Visible = false;
            prevTextBoxActCast1.Visible = false;
            prevTextBoxActCast2.Visible = false;
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

            float relativeX = 175.0f / 960.0f;
            float relativeY = 400.0f / 540.0f;
            float relativeX2 = 175.0f / 960.0f;
            float relativeY2 = 365.0f / 540.0f;
            string windowPosition = comboBoxWindowPosition.SelectedItem?.ToString() ?? "Down";
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
                await prevCustomRichTextBoxText.StreamText(textBoxText.Text, 20, checkBoxAutoScroll.Checked, cancelTokenSource.Token, skipTokenSource.Token);
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

        private void panelPreview_SizeChanged(object sender, EventArgs e)
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();
            }
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
            string shortcutKey = ShortcutKeyManager.KeyCodeToString(e);

            foreach (var action in _shortcutKeyBindings)
            {
                if (action.ShortcutKey == shortcutKey)
                {
                    switch (action.FunctionName)
                    {
                        case "Duplicate row":
                            buttonDuplicateRow.PerformClick();
                            break;
                        case "Preview":
                            buttonPreview.PerformClick();
                            break;
                        case "Reload":
                            buttonReload.PerformClick();
                            break;
                        case "Skip Streaming":
                            buttonStreamSkip.PerformClick();
                            break;
                        case "Next row":
                            buttonNextRow.PerformClick();
                            break;
                        case "Previous row":
                            buttonPreviousRow.PerformClick();
                            break;
                        case "Move up":
                            buttonMoveUp.PerformClick();
                            break;
                        case "Move down":
                            buttonMoveDown.PerformClick();
                            break;
                        case "Apply":
                            buttonApply.PerformClick();
                            break;
                        case "Tag":
                            comboBoxTagType.Focus();
                            break;
                        case "NPL":
                            textBoxNPL.Focus();
                            textBoxNPL.SelectionStart = textBoxNPL.Text.Length;
                            break;
                        case "NPC":
                            textBoxNPC.Focus();
                            textBoxNPC.SelectionStart = textBoxNPC.Text.Length;
                            break;
                        case "NPR":
                            textBoxNPR.Focus();
                            textBoxNPR.SelectionStart = textBoxNPR.Text.Length;
                            break;
                        case "Text":
                            textBoxText.Focus();
                            textBoxText.SelectionStart = textBoxText.Text.Length;
                            break;
                        case "Talk cast1":
                            radioButtonTalkCast1.PerformClick();
                            break;
                        case "Talk cast2":
                            radioButtonTalkCast2.PerformClick();
                            break;
                        case "Cast1":
                            comboBoxCast1.Focus();
                            break;
                        case "Cast2":
                            comboBoxCast2.Focus();
                            break;
                        case "Input Helper":
                            Point position = textBoxText.GetPositionFromCharIndex(textBoxText.SelectionStart);
                            position = this.PointToScreen(position);
                            contextMenuStripInputHelper.Show(position);
                            break;
                        case "Save":
                            if (cancelTokenSource != null)
                            {
                                cancelTokenSource.Cancel();
                            }
                            UpdateDataFromFields();
                            DataChanged?.Invoke(this, _data);
                            CSVSaveRequested?.Invoke(this, new SaveCSVEventArgs(true));
                            break;
                        case "Save As...":
                            if (cancelTokenSource != null)
                            {
                                cancelTokenSource.Cancel();
                            }
                            UpdateDataFromFields();
                            DataChanged?.Invoke(this, _data);
                            CSVSaveRequested?.Invoke(this, new SaveCSVEventArgs(false));
                            break;
                    }
                    e.SuppressKeyPress = true;
                    return;
                }
            }
        }

        public void UpdateToolTipKeys()
        {
            Dictionary<string, Control> controlDictionary = new Dictionary<string, Control>()
            {
                { "Duplicate row", buttonDuplicateRow },
                { "Preview", buttonPreview },
                { "Reload", buttonReload },
                { "Skip Streaming", buttonStreamSkip },
                { "Next row", buttonNextRow },
                { "Previous row", buttonPreviousRow },
                { "Move up", buttonMoveUp },
                { "Move down", buttonMoveDown },
                { "Apply", buttonApply },
                { "Tag", comboBoxTagType },
                { "NPL", textBoxNPL },
                { "NPC", textBoxNPC },
                { "NPR", textBoxNPR },
                { "Text", textBoxText },
                { "Talk cast1", radioButtonTalkCast1 },
                { "Talk cast2", radioButtonTalkCast2 },
                { "Cast1", comboBoxCast1 },
                { "Cast2", comboBoxCast2 },
            };

            foreach (var action in _shortcutKeyBindings)
            {
                if (controlDictionary.TryGetValue(action.FunctionName, out var control))
                {
                    toolTipKeys.SetToolTip(control, $"{action.FunctionName} ({action.ShortcutKey})");
                }
            }
        }

        private void buttonShortcutKeysSetting_Click(object sender, EventArgs e)
        {
            FormKeysSetting keysettingForm = new FormKeysSetting(_shortcutKeyBindings);
            if (keysettingForm.ShowDialog() == DialogResult.OK)
            {
                _shortcutKeyBindings = keysettingForm.GetShortcutKeyBindings();
                UpdateToolTipKeys();
            }
        }
    }
}
