namespace ClipboardToolForBakin2
{
    partial class FormColumnSelector
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
            flowLayoutPanelViewChange = new FlowLayoutPanel();
            ButtonConfirm = new Button();
            ButtonMinimum = new Button();
            buttonTalk = new Button();
            buttonAll = new Button();
            SuspendLayout();
            // 
            // flowLayoutPanelViewChange
            // 
            flowLayoutPanelViewChange.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanelViewChange.AutoScroll = true;
            flowLayoutPanelViewChange.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelViewChange.Location = new Point(12, 12);
            flowLayoutPanelViewChange.Name = "flowLayoutPanelViewChange";
            flowLayoutPanelViewChange.Size = new Size(600, 361);
            flowLayoutPanelViewChange.TabIndex = 0;
            // 
            // ButtonConfirm
            // 
            ButtonConfirm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ButtonConfirm.Location = new Point(462, 379);
            ButtonConfirm.Name = "ButtonConfirm";
            ButtonConfirm.Size = new Size(150, 50);
            ButtonConfirm.TabIndex = 1;
            ButtonConfirm.Text = "OK";
            ButtonConfirm.UseVisualStyleBackColor = true;
            ButtonConfirm.Click += ButtonConfirm_Click;
            // 
            // ButtonMinimum
            // 
            ButtonMinimum.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ButtonMinimum.Location = new Point(12, 379);
            ButtonMinimum.Name = "ButtonMinimum";
            ButtonMinimum.Size = new Size(100, 50);
            ButtonMinimum.TabIndex = 2;
            ButtonMinimum.Text = "Minimum";
            ButtonMinimum.UseVisualStyleBackColor = true;
            ButtonMinimum.Click += ButtonMinimum_Click;
            // 
            // buttonTalk
            // 
            buttonTalk.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonTalk.Location = new Point(118, 379);
            buttonTalk.Name = "buttonTalk";
            buttonTalk.Size = new Size(100, 50);
            buttonTalk.TabIndex = 3;
            buttonTalk.Text = "Talk";
            buttonTalk.UseVisualStyleBackColor = true;
            buttonTalk.Click += buttonTalkMode_Click;
            // 
            // buttonAll
            // 
            buttonAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonAll.Location = new Point(224, 379);
            buttonAll.Name = "buttonAll";
            buttonAll.Size = new Size(100, 50);
            buttonAll.TabIndex = 4;
            buttonAll.Text = "All";
            buttonAll.UseVisualStyleBackColor = true;
            buttonAll.Click += buttonAll_Click;
            // 
            // ColumnSelectorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 441);
            Controls.Add(buttonAll);
            Controls.Add(buttonTalk);
            Controls.Add(ButtonMinimum);
            Controls.Add(ButtonConfirm);
            Controls.Add(flowLayoutPanelViewChange);
            Name = "ColumnSelectorForm";
            Text = "ColumnSelectorForm";
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanelViewChange;
        private Button ButtonConfirm;
        private Button ButtonMinimum;
        private Button buttonTalk;
        private Button buttonAll;
    }
}