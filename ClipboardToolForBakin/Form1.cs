using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ClipboardToolForBakin2;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using static ClipboardToolForBakin2.StringUtil;

namespace ClipboardToolForBakin
{
    public partial class MainForm : Form
    {
        private BindingSource _bindingSource;
        private BindingList<BakinPanelData.RowData> _dataList;

        private Rectangle dragBox;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;

        private string lastClipboardText = string.Empty;
        private System.Windows.Forms.Timer clipboardCheckTimer;

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
            CopyFromClipboardButton.Enabled = Clipboard.ContainsData("Yukar2ScriptCommands");
            clipboardCheckTimer = new System.Windows.Forms.Timer();
            clipboardCheckTimer.Interval = 500;
            clipboardCheckTimer.Tick += CheckClipboard;
            clipboardCheckTimer.Start();
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
                DefaultCellStyle = new DataGridViewCellStyle { NullValue = "Notes" }
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
                Map(m => m.Text).Name("text").Optional().Default("");
                Map(m => m.NPL).Name("NPL").Optional().Default("");
                Map(m => m.NPC).Name("NPC").Optional().Default("");
                Map(m => m.NPR).Name("NPR").Optional().Default("");
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
                Map(m => m.WindowPosition).Name("WindowPosition").Optional().Default("Down");
                Map(m => m.SpeechBubble).Name("SpeechBubble").TypeConverter<CastValueConverter>().Optional().Default("00000000-00000000-00000000-00000000");
                Map(m => m.UseMapLight).Name("UseMapLight").TypeConverter<BoolToStringConverter>().Optional().Default("off");
                Map(m => m.Memo).Name("Memo").Optional().Default("");
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
                    record.SpeechBubble = "Reserved";
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
                ReadCsv(openFileDialog.FileName);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                WriteCsv(saveFileDialog.FileName);
            }
        }

        private void CopyToClipboardButton_Click(object sender, EventArgs e)
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
                        NPL = selectedRow.Cells["NPL"].Value?.ToString() ?? "",
                        NPC = selectedRow.Cells["NPC"].Value?.ToString() ?? "",
                        NPR = selectedRow.Cells["NPR"].Value?.ToString() ?? "",
                        text = selectedRow.Cells["Text"].Value?.ToString() ?? ""
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
                    string combinedText = CombineString(selectedData);

                    var rowData = new BakinPanelData.RowData
                    {
                        Tag = selectedRow.Cells["Tag"].Value?.ToString() ?? "",
                        //Text = selectedRow.Cells["Text"].Value?.ToString() ?? "",
                        Text = combinedText,
                        Cast1 = selectedRow.Cells["Cast1"].Value?.ToString() ?? "",
                        ActCast1 = selectedRow.Cells["ActCast1"].Value?.ToString() ?? "",
                        Cast2 = selectedRow.Cells["Cast2"].Value?.ToString() ?? "",
                        ActCast2 = selectedRow.Cells["ActCast2"].Value?.ToString() ?? "",
                        TalkCast = selectedRow.Cells["TalkCast"].Value?.ToString() ?? "",
                        MirrorCast1 = (bool)(selectedRow.Cells["MirrorCast1"].Value ?? false),
                        MirrorCast2 = (bool)(selectedRow.Cells["MirrorCast2"].Value ?? false),
                        Billboard1 = (bool)(selectedRow.Cells["Billboard1"].Value ?? false),
                        Billboard2 = (bool)(selectedRow.Cells["Billboard2"].Value ?? false),
                        WindowPosition = selectedRow.Cells["WindowPosition"].Value?.ToString() ?? "",
                        SpeechBubble = selectedRow.Cells["SpeechBubble"].Value?.ToString() ?? "",
                        UseMapLight = (bool)(selectedRow.Cells["UseMapLight"].Value ?? false),
                    };
                    selectedRows.Add(rowData);
                }
                BakinPanelData.SetClipBoard(selectedRows);
            }
        }

        private void buttonViewChange_Click(object sender, EventArgs e)
        {
            ColumnSelectorForm columnSelectorForm = new ColumnSelectorForm(dataGridView.Columns.Cast<DataGridViewColumn>());
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

        private void AddRowButton_Click(object sender, EventArgs e)
        {
            BakinPanelData.RowData newRowData = new BakinPanelData.RowData();
            _bindingSource.Add(newRowData);
            dataGridView.ClearSelection();
            dataGridView.Rows[dataGridView.Rows.Count - 1].Selected = true;
            dataGridView.CurrentCell = dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[0];
        }

        private void deleteButton_Click(object sender, EventArgs e)
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

        private void PasteFromClipboardButton_Click(object sender, EventArgs e)
        {
            bool hasYukar2ScriptCommandsFormatData = Clipboard.ContainsData("Yukar2ScriptCommands");
            if (!hasYukar2ScriptCommandsFormatData)
            {
                return;
            }

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
            //InitializeDataGridSetting();
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

        private void copyFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteFromClipboardButton_Click(sender, e);
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyToClipboardButton_Click(sender, e);
        }

        private void deleteSelectedRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteButton_Click(sender, e);
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            bool hasSelectedRows = dataGridView.SelectedRows.Count > 0;
            copyToClipboardToolStripMenuItem.Enabled = hasSelectedRows;
            deleteSelectedRowsToolStripMenuItem.Enabled = hasSelectedRows;

            bool hasYukar2ScriptCommandsFormatData = Clipboard.ContainsData("Yukar2ScriptCommands");
            copyFromClipboardToolStripMenuItem.Enabled = hasYukar2ScriptCommandsFormatData;
        }

        private void addNewRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRowButton_Click(sender, e);
        }

        private void btnClearDataGridView_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to clear the DataGridView?", "Clear DataGridView", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _dataList.Clear();
                _bindingSource.ResetBindings(false);
            }
        }

        private void CheckClipboard(object sender, EventArgs e)
        {
            string clipboardText = Clipboard.GetText();

            if (clipboardText != lastClipboardText)
            {
                CopyFromClipboardButton.Enabled = Clipboard.ContainsData("Yukar2ScriptCommands");
                lastClipboardText = clipboardText;
            }
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            CopyToClipboardButton.Enabled = dataGridView.SelectedRows.Count > 0;
        }
    }
}
