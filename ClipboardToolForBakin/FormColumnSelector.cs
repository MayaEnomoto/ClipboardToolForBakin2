namespace ClipboardToolForBakin2
{
    public partial class FormColumnSelector : Form
    {
        public Dictionary<string, bool> ColumnStates { get; private set; }
        private Dictionary<string, CheckBox> checkboxes = new Dictionary<string, CheckBox>();

        public FormColumnSelector(IEnumerable<DataGridViewColumn> columns)
        {
            InitializeComponent();

            ColumnStates = new Dictionary<string, bool>();

            foreach (var column in columns)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.AutoSize = true;
                checkbox.Name = column.Name;
                checkbox.Text = column.Name;
                checkbox.Checked = column.Visible;

                checkbox.CheckedChanged += (s, e) =>
                {
                    ColumnStates[checkbox.Name] = checkbox.Checked;
                };

                ColumnStates[checkbox.Name] = checkbox.Checked;

                checkbox.Margin = new Padding(10);

                checkboxes[checkbox.Name] = checkbox;
                flowLayoutPanelViewChange.Controls.Add(checkbox);
            }
        }

        private void buttonTalkMode_Click(object sender, EventArgs e)
        {
            string[] presetColumns = { "Tag", "Text", "NPL", "NPC", "NPR", "blspd", "blrate", "lipspd", "Memo" };
            foreach (var checkbox in checkboxes.Values)
            {
                checkbox.Checked = false;
            }
            foreach (string colName in presetColumns)
            {
                if (checkboxes.ContainsKey(colName))
                {
                    checkboxes[colName].Checked = true;
                }
            }
        }
        private void ButtonMinimum_Click(object sender, EventArgs e)
        {
            string[] presetColumns = { "Tag", "Text", "Memo" };
            foreach (var checkbox in checkboxes.Values)
            {
                checkbox.Checked = false;
            }
            foreach (string colName in presetColumns)
            {
                if (checkboxes.ContainsKey(colName))
                {
                    checkboxes[colName].Checked = true;
                }
            }
        }

        private void buttonAll_Click(object sender, EventArgs e)
        {
            foreach (var checkbox in checkboxes.Values)
            {
                checkbox.Checked = true;
            }
        }

        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
