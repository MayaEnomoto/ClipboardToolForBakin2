namespace ClipboardToolForBakin2
{
    partial class FormTextToSound
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
            textBoxReadme = new TextBox();
            label1 = new Label();
            textBoxCurrentUUID = new TextBox();
            buttonDisable = new Button();
            buttonEnable = new Button();
            SuspendLayout();
            // 
            // textBoxReadme
            // 
            textBoxReadme.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxReadme.Location = new Point(12, 12);
            textBoxReadme.Multiline = true;
            textBoxReadme.Name = "textBoxReadme";
            textBoxReadme.ReadOnly = true;
            textBoxReadme.Size = new Size(600, 100);
            textBoxReadme.TabIndex = 0;
            textBoxReadme.Text = "Copy the common event \"as_TextToSound_Start\" with a right click and paste it into the text box below with a right click.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 136);
            label1.Name = "label1";
            label1.Size = new Size(196, 15);
            label1.TabIndex = 1;
            label1.Text = "Current UUID(as_TextToSound_Start)";
            // 
            // textBoxCurrentUUID
            // 
            textBoxCurrentUUID.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxCurrentUUID.BackColor = SystemColors.Info;
            textBoxCurrentUUID.Location = new Point(12, 154);
            textBoxCurrentUUID.Name = "textBoxCurrentUUID";
            textBoxCurrentUUID.Size = new Size(600, 23);
            textBoxCurrentUUID.TabIndex = 0;
            // 
            // buttonDisable
            // 
            buttonDisable.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonDisable.Location = new Point(406, 379);
            buttonDisable.Name = "buttonDisable";
            buttonDisable.Size = new Size(100, 50);
            buttonDisable.TabIndex = 1;
            buttonDisable.Text = "Disable";
            buttonDisable.UseVisualStyleBackColor = true;
            buttonDisable.Click += buttonDisable_Click;
            // 
            // buttonEnable
            // 
            buttonEnable.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonEnable.Location = new Point(512, 379);
            buttonEnable.Name = "buttonEnable";
            buttonEnable.Size = new Size(100, 50);
            buttonEnable.TabIndex = 2;
            buttonEnable.Text = "Enable";
            buttonEnable.UseVisualStyleBackColor = true;
            buttonEnable.Click += buttonEnable_Click;
            // 
            // FormTextToSound
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 441);
            Controls.Add(buttonEnable);
            Controls.Add(buttonDisable);
            Controls.Add(textBoxCurrentUUID);
            Controls.Add(label1);
            Controls.Add(textBoxReadme);
            Name = "FormTextToSound";
            Text = "Enable Text to Sounds";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxReadme;
        private Label label1;
        private TextBox textBoxCurrentUUID;
        private Button buttonDisable;
        private Button buttonEnable;
    }
}