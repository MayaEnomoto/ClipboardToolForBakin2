using ClipboardToolForBakin;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace ClipboardToolForBakin2
{
    public partial class FormComboBoxConfig : Form
    {
        private List<ResourceItem> _comboBoxItems;
        private const int NumberOfItems = 32;

        public FormComboBoxConfig(List<ResourceItem> comboBoxItems)
        {
            InitializeComponent();
            _comboBoxItems = comboBoxItems;
            GenerateInputFields();
            PopulateFieldsWithExistingItems();
        }

        private void GenerateInputFields()
        {
            for (int i = 0; i < NumberOfItems; i++)
            {
                FlowLayoutPanel innerPanel = new FlowLayoutPanel();
                innerPanel.AutoSize = true;
                innerPanel.FlowDirection = FlowDirection.LeftToRight;
                outerPanel.Controls.Add(innerPanel);

                Label nameLabel = new Label()
                {
                    Name = $"nameLabel{i}",
                    Text = $"Resource{i}",
                    AutoSize = true,
                    Margin = new Padding(10)
                };
                innerPanel.Controls.Add(nameLabel);

                TextBox keyBox = new TextBox()
                {
                    Name = $"keyBox{i}",
                    Width = 200,
                    Margin = new Padding(10)
                };
                innerPanel.Controls.Add(keyBox);

                TextBox valueBox = new TextBox()
                {
                    Name = $"valueBox{i}",
                    Width = 500,
                    Margin = new Padding(10)
                };
                valueBox.KeyPress += (sender, e) =>
                {
                    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = !(char.IsDigit(e.KeyChar) || (e.KeyChar >= 'a' && e.KeyChar <= 'f') ||
                                      (e.KeyChar >= 'A' && e.KeyChar <= 'F') || e.KeyChar == (char)Keys.Back);
                    }
                };
                valueBox.TextChanged += (sender, e) =>
                {
                    if (valueBox.Text.Replace("-", "").Length > 32)
                    {
                        int cursorPos = valueBox.SelectionStart - 1;
                        valueBox.Text = valueBox.Text.Remove(cursorPos, 1);
                        valueBox.SelectionStart = cursorPos;
                    }
                };
                valueBox.Leave += (sender, e) =>
                {
                    if (valueBox.Text == String.Empty) return;
                    string paddedText = valueBox.Text.Replace("-", "").PadLeft(32, '0').ToUpper();
                    string result = Regex.Replace(paddedText, ".{8}", "$0-").TrimEnd('-');
                    valueBox.Text = result;
                    valueBox.SelectionStart = valueBox.Text.Length;
                };
                ToolStripMenuItem paste1Item = new ToolStripMenuItem("Get Cast1 UUID");
                paste1Item.Click += (sender, e) =>
                {
                    valueBox.Text = ProcessDataForPaste1();
                };
                ToolStripMenuItem paste2Item = new ToolStripMenuItem("Get Cast2 UUID");
                paste2Item.Click += (sender, e) =>
                {
                    valueBox.Text = ProcessDataForPaste2();
                };
                ToolStripMenuItem paste3Item = new ToolStripMenuItem("Get Event UUID");
                paste3Item.Click += (sender, e) =>
                {
                    valueBox.Text = ProcessDataForPaste3();
                };
                ContextMenuStrip cms = new ContextMenuStrip();
                cms.Items.Add(paste1Item);
                cms.Items.Add(paste2Item);
                cms.Items.Add(paste3Item);
                cms.Opening += (sender, e) =>
                {
                    paste1Item.Enabled = false;
                    paste2Item.Enabled = false;
                    paste3Item.Enabled = false;
                    if (Clipboard.ContainsData("Yukar2ScriptCommands"))
                    {
                        paste1Item.Enabled = checkStatusPaste1();
                        paste2Item.Enabled = checkStatusPaste2();
                        paste3Item.Enabled = checkStatusPaste3();
                    }
                };
                valueBox.ContextMenuStrip = cms;
                innerPanel.Controls.Add(valueBox);

                TextBox filePathBox = new TextBox()
                {
                    Name = $"filePathBox{i}",
                    Width = 200,
                    Margin = new Padding(10)
                };
                innerPanel.Controls.Add(filePathBox);

                PictureBox pictureBox = new PictureBox()
                {
                    Name = $"pictureBox{i}",
                    Width = 100,
                    Height = 100,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BorderStyle = BorderStyle.Fixed3D,
                    Margin = new Padding(10)
                };
                innerPanel.Controls.Add(pictureBox);

                Button browseButton = new Button()
                {
                    Name = $"browseButton{i}",
                    Text = "Browse",
                    AutoSize = true,
                    Margin = new Padding(10)
                };
                browseButton.Click += (sender, e) =>
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        try
                        {
                            pictureBox.Image = new Bitmap(openFileDialog.FileName);
                            filePathBox.Text = openFileDialog.FileName;
                        }
                        catch (Exception)
                        {
                            filePathBox.Text = string.Empty;
                            MessageBox.Show("Error opening the image file. Please select a valid PNG file.");
                        }
                    }
                };
                innerPanel.Controls.Add(browseButton);
            }
        }

        private void PopulateFieldsWithExistingItems()
        {
            bool isImagePathError = false;
            for (int i = 0; i < NumberOfItems; i++)
            {
                var keyBox = Controls.Find($"keyBox{i}", true).FirstOrDefault() as TextBox;
                var valueBox = Controls.Find($"valueBox{i}", true).FirstOrDefault() as TextBox;
                var filePathBox = Controls.Find($"filePathBox{i}", true).FirstOrDefault() as TextBox;
                var pictureBox = Controls.Find($"pictureBox{i}", true).FirstOrDefault() as PictureBox;

                string key = i < _comboBoxItems.Count ? _comboBoxItems[i].Key : string.Empty;
                string value = i < _comboBoxItems.Count ? _comboBoxItems[i].Value : string.Empty;
                string imagePath = i < _comboBoxItems.Count ? _comboBoxItems[i].ImagePath : string.Empty;

                if (keyBox != null)
                {
                    keyBox.Text = key;
                }

                if (valueBox != null)
                {
                    valueBox.Text = value;
                }

                if (filePathBox != null)
                {
                    filePathBox.Text = imagePath;
                }

                if (pictureBox != null && !string.IsNullOrEmpty(imagePath))
                {
                    if (System.IO.File.Exists(imagePath))
                    {
                        pictureBox.Image = new Bitmap(imagePath);
                    }
                    else
                    {
                        pictureBox.Image = null;
                        isImagePathError = true;
                    }
                }
                else if (pictureBox != null)
                {
                    pictureBox.Image = null;
                }

                if (keyBox != null && valueBox != null && filePathBox != null)
                {
                    if (i == 0 || i == 1)
                    {
                        keyBox.Enabled = false;
                        valueBox.Enabled = false;
                        filePathBox.Enabled = false;
                        var browseButton = Controls.Find($"browseButton{i}", true).FirstOrDefault() as Button;
                        if (browseButton != null)
                        {
                            browseButton.Enabled = false;
                        }
                    }
                }
            }
            if (isImagePathError == true)
            {
                MessageBox.Show($"Failed to load image file.\r\nPlease correct the file path or delete the file path.");
            }
        }

        private void UpdateComboBoxItems()
        {
            for (int i = 0; i < NumberOfItems; i++)
            {
                var keyBox = Controls.Find($"keyBox{i}", true).FirstOrDefault() as TextBox;
                var valueBox = Controls.Find($"valueBox{i}", true).FirstOrDefault() as TextBox;
                var filePathBox = Controls.Find($"filePathBox{i}", true).FirstOrDefault() as TextBox;
                string key = keyBox != null ? keyBox.Text : string.Empty;
                string value = valueBox != null ? valueBox.Text : string.Empty;
                string imagePath = filePathBox != null ? filePathBox.Text : string.Empty;

                if (i < _comboBoxItems.Count)
                {
                    _comboBoxItems[i] = new ResourceItem(key, value, imagePath);
                }
                else
                {
                    _comboBoxItems.Add(new ResourceItem(key, value, imagePath));
                }
            }
        }

        public List<ResourceItem> GetItems()
        {
            return _comboBoxItems;
        }

        public static void InitComboBoxItems(List<ResourceItem> comboBoxItems)
        {
            if (comboBoxItems.Count == 0 || (comboBoxItems[0].Key != "Unassigned" && comboBoxItems[0].Value != "00000000-00000000-00000000-00000000"))
            {
                comboBoxItems.Insert(0, new ResourceItem("Unassigned", "00000000-00000000-00000000-00000000", string.Empty));
            }
            if (comboBoxItems.Count == 1 || (comboBoxItems[1].Key != "Direct reference to UUID" && comboBoxItems[1].Value != string.Empty))
            {
                comboBoxItems.Insert(1, new ResourceItem("Direct reference to UUID", string.Empty, string.Empty));
            }
        }

        private bool checkStatusPaste1()
        {
            var data = BakinPanelData.GetClipBoardData();
            if (!data.Any()) return false;
            var firstData = data.First();
            if (data.Count() != 1) return false;
            if (firstData.Tag != "Talk") return false;
            if (string.IsNullOrWhiteSpace(firstData.Cast1) || firstData.Cast1 == "00000000-00000000-00000000-00000000") return false;
            return true;
        }

        private bool checkStatusPaste2()
        {
            var data = BakinPanelData.GetClipBoardData();
            if (!data.Any()) return false;
            var firstData = data.First();
            if (data.Count() != 1) return false;
            if (firstData.Tag != "Talk") return false;
            if (string.IsNullOrWhiteSpace(firstData.Cast2) || firstData.Cast2 == "00000000-00000000-00000000-00000000") return false;
            return true;
        }

        private bool checkStatusPaste3()
        {
            var data = BakinPanelData.GetClipBoardData();
            if (!data.Any()) return false;
            var firstData = data.First();
            if (data.Count() != 1) return false;
            if (firstData.Tag != "Talk") return false;
            if (string.IsNullOrWhiteSpace(firstData.SpeechBubble) || firstData.SpeechBubble == "00000000-00000000-00000000-00000000") return false;
            return true;
        }

        private string ProcessDataForPaste1()
        {
            var data = BakinPanelData.GetClipBoardData();
            if (!data.Any()) return String.Empty;
            var firstData = data.First();

            return firstData.Cast1;
        }

        private string ProcessDataForPaste2()
        {
            var data = BakinPanelData.GetClipBoardData();
            if (!data.Any()) return String.Empty;
            var firstData = data.First();
            return firstData.Cast2;
        }

        private string ProcessDataForPaste3()
        {
            var data = BakinPanelData.GetClipBoardData();
            if (!data.Any()) return string.Empty;
            var firstData = data.First();
            if (data.Count() != 1) return string.Empty;
            if (firstData.Tag != "Talk") return string.Empty;
            return firstData.SpeechBubble;
        }

        public List<ResourceItem> LoadItemsFromJsonFile(string filePath)
        {
            try
            {
                var jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<ResourceItem>>(jsonString) ?? new List<ResourceItem>();
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"An error occurred while loading the file: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<ResourceItem>();
            }
        }

        public void SaveItemsToJsonFile(List<ResourceItem> items, string filePath)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };
                var jsonString = JsonSerializer.Serialize(items, options);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the file: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            UpdateComboBoxItems();
            this.DialogResult = DialogResult.OK;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _comboBoxItems = LoadItemsFromJsonFile(openFileDialog.FileName);
                PopulateFieldsWithExistingItems();
            }
        }

        private void buttonSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                UpdateComboBoxItems();
                SaveItemsToJsonFile(_comboBoxItems, saveFileDialog.FileName);
            }
        }

        private void FormComboBoxConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JSON files (*.json)|*.json";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    UpdateComboBoxItems();
                    SaveItemsToJsonFile(_comboBoxItems, saveFileDialog.FileName);
                }
            }
        }
    }
}
