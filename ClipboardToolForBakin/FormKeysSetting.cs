using static ClipboardToolForBakin2.FormKeysSetting;

namespace ClipboardToolForBakin2
{
    public partial class FormKeysSetting : Form
    {
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "shortcutkeys.json");

        public class ShortcutKeyBinding
        {
            public string FunctionName { get; set; } = string.Empty;
            public string ShortcutKey { get; set; } = string.Empty;
        }

        private List<ShortcutKeyBinding> _shortcutKeyBindings;

        public FormKeysSetting(List<ShortcutKeyBinding> shortcutKeyBindings)
        {
            InitializeComponent();
            _shortcutKeyBindings = shortcutKeyBindings;
            if (_shortcutKeyBindings.Count == 0)
            {
                ShortcutKeyManager.InitKeyBindings(_shortcutKeyBindings);
            }
            for (int i = 0; i < _shortcutKeyBindings.Count; i++)
            {
                FlowLayoutPanel innerPanel = new FlowLayoutPanel();
                innerPanel.AutoSize = true;
                innerPanel.FlowDirection = FlowDirection.LeftToRight;
                outerPanel.Controls.Add(innerPanel);

                var label = new Label
                {
                    Text = _shortcutKeyBindings[i].FunctionName,
                    AutoSize = true,
                    Margin = new Padding(10)
                };
                innerPanel.Controls.Add(label);

                var textBox = new TextBox
                {
                    Text = _shortcutKeyBindings[i].ShortcutKey,
                    Width = 200,
                    ReadOnly = true,
                    Margin = new Padding(10)
                };
#nullable disable
                textBox.KeyDown += textBox_KeyDown;
                textBox.Leave += textBox_Leave;
                textBox.TextChanged += textBox_TextChanged;
#nullable restore
                innerPanel.Controls.Add(textBox);

                var clearButton = new Button
                {
                    Text = "Clear",
                    AutoSize = true,
                    Margin = new Padding(10)
                };
#nullable disable
                clearButton.Click += ClearButton_Click;
#nullable restore
                innerPanel.Controls.Add(clearButton);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            Button clearButton = (Button)sender;
            FlowLayoutPanel innerPanel = (FlowLayoutPanel)clearButton.Parent;
            TextBox textBox = (TextBox)innerPanel.Controls[1];
            textBox.Text = ShortcutKeyManager._Nothing;
            int index = outerPanel.Controls.IndexOf(innerPanel);
            _shortcutKeyBindings[index].ShortcutKey = ShortcutKeyManager._Nothing;
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            string inputKey = ((TextBox)sender).Text;
            if (CheckSingleKeyShortcut(inputKey))
            {
                MessageBox.Show("Invalid shortcut key detected. Please input a valid shortcut key combination.");
            }
            else if (CheckShortcutDuplicate(inputKey))
            {
                MessageBox.Show("Duplicate shortcut key detected. Please input a unique shortcut key.");
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            string shortcutKey = ShortcutKeyManager.KeyCodeToString(e);
            ((TextBox)sender).Text = shortcutKey;

            for (int i = 0; i < _shortcutKeyBindings.Count; i++)
            {
                if (outerPanel.Controls.OfType<FlowLayoutPanel>().SelectMany(panel => panel.Controls.OfType<TextBox>()).ElementAt(i) == sender)
                {
                    _shortcutKeyBindings[i].ShortcutKey = shortcutKey;
                    break;
                }
            }

            e.SuppressKeyPress = true;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            string inputKey = ((TextBox)sender).Text;
            var textBoxes = outerPanel.Controls.OfType<FlowLayoutPanel>().SelectMany(panel => panel.Controls.OfType<TextBox>()).ToArray();

            foreach (var box in textBoxes)
            {
                box.ForeColor = Color.Black;
            }

            foreach (var box in textBoxes)
            {
                if (CheckShortcutDuplicate(box.Text))
                {
                    box.ForeColor = Color.Red;
                }
            }
        }

        private bool CheckSingleKeyShortcut(string key)
        {
            if (key == ShortcutKeyManager._Nothing) return false;
            var singleKeys = new List<string> { "Ctrl", "Shift", "Alt" };
            foreach (var singleKey in singleKeys)
            {
                if (key == singleKey) return true;
            }
            return false;
        }

        private bool CheckShortcutDuplicate(string key)
        {
            if (key == ShortcutKeyManager._Nothing) return false;
            var textBoxes = outerPanel.Controls.OfType<FlowLayoutPanel>().SelectMany(panel => panel.Controls.OfType<TextBox>()).ToArray();
            int count = textBoxes.Count(t => t.Text == key);
            return count > 1;
        }

        private void UpdateShortcutKeyBindings()
        {
            var textBoxes = outerPanel.Controls.OfType<FlowLayoutPanel>().SelectMany(panel => panel.Controls.OfType<TextBox>()).ToArray();
            for (int i = 0; i < _shortcutKeyBindings.Count; i++)
            {
                textBoxes[i].Text = _shortcutKeyBindings[i].ShortcutKey;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ShortcutKeyManager.InitKeyBindings(_shortcutKeyBindings);
            try
            {
                UpdateShortcutKeyBindings();
            }
            catch
            {
                MessageBox.Show("Initialization of shortcut keys failed. please delete [shortcutkeys.json] and restart the application.");
            }
        }

        private void FormKeysSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ShortcutKeyManager.LoadShortcutKeys(ref _shortcutKeyBindings);
            }
            catch
            {
                MessageBox.Show("Initialization of shortcut keys failed. please delete [shortcutkeys.json] and restart the application.");
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            try
            {
                ShortcutKeyManager.LoadShortcutKeys(ref _shortcutKeyBindings);
            }
            catch
            {
                MessageBox.Show("Initialization of shortcut keys failed. please delete [shortcutkeys.json] and restart the application.");
            }
            UpdateShortcutKeyBindings();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            foreach (var keyBinding in _shortcutKeyBindings)
            {
                if (CheckShortcutDuplicate(keyBinding.ShortcutKey))
                {
                    MessageBox.Show("Duplicate shortcut keys detected. Please make sure all shortcut keys are unique.");
                    return;
                }
            }
            ShortcutKeyManager.SaveShortcutKeys(_shortcutKeyBindings);
            this.DialogResult = DialogResult.OK;
        }

        public List<ShortcutKeyBinding> GetShortcutKeyBindings()
        {
            return _shortcutKeyBindings;
        }
    }
}
