namespace ClipboardToolForBakin2
{
    partial class FormComboBoxConfig
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
            buttonOK = new Button();
            outerPanel = new FlowLayoutPanel();
            label1 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            buttonLoad = new Button();
            buttonSaveAs = new Button();
            SuspendLayout();
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOK.Location = new Point(782, 439);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(150, 50);
            buttonOK.TabIndex = 3;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // outerPanel
            // 
            outerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            outerPanel.AutoScroll = true;
            outerPanel.FlowDirection = FlowDirection.TopDown;
            outerPanel.Location = new Point(12, 51);
            outerPanel.Name = "outerPanel";
            outerPanel.Size = new Size(764, 438);
            outerPanel.TabIndex = 0;
            outerPanel.WrapContents = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 2;
            label1.Text = "ResourceN";
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Location = new Point(82, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(150, 23);
            textBox1.TabIndex = 3;
            textBox1.TabStop = false;
            textBox1.Text = "Enter the display name.";
            // 
            // textBox2
            // 
            textBox2.Enabled = false;
            textBox2.Location = new Point(238, 12);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(400, 23);
            textBox2.TabIndex = 4;
            textBox2.TabStop = false;
            textBox2.Text = "Please enter the UUID. (ex)00000000-00000000-00000000-00000000";
            // 
            // buttonLoad
            // 
            buttonLoad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonLoad.Location = new Point(782, 51);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(150, 50);
            buttonLoad.TabIndex = 1;
            buttonLoad.Text = "Load";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // buttonSaveAs
            // 
            buttonSaveAs.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSaveAs.Location = new Point(782, 107);
            buttonSaveAs.Name = "buttonSaveAs";
            buttonSaveAs.Size = new Size(150, 50);
            buttonSaveAs.TabIndex = 2;
            buttonSaveAs.Text = "Save As...";
            buttonSaveAs.UseVisualStyleBackColor = true;
            buttonSaveAs.Click += buttonSaveAs_Click;
            // 
            // FormComboBoxConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 501);
            Controls.Add(buttonSaveAs);
            Controls.Add(buttonLoad);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(outerPanel);
            Controls.Add(buttonOK);
            KeyPreview = true;
            Name = "FormComboBoxConfig";
            Text = "UUID Definition";
            KeyDown += FormComboBoxConfig_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonOK;
        private FlowLayoutPanel outerPanel;
        private Label label1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button buttonLoad;
        private Button buttonSaveAs;
    }
}