namespace ClipboardToolForBakin2
{
    partial class FormKeysSetting
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
            outerPanel = new FlowLayoutPanel();
            buttonReset = new Button();
            buttonReload = new Button();
            buttonApply = new Button();
            SuspendLayout();
            // 
            // outerPanel
            // 
            outerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            outerPanel.AutoScroll = true;
            outerPanel.FlowDirection = FlowDirection.TopDown;
            outerPanel.Location = new Point(12, 12);
            outerPanel.Name = "outerPanel";
            outerPanel.Size = new Size(494, 417);
            outerPanel.TabIndex = 0;
            outerPanel.WrapContents = false;
            // 
            // buttonReset
            // 
            buttonReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonReset.Location = new Point(512, 267);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(100, 50);
            buttonReset.TabIndex = 1;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // buttonReload
            // 
            buttonReload.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonReload.Location = new Point(512, 323);
            buttonReload.Name = "buttonReload";
            buttonReload.Size = new Size(100, 50);
            buttonReload.TabIndex = 2;
            buttonReload.Text = "Reload";
            buttonReload.UseVisualStyleBackColor = true;
            buttonReload.Click += buttonReload_Click;
            // 
            // buttonApply
            // 
            buttonApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonApply.Location = new Point(512, 379);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(100, 50);
            buttonApply.TabIndex = 3;
            buttonApply.Text = "Apply";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonApply_Click;
            // 
            // FormKeysSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 441);
            Controls.Add(buttonApply);
            Controls.Add(buttonReload);
            Controls.Add(buttonReset);
            Controls.Add(outerPanel);
            Name = "FormKeysSetting";
            Text = "Shortcut keys setting (Preview editor)";
            FormClosing += FormKeysSetting_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel outerPanel;
        private Button buttonReset;
        private Button buttonReload;
        private Button buttonApply;
    }
}