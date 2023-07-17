namespace ClipboardToolForBakin2
{
    partial class FormReplaceUUID
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReplaceUUID));
            textBoxOldValue = new TextBox();
            textBoxNewValue = new TextBox();
            buttonApply = new Button();
            Cancel = new Button();
            comboBoxTarget = new ComboBox();
            textBoxReadme = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // textBoxOldValue
            // 
            textBoxOldValue.BackColor = SystemColors.Control;
            textBoxOldValue.Location = new Point(258, 288);
            textBoxOldValue.Margin = new Padding(6, 6, 6, 6);
            textBoxOldValue.Name = "textBoxOldValue";
            textBoxOldValue.ReadOnly = true;
            textBoxOldValue.Size = new Size(647, 39);
            textBoxOldValue.TabIndex = 0;
            textBoxOldValue.TabStop = false;
            // 
            // textBoxNewValue
            // 
            textBoxNewValue.Location = new Point(258, 446);
            textBoxNewValue.Margin = new Padding(6, 6, 6, 6);
            textBoxNewValue.Name = "textBoxNewValue";
            textBoxNewValue.Size = new Size(647, 39);
            textBoxNewValue.TabIndex = 1;
            // 
            // buttonApply
            // 
            buttonApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonApply.Enabled = false;
            buttonApply.Location = new Point(951, 809);
            buttonApply.Margin = new Padding(6, 6, 6, 6);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(186, 107);
            buttonApply.TabIndex = 3;
            buttonApply.Text = "Apply";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonApply_Click;
            // 
            // Cancel
            // 
            Cancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Cancel.Location = new Point(754, 809);
            Cancel.Margin = new Padding(6, 6, 6, 6);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(186, 107);
            Cancel.TabIndex = 2;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += Cancel_Click;
            // 
            // comboBoxTarget
            // 
            comboBoxTarget.FormattingEnabled = true;
            comboBoxTarget.Location = new Point(22, 288);
            comboBoxTarget.Margin = new Padding(6, 6, 6, 6);
            comboBoxTarget.Name = "comboBoxTarget";
            comboBoxTarget.Size = new Size(221, 40);
            comboBoxTarget.TabIndex = 0;
            comboBoxTarget.SelectedIndexChanged += comboBoxTarget_SelectedIndexChanged;
            // 
            // textBoxReadme
            // 
            textBoxReadme.Location = new Point(22, 26);
            textBoxReadme.Margin = new Padding(6, 6, 6, 6);
            textBoxReadme.Multiline = true;
            textBoxReadme.Name = "textBoxReadme";
            textBoxReadme.ReadOnly = true;
            textBoxReadme.Size = new Size(1111, 181);
            textBoxReadme.TabIndex = 5;
            textBoxReadme.TabStop = false;
            textBoxReadme.Text = resources.GetString("textBoxReadme.Text");
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 250);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(230, 32);
            label1.TabIndex = 6;
            label1.Text = "UUID to be replaced";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(258, 343);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(101, 86);
            label2.TabIndex = 7;
            label2.Text = "↓";
            // 
            // FormReplaceUUID
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1159, 941);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxReadme);
            Controls.Add(comboBoxTarget);
            Controls.Add(Cancel);
            Controls.Add(buttonApply);
            Controls.Add(textBoxNewValue);
            Controls.Add(textBoxOldValue);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(6, 6, 6, 6);
            Name = "FormReplaceUUID";
            Text = "UUID replacer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxOldValue;
        private TextBox textBoxNewValue;
        private Button buttonApply;
        private Button Cancel;
        private ComboBox comboBoxTarget;
        private TextBox textBoxReadme;
        private Label label1;
        private Label label2;
    }
}