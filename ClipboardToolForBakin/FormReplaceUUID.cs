using ClipboardToolForBakin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardToolForBakin2
{
    public partial class FormReplaceUUID : Form
    {
        private List<ResourceItem> _comboBoxItems;

        public string OldValue { get; private set; } = string.Empty;
        public string NewValue { get; private set; } = string.Empty;

        public FormReplaceUUID(List<ResourceItem> comboBoxItems)
        {
            _comboBoxItems = comboBoxItems;
            InitializeComponent();
            UpdateComboBox();
            textBoxNewValue.KeyPress += (sender, e) =>
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
            textBoxNewValue.TextChanged += (sender, e) =>
            {
                if (textBoxNewValue.Text.Replace("-", "").Length > 32)
                {
                    int cursorPos = textBoxNewValue.SelectionStart - 1;
                    textBoxNewValue.Text = textBoxNewValue.Text.Remove(cursorPos, 1);
                    textBoxNewValue.SelectionStart = cursorPos;
                }
            };
            textBoxNewValue.Leave += (sender, e) =>
            {
                if (textBoxNewValue.Text == String.Empty) return;
                string paddedText = textBoxNewValue.Text.Replace("-", "").PadLeft(32, '0').ToUpper();
                string result = Regex.Replace(paddedText, ".{8}", "$0-").TrimEnd('-');
                textBoxNewValue.Text = result;
                textBoxNewValue.SelectionStart = textBoxNewValue.Text.Length;
            };
            ToolStripMenuItem paste1Item = new ToolStripMenuItem("Get Cast1 UUID");
            paste1Item.Click += (sender, e) =>
            {
                textBoxNewValue.Text = ProcessDataForPaste1();
            };
            ToolStripMenuItem paste2Item = new ToolStripMenuItem("Get Cast2 UUID");
            paste2Item.Click += (sender, e) =>
            {
                textBoxNewValue.Text = ProcessDataForPaste2();
            };
            ToolStripMenuItem paste3Item = new ToolStripMenuItem("Get Event UUID");
            paste3Item.Click += (sender, e) =>
            {
                textBoxNewValue.Text = ProcessDataForPaste3();
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
            textBoxNewValue.ContextMenuStrip = cms;
        }

        private void UpdateComboBox()
        {
            int cnt = 0;
            comboBoxTarget.Items.Clear();
            foreach (var item in _comboBoxItems)
            {
                if ((!string.IsNullOrEmpty(item.Key) && !string.IsNullOrEmpty(item.Value)) || cnt == 0 || cnt == 1)
                {
                    comboBoxTarget.Items.Add($"[{_comboBoxItems.IndexOf(item)}]:{item.Key}");
                    //CacheImage(item.ImagePath);
                }
                cnt++;
            }
        }

        private void comboBoxTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedIndex = comboBoxTarget.SelectedIndex;

            if (selectedIndex < 0 || selectedIndex >= _comboBoxItems.Count)
            {
                buttonApply.Enabled = false;
                return;
            }

            var selectedItem = _comboBoxItems[selectedIndex];
            if (string.IsNullOrEmpty(selectedItem.Value) || selectedItem.Value == "00000000-00000000-00000000-00000000")
            {
                textBoxOldValue.Text = String.Empty;
                buttonApply.Enabled = false;
            }
            else
            {
                textBoxOldValue.Text = selectedItem.Value;
                buttonApply.Enabled = true;
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            OldValue = textBoxOldValue.Text;
            NewValue = textBoxNewValue.Text;

            var selectedIndex = comboBoxTarget.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < _comboBoxItems.Count)
            {
                var selectedItem = _comboBoxItems[selectedIndex];
                selectedItem.Value = NewValue;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }





        //////////////////////////////////////////
        // Same as FormComboBoxConfig.cs method...
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
    }
}
