using ClipboardToolForBakin;
using System.Text.RegularExpressions;

namespace ClipboardToolForBakin2
{
    public partial class FormTextToSound : Form
    {
        public FormTextToSound()
        {
            InitializeComponent();
            textBoxCurrentUUID.Text = BakinPanelData.GetCurrentUUID();

            textBoxCurrentUUID.KeyPress += (sender, e) =>
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
            textBoxCurrentUUID.TextChanged += (sender, e) =>
            {
                if (textBoxCurrentUUID.Text.Replace("-", "").Length > 32)
                {
                    int cursorPos = textBoxCurrentUUID.SelectionStart - 1;
                    textBoxCurrentUUID.Text = textBoxCurrentUUID.Text.Remove(cursorPos, 1);
                    textBoxCurrentUUID.SelectionStart = cursorPos;
                }
            };
            textBoxCurrentUUID.Leave += (sender, e) =>
            {
                if (textBoxCurrentUUID.Text == String.Empty) return;
                string paddedText = textBoxCurrentUUID.Text.Replace("-", "").PadLeft(32, '0').ToUpper();
                string result = Regex.Replace(paddedText, ".{8}", "$0-").TrimEnd('-');
                textBoxCurrentUUID.Text = result;
                textBoxCurrentUUID.SelectionStart = textBoxCurrentUUID.Text.Length;
            };
            ToolStripMenuItem paste1Item = new ToolStripMenuItem("Get Common Event UUID");
            paste1Item.Click += (sender, e) =>
            {
                BakinPanelData.UpdateCallCommonEvent();
                textBoxCurrentUUID.Text = BakinPanelData.GetCurrentUUID();
            };
            ContextMenuStrip cms = new ContextMenuStrip();
            cms.Items.Add(paste1Item);
            cms.Opening += (sender, e) =>
            {
                paste1Item.Enabled = false;
                if (Clipboard.ContainsData("Yukar2ScriptCommands"))
                {
                    paste1Item.Enabled = BakinPanelData.IsCommonEventCallDataOnEventPanel();
                }
            };
            textBoxCurrentUUID.ContextMenuStrip = cms;
        }

        private void buttonDisable_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void buttonEnable_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private bool IsHexString(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"\A\b[0-9a-fA-F]+\b\Z");
        }
    }
}
