using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ClipboardToolForBakin2;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using static ClipboardToolForBakin.BakinPanelData;
using static ClipboardToolForBakin2.StringUtil;

namespace ClipboardToolForBakin
{
    public partial class MainForm : Form
    {
        private string _currentFilePath = "";
        private BindingSource _bindingSource;
        private BindingList<BakinPanelData.RowData> _dataList;
        private Rectangle dragBox;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;
        private string lastClipboardText = string.Empty;
        private System.Windows.Forms.Timer clipboardCheckTimer;
        private FormPreviewEditor _FormPreviewEditor = null;
        private List<ResourceItem> _comboBoxItems = new List<ResourceItem>();

        public MainForm()
        {
            InitializeComponent();
            InitializeDataGrid();
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.Name != "Tag" && column.Name != "Text" && column.Name != "Memo")
                {
                    column.Visible = false;
                }
            }

            lastClipboardText = Clipboard.GetText();
            buttonPasteFromClipboard.Enabled = Clipboard.ContainsData("Yukar2ScriptCommands");
            clipboardCheckTimer = new System.Windows.Forms.Timer();
            clipboardCheckTimer.Interval = 500;
            clipboardCheckTimer.Tick += CheckClipboard;
            clipboardCheckTimer.Start();
            buttonPreviewEditor.Enabled = false;
            buttonDeleteRows.Enabled = false;
            buttonCopyToClipboard.Enabled = false;

            FormComboBoxConfig.InitComboBoxItems(_comboBoxItems);
            UpdateWindowTitle();
        }

        private void InitializeDataGrid()
        {
            int newDefaultRowHeight = 40;
            dataGridView.RowTemplate.Height = newDefaultRowHeight;

            _dataList = new BindingList<BakinPanelData.RowData>();
            _bindingSource = new BindingSource { DataSource = _dataList };
            dataGridView.DataSource = _bindingSource;
            InitializeDataGridSetting();
        }

        private void InitializeDataGridSetting()
        {
            DataGridViewComboBoxColumn tagColumn = new DataGridViewComboBoxColumn
            {
                Name = "Tag",
                HeaderText = "Tag",
                DataSource = new string[] { "Talk", "Message", "Notes" },
                DataPropertyName = "Tag",
                ValueType = typeof(string),
                DefaultCellStyle = new DataGridViewCellStyle { NullValue = "Talk" }
            };
            int columnIndex = dataGridView.Columns["Tag"].Index;
            dataGridView.Columns.Remove("Tag");
            dataGridView.Columns.Insert(columnIndex, tagColumn);

            DataGridViewComboBoxColumn talkCastColumn = new DataGridViewComboBoxColumn
            {
                Name = "TalkCast",
                HeaderText = "TalkCast",
                DataSource = new string[] { "1", "2" },
                DataPropertyName = "TalkCast",
                ValueType = typeof(string),
                DefaultCellStyle = new DataGridViewCellStyle { NullValue = "1" }
            };
            columnIndex = dataGridView.Columns["talkCast"].Index;
            dataGridView.Columns.Remove("talkCast");
            dataGridView.Columns.Insert(columnIndex, talkCastColumn);

            var mirrorCast1Column = new DataGridViewCheckBoxColumn
            {
                Name = "MirrorCast1",
                HeaderText = "MirrorCast1",
                DataPropertyName = "MirrorCast1",
                ValueType = typeof(bool),
                DefaultCellStyle = new DataGridViewCellStyle { NullValue = false }
            };
            columnIndex = dataGridView.Columns["MirrorCast1"].Index;
            dataGridView.Columns.Remove("MirrorCast1");
            dataGridView.Columns.Insert(columnIndex, mirrorCast1Column);

            var mirrorCast2Column = new DataGridViewCheckBoxColumn
            {
                Name = "MirrorCast2",
                HeaderText = "MirrorCast2",
                DataPropertyName = "MirrorCast2",
                ValueType = typeof(bool),
                DefaultCellStyle = new DataGridViewCellStyle { NullValue = false }
            };
            columnIndex = dataGridView.Columns["MirrorCast2"].Index;
            dataGridView.Columns.Remove("MirrorCast2");
            dataGridView.Columns.Insert(columnIndex, mirrorCast2Column);

            var billboard1Column = new DataGridViewCheckBoxColumn
            {
                Name = "Billboard1",
                HeaderText = "Billboard1",
                DataPropertyName = "Billboard1",
                ValueType = typeof(bool),
                DefaultCellStyle = new DataGridViewCellStyle { NullValue = false }
            };
            columnIndex = dataGridView.Columns["Billboard1"].Index;
            dataGridView.Columns.Remove("Billboard1");
            dataGridView.Columns.Insert(columnIndex, billboard1Column);

            var billboard2Column = new DataGridViewCheckBoxColumn
            {
                Name = "Billboard2",
                HeaderText = "Billboard2",
                DataPropertyName = "Billboard2",
                ValueType = typeof(bool),
                DefaultCellStyle = new DataGridViewCellStyle { NullValue = false }
            };
            columnIndex = dataGridView.Columns["Billboard2"].Index;
            dataGridView.Columns.Remove("Billboard2");
            dataGridView.Columns.Insert(columnIndex, billboard2Column);

            var windowVisibletColumn = new DataGridViewCheckBoxColumn
            {
                Name = "WindowVisible",
                HeaderText = "WindowVisible",
                DataPropertyName = "WindowVisible",
                ValueType = typeof(bool),
                DefaultCellStyle = new DataGridViewCellStyle { NullValue = false }
            };
            columnIndex = dataGridView.Columns["WindowVisible"].Index;
            dataGridView.Columns.Remove("WindowVisible");
            dataGridView.Columns.Insert(columnIndex, windowVisibletColumn);

            var windowPositionColumn = new DataGridViewComboBoxColumn
            {
                Name = "WindowPosition",
                HeaderText = "WindowPosition",
                DataSource = new string[] { "Up", "Center", "Down", "Bubble(Player)", "Bubble(ThisEvent)", "Bubble(Member2)", "Bubble(Member3)", "Bubble(Member4)", "Bubble(Event)" },
                DataPropertyName = "WindowPosition",
                ValueType = typeof(string),
                DefaultCellStyle = new DataGridViewCellStyle { NullValue = "Down" }
            };
            columnIndex = dataGridView.Columns["WindowPosition"].Index;
            dataGridView.Columns.Remove("WindowPosition");
            dataGridView.Columns.Insert(columnIndex, windowPositionColumn);

            var useMapLightColumn = new DataGridViewCheckBoxColumn
            {
                Name = "UseMapLight",
                HeaderText = "UseMapLight",
                DataPropertyName = "UseMapLight",
                ValueType = typeof(bool),
                DefaultCellStyle = new DataGridViewCellStyle { NullValue = false }
            };
            columnIndex = dataGridView.Columns["UseMapLight"].Index;
            dataGridView.Columns.Remove("UseMapLight");
            dataGridView.Columns.Insert(columnIndex, useMapLightColumn);
        }

        private class CsvRowDataMap : ClassMap<BakinPanelData.RowData>
        {
            public CsvRowDataMap()
            {
                Map(m => m.Tag).Name("Tag").TypeConverter<TagConverter>().Optional().Default("Talk");
                Map(m => m.Text).Name("text").Optional().Default(string.Empty);
                Map(m => m.NPL).Name("NPL").Optional().Default(string.Empty);
                Map(m => m.NPC).Name("NPC").Optional().Default(string.Empty);
                Map(m => m.NPR).Name("NPR").Optional().Default(string.Empty);
                Map(m => m.blspd).Name("blspd").Optional().Default("0");
                Map(m => m.blrate).Name("blrate").Optional().Default("0");
                Map(m => m.lipspd).Name("lipspd").Optional().Default("0.0");
                Map(m => m.Cast1).Name("Cast1").TypeConverter<CastValueConverter>().Optional().Default("00000000-00000000-00000000-00000000");
                Map(m => m.ActCast1).Name("ActCast1").TypeConverter<ActCastValueConverter>().Optional().Default("----");
                Map(m => m.Cast2).Name("Cast2").TypeConverter<CastValueConverter>().Optional().Default("00000000-00000000-00000000-00000000");
                Map(m => m.ActCast2).Name("ActCast2").TypeConverter<ActCastValueConverter>().Optional().Default("----");
                Map(m => m.TalkCast).Name("TalkCast").Optional().Default("1");
                Map(m => m.MirrorCast1).Name("MirrorCast1").TypeConverter<BoolToStringConverter>().Optional().Default("off");
                Map(m => m.MirrorCast2).Name("MirrorCast2").TypeConverter<BoolToStringConverter>().Optional().Default("off");
                Map(m => m.Billboard1).Name("Billboard1").TypeConverter<BoolToStringConverter>().Optional().Default("off");
                Map(m => m.Billboard2).Name("Billboard2").TypeConverter<BoolToStringConverter>().Optional().Default("off");
                Map(m => m.WindowVisible).Name("WindowVisible").TypeConverter<BoolToStringConverter>().Optional().Default("off");
                Map(m => m.WindowPosition).Name("WindowPosition").Optional().Default("Down");
                Map(m => m.SpeechBubble).Name("SpeechBubble").TypeConverter<CastValueConverter>().Optional().Default("00000000-00000000-00000000-00000000");
                Map(m => m.UseMapLight).Name("UseMapLight").TypeConverter<BoolToStringConverter>().Optional().Default("off");
                Map(m => m.Memo).Name("Memo").Optional().Default(string.Empty);
            }
        }

        public class TagConverter : DefaultTypeConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                string[] validTags = { "Talk", "Message", "Notes" };
                if (!validTags.Contains(text.Trim(), StringComparer.InvariantCultureIgnoreCase))
                {
                    return "Notes";
                }
                return text.Trim();
            }
        }

        public class CastValueConverter : DefaultTypeConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (!IsValidHEXFormat(text))
                {
                    return "00000000-00000000-00000000-00000000";
                }
                return text;
            }
        }

        public class ActCastValueConverter : DefaultTypeConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (!IsInRangeASCII(text))
                {
                    return "----";
                }
                return text;
            }
        }

        public class BoolToStringConverter : DefaultTypeConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    return false;
                }
                if (text.Trim().Equals("On", StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
                else if (text.Trim().Equals("Off", StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        private void ReadCsv(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            using (CsvReader csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower()
            }))
            {
                csv.Context.RegisterClassMap<CsvRowDataMap>();
                List<BakinPanelData.RowData> records = null;
                try
                {
                    records = csv.GetRecords<BakinPanelData.RowData>().ToList();
                }
                catch (ReaderException ex)
                {
                    MessageBox.Show($"Error reading CSV file: {ex.InnerException?.Message ?? ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (var record in records)
                {
                    if (!string.IsNullOrEmpty(record.Text))
                    {
                        string tempReplacement = "<__TEMP_NEWLINE__>";
                        record.Text = record.Text.Replace("\\n", tempReplacement);
                        record.Text = record.Text.Replace("\r\n", "\\n").Replace("\r", "\\n").Replace("\n", "\\n");
                        record.Text = record.Text.Replace(tempReplacement, "\\n");
                    }
                }
                _dataList = new BindingList<BakinPanelData.RowData>(records);
                _bindingSource.DataSource = _dataList;
                dataGridView.DataSource = _bindingSource;
                dataGridView.AutoResizeColumns();
                dataGridView.AutoResizeRows();
            }
        }

        private void WriteCsv(string filePath)
        {
            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvRowDataMap>();
            csv.WriteRecords(_dataList);
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _currentFilePath = openFileDialog.FileName;
                try
                {
                    ReadCsv(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening file: " + ex.Message);
                    _currentFilePath = "";
                }
                UpdateWindowTitle();
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCSV();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsCSV();
        }

        private void buttonCopyToClipboard_Click(object sender, EventArgs e)
        {
            var selectedRowCount = dataGridView.SelectedRows.Count;
            if (selectedRowCount >= 1)
            {
                List<BakinPanelData.RowData> selectedRows = new List<BakinPanelData.RowData>();
                for (int i = 0; i < selectedRowCount; i++)
                {
                    SetDefaultValues(dataGridView.SelectedRows[i]);
                    var selectedRow = dataGridView.SelectedRows[i];

                    StringData selectedData = new StringData
                    {
                        NPL = selectedRow.Cells["NPL"].Value?.ToString() ?? string.Empty,
                        NPC = selectedRow.Cells["NPC"].Value?.ToString() ?? string.Empty,
                        NPR = selectedRow.Cells["NPR"].Value?.ToString() ?? string.Empty,
                        text = selectedRow.Cells["Text"].Value?.ToString() ?? string.Empty
                    };

                    int blspd;
                    if (int.TryParse(selectedRow.Cells["blspd"].Value?.ToString(), out blspd))
                    {
                        selectedData.Blspd = blspd;
                    }

                    int blrate;
                    if (int.TryParse(selectedRow.Cells["blrate"].Value?.ToString(), out blrate))
                    {
                        selectedData.Blrate = blrate;
                    }

                    float lipspd;
                    if (float.TryParse(selectedRow.Cells["lipspd"].Value?.ToString(), out lipspd))
                    {
                        selectedData.Lipspd = lipspd;
                    }

                    string combinedText = selectedRow.Cells["Text"].Value?.ToString() ?? string.Empty;
                    string cellValue = selectedRow.Cells["Tag"].Value?.ToString() ?? string.Empty;
                    if (cellValue == "Talk" || cellValue == "Memo")
                    {
                        combinedText = CombineString(selectedData);
                    }
                    else
                    {
                        ;
                    }

                    var rowData = new BakinPanelData.RowData
                    {
                        //Tag = selectedRow.Cells["Tag"].Value?.ToString() ?? string.Empty,
                        //Text = selectedRow.Cells["Text"].Value?.ToString() ?? string.Empty,
                        Tag = cellValue,
                        Text = combinedText,
                        Cast1 = selectedRow.Cells["Cast1"].Value?.ToString() ?? string.Empty,
                        ActCast1 = selectedRow.Cells["ActCast1"].Value?.ToString() ?? string.Empty,
                        Cast2 = selectedRow.Cells["Cast2"].Value?.ToString() ?? string.Empty,
                        ActCast2 = selectedRow.Cells["ActCast2"].Value?.ToString() ?? string.Empty,
                        TalkCast = selectedRow.Cells["TalkCast"].Value?.ToString() ?? string.Empty,
                        MirrorCast1 = (bool)(selectedRow.Cells["MirrorCast1"].Value ?? false),
                        MirrorCast2 = (bool)(selectedRow.Cells["MirrorCast2"].Value ?? false),
                        Billboard1 = (bool)(selectedRow.Cells["Billboard1"].Value ?? false),
                        Billboard2 = (bool)(selectedRow.Cells["Billboard2"].Value ?? false),
                        WindowVisible = (bool)(selectedRow.Cells["WindowVisible"].Value ?? false),
                        WindowPosition = selectedRow.Cells["WindowPosition"].Value?.ToString() ?? string.Empty,
                        SpeechBubble = selectedRow.Cells["SpeechBubble"].Value?.ToString() ?? string.Empty,
                        UseMapLight = (bool)(selectedRow.Cells["UseMapLight"].Value ?? false),
                    };
                    selectedRows.Add(rowData);
                }
                BakinPanelData.SetClipBoard(selectedRows);
            }
        }

        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["ActCast1"].Index ||
                e.ColumnIndex == dataGridView.Columns["ActCast2"].Index)
            {
                string input = e.FormattedValue.ToString();
                if (!IsInRangeASCII(input))
                {
                    e.Cancel = true;
                }
            }
        }

        private static bool IsInRangeASCII(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }

            foreach (char c in input)
            {
                if (!(char.IsLetterOrDigit(c)))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsValidHEXFormat(string input)
        {
            Regex pattern = new Regex(@"^([0-9A-Fa-f]{8}-){3}[0-9A-Fa-f]{8}$");
            return pattern.IsMatch(input);
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["Cast1"].Index ||
                e.ColumnIndex == dataGridView.Columns["Cast2"].Index ||
                e.ColumnIndex == dataGridView.Columns["SpeechBubble"].Index)
            {
                ValidateCastCellValue(e.RowIndex, e.ColumnIndex);
            }
            if (e.ColumnIndex == dataGridView.Columns["ActCast1"].Index ||
                e.ColumnIndex == dataGridView.Columns["ActCast2"].Index)
            {
                ValidateActCastCellValue(e.RowIndex, e.ColumnIndex);
            }
            if (e.ColumnIndex == dataGridView.Columns["Text"].Index)
            {
                string cellValue = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (!string.IsNullOrEmpty(cellValue))
                {
                    string tempReplacement = "<__TEMP_NEWLINE__>";
                    cellValue = cellValue.Replace("\\n", tempReplacement);
                    cellValue = cellValue.Replace("\r\n", "\\n").Replace("\r", "\\n").Replace("\n", "\\n");
                    cellValue = cellValue.Replace(tempReplacement, "\\n");
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cellValue;
                }
            }
        }

        private void SetDefaultValues(DataGridViewRow row)
        {
            DataGridViewCell cast1Cell = row.Cells["Cast1"];
            DataGridViewCell cast2Cell = row.Cells["Cast2"];
            DataGridViewCell actCast1Cell = row.Cells["ActCast1"];
            DataGridViewCell actCast2Cell = row.Cells["ActCast2"];
            DataGridViewCell speechBubble = row.Cells["SpeechBubble"];

            if (cast1Cell.Value == null || string.IsNullOrWhiteSpace(cast1Cell.Value.ToString()))
            {
                cast1Cell.Value = "00000000-00000000-00000000-00000000";
            }
            if (cast2Cell.Value == null || string.IsNullOrWhiteSpace(cast2Cell.Value.ToString()))
            {
                cast2Cell.Value = "00000000-00000000-00000000-00000000";
            }
            if (actCast1Cell.Value == null || string.IsNullOrWhiteSpace(actCast1Cell.Value.ToString()))
            {
                actCast1Cell.Value = "----";
            }
            if (actCast2Cell.Value == null || string.IsNullOrWhiteSpace(actCast2Cell.Value.ToString()))
            {
                actCast2Cell.Value = "----";
            }
            if (speechBubble.Value == null || string.IsNullOrWhiteSpace(speechBubble.Value.ToString()))
            {
                speechBubble.Value = "00000000-00000000-00000000-00000000";
            }
        }

        private void ValidateCastCellValue(int rowIndex, int columnIndex)
        {
            DataGridViewCell cell = dataGridView.Rows[rowIndex].Cells[columnIndex];
            string cellValue = cell.Value?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(cellValue))
            {
                cell.Value = "00000000-00000000-00000000-00000000";
            }
            if (!IsValidHEXFormat(cellValue))
            {
                cell.Value = "00000000-00000000-00000000-00000000";
            }
        }

        private void ValidateActCastCellValue(int rowIndex, int columnIndex)
        {
            DataGridViewCell cell = dataGridView.Rows[rowIndex].Cells[columnIndex];
            string cellValue = cell.Value?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(cellValue))
            {
                cell.Value = "----";
            }
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            buttonCopyToClipboard.Enabled = dataGridView.SelectedRows.Count > 0;
            buttonDeleteRows.Enabled = dataGridView.SelectedRows.Count > 0;
            if (dataGridView.SelectedRows.Count > 0 || dataGridView.SelectedCells.Count > 0)
            {
                buttonPreviewEditor.Enabled = true;
            }
            else
            {
                buttonPreviewEditor.Enabled = false;
            }
        }

        private void OpenFormBWithSelectedRowData()
        {
            DataGridViewRow selectedRow = null;

            if (dataGridView.SelectedRows.Count >= 1)
            {
                selectedRow = dataGridView.SelectedRows[0];
            }
            else if (dataGridView.SelectedCells.Count >= 1)
            {
                selectedRow = dataGridView.SelectedCells[0].OwningRow;
            }

            if (selectedRow != null)
            {
                dataGridView.ClearSelection();
                selectedRow.Selected = true;

                var rowData = new BakinPanelData.RowData
                {
                    Tag = selectedRow.Cells["Tag"].Value?.ToString() ?? string.Empty,
                    Text = selectedRow.Cells["Text"].Value?.ToString() ?? string.Empty,
                    NPL = selectedRow.Cells["NPL"].Value?.ToString() ?? string.Empty,
                    NPC = selectedRow.Cells["NPC"].Value?.ToString() ?? string.Empty,
                    NPR = selectedRow.Cells["NPR"].Value?.ToString() ?? string.Empty,
                    blspd = int.TryParse(selectedRow.Cells["blspd"].Value?.ToString(), out var blspd) ? blspd : 0,
                    blrate = int.TryParse(selectedRow.Cells["blrate"].Value?.ToString(), out var blrate) ? blrate : 0,
                    lipspd = float.TryParse(selectedRow.Cells["lipspd"].Value?.ToString(), out var lipspd) ? lipspd : 0,
                    Cast1 = selectedRow.Cells["Cast1"].Value?.ToString() ?? string.Empty,
                    ActCast1 = selectedRow.Cells["ActCast1"].Value?.ToString() ?? string.Empty,
                    Cast2 = selectedRow.Cells["Cast2"].Value?.ToString() ?? string.Empty,
                    ActCast2 = selectedRow.Cells["ActCast2"].Value?.ToString() ?? string.Empty,
                    TalkCast = selectedRow.Cells["TalkCast"].Value?.ToString() ?? string.Empty,
                    MirrorCast1 = selectedRow.Cells["MirrorCast1"].Value as bool? ?? false,
                    MirrorCast2 = selectedRow.Cells["MirrorCast2"].Value as bool? ?? false,
                    Billboard1 = selectedRow.Cells["Billboard1"].Value as bool? ?? false,
                    Billboard2 = selectedRow.Cells["Billboard2"].Value as bool? ?? false,
                    WindowVisible = selectedRow.Cells["WindowVisible"].Value as bool? ?? false,
                    WindowPosition = selectedRow.Cells["WindowPosition"].Value?.ToString() ?? string.Empty,
                    SpeechBubble = selectedRow.Cells["SpeechBubble"].Value?.ToString() ?? string.Empty,
                    UseMapLight = selectedRow.Cells["UseMapLight"].Value as bool? ?? false,
                    Memo = selectedRow.Cells["Memo"].Value?.ToString() ?? string.Empty
                };

                if (_FormPreviewEditor == null || _FormPreviewEditor.IsDisposed)
                {
                    _FormPreviewEditor = new FormPreviewEditor(rowData, _comboBoxItems);
                    _FormPreviewEditor.DataChanged += FormPreviewEditor_DataChanged;
                    _FormPreviewEditor.RowChangeRequested += FormPreviewEditor_RowChangeRequested;
                    _FormPreviewEditor.RowSwapRequested += FormPreviewEditor_RowSwapRequested;
                    _FormPreviewEditor.RowAddRequested += FormPreviewEditor_RowAddRequested;
                    _FormPreviewEditor.CSVSaveRequested += FormPreviewEditor_CSVSaveRequested;
                    _FormPreviewEditor.UpdateComboBoxItems(_comboBoxItems);
                }
                else
                {
                    _FormPreviewEditor.UpdateSelectedData(rowData);
                    _FormPreviewEditor.UpdateComboBoxItems(_comboBoxItems);
                    _FormPreviewEditor.PopulateFieldsWithData();
                }

                if (!_FormPreviewEditor.Visible)
                {
                    //_FormPreviewEditor.Show();
                    _FormPreviewEditor.ShowDialog(this);
                }
                else
                {
                    _FormPreviewEditor.BringToFront();
                }
            }
        }

        private void FormPreviewEditor_DataChanged(object sender, RowData e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView.SelectedRows[0];

                selectedRow.Cells["Tag"].Value = e.Tag;
                selectedRow.Cells["Text"].Value = e.Text;
                selectedRow.Cells["NPL"].Value = e.NPL;
                selectedRow.Cells["NPC"].Value = e.NPC;
                selectedRow.Cells["NPR"].Value = e.NPR;
                selectedRow.Cells["blspd"].Value = e.blspd;
                selectedRow.Cells["blrate"].Value = e.blrate;
                selectedRow.Cells["lipspd"].Value = e.lipspd;
                selectedRow.Cells["Cast1"].Value = e.Cast1;
                selectedRow.Cells["ActCast1"].Value = e.ActCast1;
                selectedRow.Cells["Cast2"].Value = e.Cast2;
                selectedRow.Cells["ActCast2"].Value = e.ActCast2;
                selectedRow.Cells["TalkCast"].Value = e.TalkCast;
                selectedRow.Cells["MirrorCast1"].Value = e.MirrorCast1;
                selectedRow.Cells["MirrorCast2"].Value = e.MirrorCast2;
                selectedRow.Cells["Billboard1"].Value = e.Billboard1;
                selectedRow.Cells["Billboard2"].Value = e.Billboard2;
                selectedRow.Cells["WindowVisible"].Value = e.WindowVisible;
                selectedRow.Cells["WindowPosition"].Value = e.WindowPosition;
                selectedRow.Cells["SpeechBubble"].Value = e.SpeechBubble;
                selectedRow.Cells["UseMapLight"].Value = e.UseMapLight;
                selectedRow.Cells["Memo"].Value = e.Memo;

                dataGridView.RefreshEdit();
            }
        }

        private void FormPreviewEditor_RowChangeRequested(object sender, PreviewEditorEventArgs e)
        {
            if (dataGridView.SelectedRows.Count >= 1)
            {
                int selectedIndex = dataGridView.SelectedRows[0].Index;
                int newIndex = selectedIndex + e.Change;

                if (newIndex >= 0 && newIndex < dataGridView.Rows.Count)
                {
                    dataGridView.ClearSelection();
                    dataGridView.Rows[newIndex].Selected = true;

                    int firstVisibleIndex = dataGridView.FirstDisplayedScrollingRowIndex;
                    int visibleRowCount = dataGridView.DisplayedRowCount(false);
                    if (newIndex < firstVisibleIndex || newIndex >= firstVisibleIndex + visibleRowCount)
                    {
                        dataGridView.FirstDisplayedScrollingRowIndex = newIndex;
                    }

                    OpenFormBWithSelectedRowData();
                }
            }
        }

        private void FormPreviewEditor_RowSwapRequested(object sender, SwapRowEventArgs e)
        {
            if (dataGridView.SelectedRows.Count >= 1)
            {
                int selectedIndex = dataGridView.SelectedRows[0].Index;
                int newIndex = selectedIndex + e.Change;

                if (newIndex >= 0 && newIndex < dataGridView.Rows.Count)
                {
                    var selectedData = _bindingSource[selectedIndex];
                    var swapData = _bindingSource[newIndex];

                    _bindingSource[selectedIndex] = swapData;
                    _bindingSource[newIndex] = selectedData;

                    dataGridView.ClearSelection();

                    dataGridView.Rows[newIndex].Selected = true;

                    int firstVisibleIndex = dataGridView.FirstDisplayedScrollingRowIndex;
                    int visibleRowCount = dataGridView.DisplayedRowCount(false);
                    if (newIndex < firstVisibleIndex || newIndex >= firstVisibleIndex + visibleRowCount)
                    {
                        dataGridView.FirstDisplayedScrollingRowIndex = newIndex;
                    }

                    OpenFormBWithSelectedRowData();
                }
            }
        }

        private void FormPreviewEditor_RowAddRequested(object sender, AddRowEventArgs e)
        {
            List<int> rowIndexes = new List<int>();

            if (dataGridView.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    rowIndexes.Add(row.Index);
                }
            }
            else if (dataGridView.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                {
                    if (!rowIndexes.Contains(cell.RowIndex))
                    {
                        rowIndexes.Add(cell.RowIndex);
                    }
                }
            }

            rowIndexes.Sort();

            int insertIndex = -1;
            if (rowIndexes.Count > 0)
            {
                insertIndex = rowIndexes[rowIndexes.Count - 1] + 1;
                BakinPanelData.RowData newRowData = new BakinPanelData.RowData();
                _bindingSource.Insert(insertIndex, newRowData);

                dataGridView.ClearSelection();
                dataGridView.Rows[insertIndex].Selected = true;
                dataGridView.CurrentCell = dataGridView.Rows[insertIndex].Cells[0];
            }
            else
            {
                BakinPanelData.RowData newRowData = new BakinPanelData.RowData();
                _bindingSource.Add(newRowData);
                dataGridView.ClearSelection();
                dataGridView.Rows[dataGridView.Rows.Count - 1].Selected = true;
                dataGridView.CurrentCell = dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[0];
                insertIndex = dataGridView.Rows.Count - 1;
            }

            int firstVisibleIndex = dataGridView.FirstDisplayedScrollingRowIndex;
            int visibleRowCount = dataGridView.DisplayedRowCount(false);
            if (insertIndex < firstVisibleIndex || insertIndex >= firstVisibleIndex + visibleRowCount)
            {
                dataGridView.FirstDisplayedScrollingRowIndex = insertIndex;
            }
        }

        private void FormPreviewEditor_CSVSaveRequested(object sender, SaveCSVEventArgs e)
        {
            SaveCSV();
        }

        private void CheckClipboard(object sender, EventArgs e)
        {
            buttonPasteFromClipboard.Enabled = Clipboard.ContainsData("Yukar2ScriptCommands");
        }

        private void dataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo = dataGridView.HitTest(e.X, e.Y);
            if (hitTestInfo.RowIndex != -1 && hitTestInfo.ColumnIndex != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                dragBox = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
            {
                dragBox = Rectangle.Empty;
            }
            rowIndexFromMouseDown = hitTestInfo.RowIndex;
        }

        private void dataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (dragBox != Rectangle.Empty && !dragBox.Contains(e.X, e.Y))
                {
                    dataGridView.DoDragDrop(dataGridView.Rows[rowIndexFromMouseDown], DragDropEffects.Move);
                }
            }
        }

        private void dataGridView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridView_DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = dataGridView.PointToClient(new Point(e.X, e.Y));
            rowIndexOfItemUnderMouseToDrop = dataGridView.HitTest(clientPoint.X, clientPoint.Y).RowIndex;
            if (rowIndexOfItemUnderMouseToDrop != -1 && e.Effect == DragDropEffects.Move)
            {
                var itemToMove = _dataList[rowIndexFromMouseDown];
                _dataList.RemoveAt(rowIndexFromMouseDown);
                _dataList.Insert(rowIndexOfItemUnderMouseToDrop, itemToMove);
            }
        }
        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            bool hasSelectedRows = dataGridView.SelectedRows.Count > 0;
            copyToClipboardToolStripMenuItem.Enabled = hasSelectedRows;
            deleteSelectedRowsToolStripMenuItem.Enabled = hasSelectedRows;
            pasteFromClipboardToolStripMenuItem.Enabled = Clipboard.ContainsData("Yukar2ScriptCommands");
        }

        private void addNewRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonInsertNewRowsBeforeSelection_Click(sender, e);
        }

        private void deleteSelectedRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonDeleteRows_Click(sender, e);
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonCopyToClipboard_Click(sender, e);
        }

        private void pasteFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonPasteFromClipboard_Click(sender, e);
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            BakinPanelData.RowData newRowData = new BakinPanelData.RowData();
            _bindingSource.Add(newRowData);
            dataGridView.ClearSelection();
            dataGridView.Rows[dataGridView.Rows.Count - 1].Selected = true;
            dataGridView.CurrentCell = dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[0];
        }

        private void buttonInsertNewRowsBeforeSelection_Click(object sender, EventArgs e)
        {
            List<int> rowIndexes = new List<int>();

            if (dataGridView.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    rowIndexes.Add(row.Index);
                }
            }
            else if (dataGridView.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                {
                    if (!rowIndexes.Contains(cell.RowIndex))
                    {
                        rowIndexes.Add(cell.RowIndex);
                    }
                }
            }

            rowIndexes.Sort();

            if (rowIndexes.Count > 0)
            {
                int insertIndex = rowIndexes[0];
                for (int i = 0; i < rowIndexes.Count; i++)
                {
                    BakinPanelData.RowData newRowData = new BakinPanelData.RowData();
                    _bindingSource.Insert(insertIndex, newRowData);
                }
                dataGridView.ClearSelection();
                for (int i = insertIndex; i < insertIndex + rowIndexes.Count; i++)
                {
                    dataGridView.Rows[i].Selected = true;
                }
                dataGridView.CurrentCell = dataGridView.Rows[insertIndex].Cells[0];
            }
            else
            {
                BakinPanelData.RowData newRowData = new BakinPanelData.RowData();
                _bindingSource.Add(newRowData);
                dataGridView.ClearSelection();
                dataGridView.Rows[dataGridView.Rows.Count - 1].Selected = true;
                dataGridView.CurrentCell = dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[0];
            }
        }

        private void buttonDeleteRows_Click(object sender, EventArgs e)
        {
            var rowsToDelete = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                rowsToDelete.Add(row);
            }
            foreach (DataGridViewRow row in rowsToDelete)
            {
                if (row != null && row.Index < _bindingSource.Count)
                {
                    _bindingSource.RemoveAt(row.Index);
                }
            }
            if (_bindingSource.Count > 0)
            {
                dataGridView.CurrentCell = dataGridView.Rows[0].Cells[0];
            }
        }

        private void buttonViewChange_Click(object sender, EventArgs e)
        {
            FormColumnSelector columnSelectorForm = new FormColumnSelector(dataGridView.Columns.Cast<DataGridViewColumn>());
            if (columnSelectorForm.ShowDialog() == DialogResult.OK)
            {
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    if (columnSelectorForm.ColumnStates.TryGetValue(column.Name, out bool isVisible))
                    {
                        column.Visible = isVisible;
                    }
                }
            }
        }

        private void buttonPasteFromClipboard_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsData("Yukar2ScriptCommands"))
            {
                var data = BakinPanelData.GetClipBoardData().ToList();
                data.Reverse();
                foreach (var rowData in data)
                {
                    StringData selectedData = ParseString(rowData.Text);
                    rowData.Text = selectedData.text;
                    rowData.NPL = selectedData.NPL;
                    rowData.NPC = selectedData.NPC;
                    rowData.NPR = selectedData.NPR;
                    rowData.blspd = selectedData.Blspd;
                    rowData.blrate = selectedData.Blrate;
                    rowData.lipspd = selectedData.Lipspd;
                    _bindingSource.Add(rowData);
                }
                _bindingSource.ResetBindings(false);
            }
        }

        private void btnClearDataGridView_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to clear the DataGridView?", "Clear DataGridView", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _dataList.Clear();
                _bindingSource.ResetBindings(false);
                _currentFilePath = null;
                UpdateWindowTitle();
            }
        }

        private void buttonPreviewEditor_Click(object sender, EventArgs e)
        {
            OpenFormBWithSelectedRowData();
        }

        private void buttonUUIDDefinition_Click(object sender, EventArgs e)
        {
            FormComboBoxConfig configForm = new FormComboBoxConfig(_comboBoxItems);
            if (configForm.ShowDialog() == DialogResult.OK)
            {
                _comboBoxItems = configForm.GetItems();
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                SaveCSV();
                e.SuppressKeyPress = true;
            }
        }

        private void SaveCSV()
        {
            if (!string.IsNullOrEmpty(_currentFilePath))
            {
                try
                {
                    WriteCsv(_currentFilePath);
                    MessageBox.Show("File saved successfully.", "Save Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving file: " + ex.Message);
                    _currentFilePath = "";
                }
                UpdateWindowTitle();
            }
            else
            {
                SaveAsCSV();
            }
        }

        private void SaveAsCSV()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _currentFilePath = saveFileDialog.FileName;
                try
                {
                    WriteCsv(_currentFilePath);
                    MessageBox.Show("File saved successfully.", "Save Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving file: " + ex.Message);
                    _currentFilePath = "";
                }
                UpdateWindowTitle();
            }
        }

        private void UpdateWindowTitle()
        {
            var exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.exe");
            var versionInfo = FileVersionInfo.GetVersionInfo(exePath);
            var fileVersion = versionInfo.FileVersion;
            var appName = Assembly.GetExecutingAssembly().GetName().Name;

            if (!string.IsNullOrEmpty(_currentFilePath))
            {
                this.Text = $"{appName} ({fileVersion}) : {_currentFilePath}";
            }
            else
            {
                this.Text = $"{appName} ({fileVersion})";
            }
        }

        private void buttonReplaceUUID_Click(object sender, EventArgs e)
        {
            var formChangeValue = new FormReplaceUUID(_comboBoxItems);
            if (formChangeValue.ShowDialog() == DialogResult.OK)
            {
                string oldValue = formChangeValue.OldValue;
                string newValue = formChangeValue.NewValue;

                int replaceCount = 0;

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    foreach (string columnName in new[] { "Cast1", "Cast2", "SpeechBubble" })
                    {
                        var cell = row.Cells[columnName];
                        if ((string)cell.Value == oldValue)
                        {
                            cell.Value = newValue;
                            replaceCount++;
                        }
                    }
                }

                dataGridView.Refresh();
                MessageBox.Show($"{replaceCount} times replaced.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
