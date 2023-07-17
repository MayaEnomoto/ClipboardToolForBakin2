namespace ClipboardToolForBakin2
{
    partial class FormPreviewEditor
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
            components = new System.ComponentModel.Container();
            buttonPreviousRow = new Button();
            buttonNextRow = new Button();
            buttonApply = new Button();
            comboBoxTagType = new ComboBox();
            labelTagType = new Label();
            labelNPL = new Label();
            textBoxNPL = new TextBox();
            textBoxNPC = new TextBox();
            textBoxNPR = new TextBox();
            labelNPC = new Label();
            labelNPR = new Label();
            textBoxText = new TextBox();
            labelText = new Label();
            labelBlspd = new Label();
            labelBlrate = new Label();
            labelLiprate = new Label();
            numericUpDownBlspd = new NumericUpDown();
            numericUpDownBlrate = new NumericUpDown();
            numericUpDownLipspd = new NumericUpDown();
            labelMemo = new Label();
            textBoxMemo = new TextBox();
            comboBoxCast1 = new ComboBox();
            labelCast1 = new Label();
            textBoxActCast1 = new TextBox();
            textBoxActCast2 = new TextBox();
            comboBoxCast2 = new ComboBox();
            labelCast2 = new Label();
            checkBoxMirrorCast1 = new CheckBox();
            checkBoxMirrorCast2 = new CheckBox();
            checkBoxBillboard1 = new CheckBox();
            checkBoxBillboard2 = new CheckBox();
            checkBoxUseMapLight = new CheckBox();
            comboBoxWindowPosition = new ComboBox();
            labelWindowPosition = new Label();
            comboBoxSpeechBubble = new ComboBox();
            labelSpeechBubble = new Label();
            radioButtonTalkCast1 = new RadioButton();
            radioButtonTalkCast2 = new RadioButton();
            checkBoxWindowHidden = new CheckBox();
            pictureBoxCast1 = new PictureBox();
            pictureBoxCast2 = new PictureBox();
            panelPreview = new Panel();
            prevTableLayoutPanelBubble = new TableLayoutPanel();
            prevPictureBoxEvent = new PictureBox();
            prevPictureBoxPc4 = new PictureBox();
            prevPictureBoxPc3 = new PictureBox();
            prevPictureBoxPc2 = new PictureBox();
            prevPictureBoxPc1 = new PictureBox();
            prevTextBoxNPLCR = new TextBox();
            prevPictureBoxCast1 = new PictureBox();
            prevPictureBoxCast2 = new PictureBox();
            buttonReload = new Button();
            contextMenuStripInputHelper = new ContextMenuStrip(components);
            buttonStreamSkip = new Button();
            prevTextBoxNotes = new TextBox();
            prevTextBoxActCast1 = new TextBox();
            prevTextBoxActCast2 = new TextBox();
            buttonPreview = new Button();
            buttonDuplicateRow = new Button();
            checkBoxAutoScroll = new CheckBox();
            buttonMoveDown = new Button();
            buttonMoveUp = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownBlspd).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownBlrate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLipspd).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCast1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCast2).BeginInit();
            panelPreview.SuspendLayout();
            prevTableLayoutPanelBubble.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)prevPictureBoxEvent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)prevPictureBoxPc4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)prevPictureBoxPc3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)prevPictureBoxPc2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)prevPictureBoxPc1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)prevPictureBoxCast1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)prevPictureBoxCast2).BeginInit();
            SuspendLayout();
            // 
            // buttonPreviousRow
            // 
            buttonPreviousRow.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonPreviousRow.Location = new Point(152, 449);
            buttonPreviousRow.Name = "buttonPreviousRow";
            buttonPreviousRow.Size = new Size(100, 40);
            buttonPreviousRow.TabIndex = 27;
            buttonPreviousRow.Text = "<<Prev";
            buttonPreviousRow.UseVisualStyleBackColor = true;
            buttonPreviousRow.Click += buttonPreviousRow_Click;
            // 
            // buttonNextRow
            // 
            buttonNextRow.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonNextRow.Location = new Point(364, 449);
            buttonNextRow.Name = "buttonNextRow";
            buttonNextRow.Size = new Size(100, 40);
            buttonNextRow.TabIndex = 29;
            buttonNextRow.Text = "Next>>";
            buttonNextRow.UseVisualStyleBackColor = true;
            buttonNextRow.Click += buttonNextRow_Click;
            // 
            // buttonApply
            // 
            buttonApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonApply.Location = new Point(470, 449);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(100, 40);
            buttonApply.TabIndex = 32;
            buttonApply.Text = "Apply";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonApply_Click;
            // 
            // comboBoxTagType
            // 
            comboBoxTagType.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxTagType.FormattingEnabled = true;
            comboBoxTagType.Items.AddRange(new object[] { "Talk", "Message", "Notes" });
            comboBoxTagType.Location = new Point(576, 27);
            comboBoxTagType.Name = "comboBoxTagType";
            comboBoxTagType.Size = new Size(110, 23);
            comboBoxTagType.TabIndex = 0;
            comboBoxTagType.SelectedIndexChanged += comboBoxTagType_SelectedIndexChanged;
            // 
            // labelTagType
            // 
            labelTagType.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelTagType.AutoSize = true;
            labelTagType.Location = new Point(576, 9);
            labelTagType.Name = "labelTagType";
            labelTagType.Size = new Size(25, 15);
            labelTagType.TabIndex = 4;
            labelTagType.Text = "Tag";
            // 
            // labelNPL
            // 
            labelNPL.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelNPL.AutoSize = true;
            labelNPL.Location = new Point(576, 53);
            labelNPL.Name = "labelNPL";
            labelNPL.Size = new Size(29, 15);
            labelNPL.TabIndex = 5;
            labelNPL.Text = "NPL";
            // 
            // textBoxNPL
            // 
            textBoxNPL.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBoxNPL.Location = new Point(576, 71);
            textBoxNPL.Name = "textBoxNPL";
            textBoxNPL.Size = new Size(110, 23);
            textBoxNPL.TabIndex = 1;
            // 
            // textBoxNPC
            // 
            textBoxNPC.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBoxNPC.Location = new Point(692, 72);
            textBoxNPC.Name = "textBoxNPC";
            textBoxNPC.Size = new Size(110, 23);
            textBoxNPC.TabIndex = 2;
            // 
            // textBoxNPR
            // 
            textBoxNPR.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBoxNPR.Location = new Point(808, 72);
            textBoxNPR.Name = "textBoxNPR";
            textBoxNPR.Size = new Size(110, 23);
            textBoxNPR.TabIndex = 3;
            // 
            // labelNPC
            // 
            labelNPC.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelNPC.AutoSize = true;
            labelNPC.Location = new Point(692, 54);
            labelNPC.Name = "labelNPC";
            labelNPC.Size = new Size(30, 15);
            labelNPC.TabIndex = 9;
            labelNPC.Text = "NPC";
            // 
            // labelNPR
            // 
            labelNPR.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelNPR.AutoSize = true;
            labelNPR.Location = new Point(808, 54);
            labelNPR.Name = "labelNPR";
            labelNPR.Size = new Size(30, 15);
            labelNPR.TabIndex = 10;
            labelNPR.Text = "NPR";
            // 
            // textBoxText
            // 
            textBoxText.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBoxText.Location = new Point(576, 160);
            textBoxText.Multiline = true;
            textBoxText.Name = "textBoxText";
            textBoxText.ScrollBars = ScrollBars.Vertical;
            textBoxText.Size = new Size(356, 75);
            textBoxText.TabIndex = 7;
            // 
            // labelText
            // 
            labelText.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelText.AutoSize = true;
            labelText.Location = new Point(576, 142);
            labelText.Name = "labelText";
            labelText.Size = new Size(28, 15);
            labelText.TabIndex = 11;
            labelText.Text = "Text";
            // 
            // labelBlspd
            // 
            labelBlspd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelBlspd.AutoSize = true;
            labelBlspd.Location = new Point(576, 97);
            labelBlspd.Name = "labelBlspd";
            labelBlspd.Size = new Size(67, 15);
            labelBlspd.TabIndex = 13;
            labelBlspd.Text = "blspd (min)";
            // 
            // labelBlrate
            // 
            labelBlrate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelBlrate.AutoSize = true;
            labelBlrate.Location = new Point(692, 98);
            labelBlrate.Name = "labelBlrate";
            labelBlrate.Size = new Size(58, 15);
            labelBlrate.TabIndex = 14;
            labelBlrate.Text = "blrate (%)";
            // 
            // labelLiprate
            // 
            labelLiprate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelLiprate.AutoSize = true;
            labelLiprate.Location = new Point(807, 98);
            labelLiprate.Name = "labelLiprate";
            labelLiprate.Size = new Size(93, 15);
            labelLiprate.TabIndex = 15;
            labelLiprate.Text = "lipspd (multiple)";
            // 
            // numericUpDownBlspd
            // 
            numericUpDownBlspd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numericUpDownBlspd.Location = new Point(576, 116);
            numericUpDownBlspd.Name = "numericUpDownBlspd";
            numericUpDownBlspd.Size = new Size(110, 23);
            numericUpDownBlspd.TabIndex = 4;
            // 
            // numericUpDownBlrate
            // 
            numericUpDownBlrate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numericUpDownBlrate.Location = new Point(692, 116);
            numericUpDownBlrate.Name = "numericUpDownBlrate";
            numericUpDownBlrate.Size = new Size(110, 23);
            numericUpDownBlrate.TabIndex = 5;
            // 
            // numericUpDownLipspd
            // 
            numericUpDownLipspd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numericUpDownLipspd.DecimalPlaces = 2;
            numericUpDownLipspd.Location = new Point(807, 116);
            numericUpDownLipspd.Name = "numericUpDownLipspd";
            numericUpDownLipspd.Size = new Size(110, 23);
            numericUpDownLipspd.TabIndex = 6;
            // 
            // labelMemo
            // 
            labelMemo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelMemo.AutoSize = true;
            labelMemo.Location = new Point(576, 421);
            labelMemo.Name = "labelMemo";
            labelMemo.Size = new Size(41, 15);
            labelMemo.TabIndex = 19;
            labelMemo.Text = "Memo";
            // 
            // textBoxMemo
            // 
            textBoxMemo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            textBoxMemo.Location = new Point(576, 439);
            textBoxMemo.Multiline = true;
            textBoxMemo.Name = "textBoxMemo";
            textBoxMemo.ScrollBars = ScrollBars.Vertical;
            textBoxMemo.Size = new Size(356, 50);
            textBoxMemo.TabIndex = 22;
            // 
            // comboBoxCast1
            // 
            comboBoxCast1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxCast1.FormattingEnabled = true;
            comboBoxCast1.Location = new Point(634, 256);
            comboBoxCast1.Name = "comboBoxCast1";
            comboBoxCast1.Size = new Size(115, 23);
            comboBoxCast1.TabIndex = 8;
            comboBoxCast1.SelectedIndexChanged += comboBoxCast1_SelectedIndexChanged_1;
            // 
            // labelCast1
            // 
            labelCast1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelCast1.AutoSize = true;
            labelCast1.Location = new Point(576, 238);
            labelCast1.Name = "labelCast1";
            labelCast1.Size = new Size(35, 15);
            labelCast1.TabIndex = 22;
            labelCast1.Text = "Cast1";
            // 
            // textBoxActCast1
            // 
            textBoxActCast1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBoxActCast1.Location = new Point(634, 285);
            textBoxActCast1.Name = "textBoxActCast1";
            textBoxActCast1.Size = new Size(115, 23);
            textBoxActCast1.TabIndex = 9;
            // 
            // textBoxActCast2
            // 
            textBoxActCast2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBoxActCast2.Location = new Point(817, 285);
            textBoxActCast2.Name = "textBoxActCast2";
            textBoxActCast2.Size = new Size(115, 23);
            textBoxActCast2.TabIndex = 11;
            // 
            // comboBoxCast2
            // 
            comboBoxCast2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxCast2.FormattingEnabled = true;
            comboBoxCast2.Location = new Point(817, 256);
            comboBoxCast2.Name = "comboBoxCast2";
            comboBoxCast2.Size = new Size(115, 23);
            comboBoxCast2.TabIndex = 10;
            comboBoxCast2.SelectedIndexChanged += comboBoxCast2_SelectedIndexChanged_1;
            // 
            // labelCast2
            // 
            labelCast2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelCast2.AutoSize = true;
            labelCast2.Location = new Point(757, 238);
            labelCast2.Name = "labelCast2";
            labelCast2.Size = new Size(35, 15);
            labelCast2.TabIndex = 27;
            labelCast2.Text = "Cast2";
            // 
            // checkBoxMirrorCast1
            // 
            checkBoxMirrorCast1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBoxMirrorCast1.AutoSize = true;
            checkBoxMirrorCast1.Location = new Point(576, 339);
            checkBoxMirrorCast1.Name = "checkBoxMirrorCast1";
            checkBoxMirrorCast1.Size = new Size(90, 19);
            checkBoxMirrorCast1.TabIndex = 14;
            checkBoxMirrorCast1.Text = "Mirror Cast1";
            checkBoxMirrorCast1.UseVisualStyleBackColor = true;
            checkBoxMirrorCast1.CheckedChanged += checkBoxMirrorCast1_CheckedChanged;
            // 
            // checkBoxMirrorCast2
            // 
            checkBoxMirrorCast2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBoxMirrorCast2.AutoSize = true;
            checkBoxMirrorCast2.Location = new Point(757, 339);
            checkBoxMirrorCast2.Name = "checkBoxMirrorCast2";
            checkBoxMirrorCast2.Size = new Size(90, 19);
            checkBoxMirrorCast2.TabIndex = 16;
            checkBoxMirrorCast2.Text = "Mirror Cast2";
            checkBoxMirrorCast2.UseVisualStyleBackColor = true;
            checkBoxMirrorCast2.CheckedChanged += checkBoxMirrorCast2_CheckedChanged;
            // 
            // checkBoxBillboard1
            // 
            checkBoxBillboard1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBoxBillboard1.AutoSize = true;
            checkBoxBillboard1.Location = new Point(672, 339);
            checkBoxBillboard1.Name = "checkBoxBillboard1";
            checkBoxBillboard1.Size = new Size(79, 19);
            checkBoxBillboard1.TabIndex = 15;
            checkBoxBillboard1.Text = "Billboard1";
            checkBoxBillboard1.UseVisualStyleBackColor = true;
            // 
            // checkBoxBillboard2
            // 
            checkBoxBillboard2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBoxBillboard2.AutoSize = true;
            checkBoxBillboard2.Location = new Point(853, 339);
            checkBoxBillboard2.Name = "checkBoxBillboard2";
            checkBoxBillboard2.Size = new Size(79, 19);
            checkBoxBillboard2.TabIndex = 17;
            checkBoxBillboard2.Text = "Billboard2";
            checkBoxBillboard2.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseMapLight
            // 
            checkBoxUseMapLight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBoxUseMapLight.AutoSize = true;
            checkBoxUseMapLight.Location = new Point(623, 408);
            checkBoxUseMapLight.Name = "checkBoxUseMapLight";
            checkBoxUseMapLight.Size = new Size(102, 19);
            checkBoxUseMapLight.TabIndex = 20;
            checkBoxUseMapLight.Text = "Use Map Light";
            checkBoxUseMapLight.UseVisualStyleBackColor = true;
            // 
            // comboBoxWindowPosition
            // 
            comboBoxWindowPosition.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxWindowPosition.FormattingEnabled = true;
            comboBoxWindowPosition.Items.AddRange(new object[] { "Up", "Center", "Down", "Bubble(Player)", "Bubble(ThisEvent)", "Bubble(Member2)", "Bubble(Member3)", "Bubble(Member4)", "Bubble(Event)" });
            comboBoxWindowPosition.Location = new Point(576, 379);
            comboBoxWindowPosition.Name = "comboBoxWindowPosition";
            comboBoxWindowPosition.Size = new Size(175, 23);
            comboBoxWindowPosition.TabIndex = 18;
            comboBoxWindowPosition.SelectedIndexChanged += comboBoxWindowPosition_SelectedIndexChanged;
            // 
            // labelWindowPosition
            // 
            labelWindowPosition.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelWindowPosition.AutoSize = true;
            labelWindowPosition.Location = new Point(576, 361);
            labelWindowPosition.Name = "labelWindowPosition";
            labelWindowPosition.Size = new Size(97, 15);
            labelWindowPosition.TabIndex = 18;
            labelWindowPosition.Text = "Window Position";
            // 
            // comboBoxSpeechBubble
            // 
            comboBoxSpeechBubble.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxSpeechBubble.FormattingEnabled = true;
            comboBoxSpeechBubble.Location = new Point(757, 379);
            comboBoxSpeechBubble.Name = "comboBoxSpeechBubble";
            comboBoxSpeechBubble.Size = new Size(175, 23);
            comboBoxSpeechBubble.TabIndex = 19;
            // 
            // labelSpeechBubble
            // 
            labelSpeechBubble.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelSpeechBubble.AutoSize = true;
            labelSpeechBubble.Location = new Point(757, 361);
            labelSpeechBubble.Name = "labelSpeechBubble";
            labelSpeechBubble.Size = new Size(85, 15);
            labelSpeechBubble.TabIndex = 19;
            labelSpeechBubble.Text = "Speech Bubble";
            // 
            // radioButtonTalkCast1
            // 
            radioButtonTalkCast1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            radioButtonTalkCast1.AutoSize = true;
            radioButtonTalkCast1.Location = new Point(576, 314);
            radioButtonTalkCast1.Name = "radioButtonTalkCast1";
            radioButtonTalkCast1.Size = new Size(76, 19);
            radioButtonTalkCast1.TabIndex = 12;
            radioButtonTalkCast1.TabStop = true;
            radioButtonTalkCast1.Text = "Talk Cast1";
            radioButtonTalkCast1.UseVisualStyleBackColor = true;
            // 
            // radioButtonTalkCast2
            // 
            radioButtonTalkCast2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            radioButtonTalkCast2.AutoSize = true;
            radioButtonTalkCast2.Location = new Point(757, 314);
            radioButtonTalkCast2.Name = "radioButtonTalkCast2";
            radioButtonTalkCast2.Size = new Size(76, 19);
            radioButtonTalkCast2.TabIndex = 13;
            radioButtonTalkCast2.TabStop = true;
            radioButtonTalkCast2.Text = "Talk Cast2";
            radioButtonTalkCast2.UseVisualStyleBackColor = true;
            // 
            // checkBoxWindowHidden
            // 
            checkBoxWindowHidden.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBoxWindowHidden.AutoSize = true;
            checkBoxWindowHidden.Location = new Point(757, 408);
            checkBoxWindowHidden.Name = "checkBoxWindowHidden";
            checkBoxWindowHidden.Size = new Size(112, 19);
            checkBoxWindowHidden.TabIndex = 21;
            checkBoxWindowHidden.Text = "Window Hidden";
            checkBoxWindowHidden.UseVisualStyleBackColor = true;
            // 
            // pictureBoxCast1
            // 
            pictureBoxCast1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxCast1.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxCast1.Location = new Point(576, 256);
            pictureBoxCast1.Name = "pictureBoxCast1";
            pictureBoxCast1.Size = new Size(52, 52);
            pictureBoxCast1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxCast1.TabIndex = 42;
            pictureBoxCast1.TabStop = false;
            // 
            // pictureBoxCast2
            // 
            pictureBoxCast2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxCast2.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxCast2.Location = new Point(757, 256);
            pictureBoxCast2.Name = "pictureBoxCast2";
            pictureBoxCast2.Size = new Size(52, 52);
            pictureBoxCast2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxCast2.TabIndex = 43;
            pictureBoxCast2.TabStop = false;
            // 
            // panelPreview
            // 
            panelPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelPreview.BackColor = SystemColors.GradientInactiveCaption;
            panelPreview.BorderStyle = BorderStyle.FixedSingle;
            panelPreview.Controls.Add(prevTableLayoutPanelBubble);
            panelPreview.Controls.Add(prevTextBoxNPLCR);
            panelPreview.Controls.Add(prevPictureBoxCast1);
            panelPreview.Controls.Add(prevPictureBoxCast2);
            panelPreview.Location = new Point(12, 12);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new Size(558, 314);
            panelPreview.TabIndex = 44;
            panelPreview.SizeChanged += panelPreview_SizeChanged;
            // 
            // prevTableLayoutPanelBubble
            // 
            prevTableLayoutPanelBubble.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            prevTableLayoutPanelBubble.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            prevTableLayoutPanelBubble.BackColor = Color.Transparent;
            prevTableLayoutPanelBubble.ColumnCount = 5;
            prevTableLayoutPanelBubble.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            prevTableLayoutPanelBubble.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            prevTableLayoutPanelBubble.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            prevTableLayoutPanelBubble.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            prevTableLayoutPanelBubble.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            prevTableLayoutPanelBubble.Controls.Add(prevPictureBoxEvent, 4, 0);
            prevTableLayoutPanelBubble.Controls.Add(prevPictureBoxPc4, 3, 0);
            prevTableLayoutPanelBubble.Controls.Add(prevPictureBoxPc3, 2, 0);
            prevTableLayoutPanelBubble.Controls.Add(prevPictureBoxPc2, 1, 0);
            prevTableLayoutPanelBubble.Controls.Add(prevPictureBoxPc1, 0, 0);
            prevTableLayoutPanelBubble.Location = new Point(3, 200);
            prevTableLayoutPanelBubble.Name = "prevTableLayoutPanelBubble";
            prevTableLayoutPanelBubble.RowCount = 1;
            prevTableLayoutPanelBubble.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            prevTableLayoutPanelBubble.Size = new Size(550, 109);
            prevTableLayoutPanelBubble.TabIndex = 56;
            // 
            // prevPictureBoxEvent
            // 
            prevPictureBoxEvent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            prevPictureBoxEvent.BackColor = Color.Transparent;
            prevPictureBoxEvent.Image = Properties.Resources.prev_event;
            prevPictureBoxEvent.Location = new Point(443, 3);
            prevPictureBoxEvent.Name = "prevPictureBoxEvent";
            prevPictureBoxEvent.Size = new Size(104, 103);
            prevPictureBoxEvent.SizeMode = PictureBoxSizeMode.StretchImage;
            prevPictureBoxEvent.TabIndex = 55;
            prevPictureBoxEvent.TabStop = false;
            // 
            // prevPictureBoxPc4
            // 
            prevPictureBoxPc4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            prevPictureBoxPc4.BackColor = Color.Transparent;
            prevPictureBoxPc4.Image = Properties.Resources.prev_pc04;
            prevPictureBoxPc4.Location = new Point(333, 3);
            prevPictureBoxPc4.Name = "prevPictureBoxPc4";
            prevPictureBoxPc4.Size = new Size(104, 103);
            prevPictureBoxPc4.SizeMode = PictureBoxSizeMode.StretchImage;
            prevPictureBoxPc4.TabIndex = 54;
            prevPictureBoxPc4.TabStop = false;
            // 
            // prevPictureBoxPc3
            // 
            prevPictureBoxPc3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            prevPictureBoxPc3.BackColor = Color.Transparent;
            prevPictureBoxPc3.ErrorImage = Properties.Resources.prev_pc03;
            prevPictureBoxPc3.Image = Properties.Resources.prev_pc03;
            prevPictureBoxPc3.Location = new Point(223, 3);
            prevPictureBoxPc3.Name = "prevPictureBoxPc3";
            prevPictureBoxPc3.Size = new Size(104, 103);
            prevPictureBoxPc3.SizeMode = PictureBoxSizeMode.StretchImage;
            prevPictureBoxPc3.TabIndex = 53;
            prevPictureBoxPc3.TabStop = false;
            // 
            // prevPictureBoxPc2
            // 
            prevPictureBoxPc2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            prevPictureBoxPc2.BackColor = Color.Transparent;
            prevPictureBoxPc2.Image = Properties.Resources.prev_pc02;
            prevPictureBoxPc2.Location = new Point(113, 3);
            prevPictureBoxPc2.Name = "prevPictureBoxPc2";
            prevPictureBoxPc2.Size = new Size(104, 103);
            prevPictureBoxPc2.SizeMode = PictureBoxSizeMode.StretchImage;
            prevPictureBoxPc2.TabIndex = 52;
            prevPictureBoxPc2.TabStop = false;
            // 
            // prevPictureBoxPc1
            // 
            prevPictureBoxPc1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            prevPictureBoxPc1.BackColor = Color.Transparent;
            prevPictureBoxPc1.Image = Properties.Resources.prev_pc01;
            prevPictureBoxPc1.Location = new Point(3, 3);
            prevPictureBoxPc1.Name = "prevPictureBoxPc1";
            prevPictureBoxPc1.Size = new Size(104, 103);
            prevPictureBoxPc1.SizeMode = PictureBoxSizeMode.StretchImage;
            prevPictureBoxPc1.TabIndex = 51;
            prevPictureBoxPc1.TabStop = false;
            // 
            // prevTextBoxNPLCR
            // 
            prevTextBoxNPLCR.Anchor = AnchorStyles.None;
            prevTextBoxNPLCR.BackColor = SystemColors.Info;
            prevTextBoxNPLCR.Location = new Point(3, 3);
            prevTextBoxNPLCR.Name = "prevTextBoxNPLCR";
            prevTextBoxNPLCR.ReadOnly = true;
            prevTextBoxNPLCR.Size = new Size(108, 23);
            prevTextBoxNPLCR.TabIndex = 46;
            prevTextBoxNPLCR.TabStop = false;
            // 
            // prevPictureBoxCast1
            // 
            prevPictureBoxCast1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            prevPictureBoxCast1.BackColor = SystemColors.GradientInactiveCaption;
            prevPictureBoxCast1.Image = Properties.Resources.prev_dumy;
            prevPictureBoxCast1.Location = new Point(3, 75);
            prevPictureBoxCast1.Name = "prevPictureBoxCast1";
            prevPictureBoxCast1.Size = new Size(223, 234);
            prevPictureBoxCast1.SizeMode = PictureBoxSizeMode.StretchImage;
            prevPictureBoxCast1.TabIndex = 46;
            prevPictureBoxCast1.TabStop = false;
            // 
            // prevPictureBoxCast2
            // 
            prevPictureBoxCast2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            prevPictureBoxCast2.BackColor = SystemColors.GradientInactiveCaption;
            prevPictureBoxCast2.Image = Properties.Resources.prev_dumy2;
            prevPictureBoxCast2.Location = new Point(329, 75);
            prevPictureBoxCast2.Name = "prevPictureBoxCast2";
            prevPictureBoxCast2.Size = new Size(224, 234);
            prevPictureBoxCast2.SizeMode = PictureBoxSizeMode.StretchImage;
            prevPictureBoxCast2.TabIndex = 47;
            prevPictureBoxCast2.TabStop = false;
            // 
            // buttonReload
            // 
            buttonReload.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonReload.Location = new Point(12, 449);
            buttonReload.Name = "buttonReload";
            buttonReload.Size = new Size(100, 40);
            buttonReload.TabIndex = 26;
            buttonReload.Text = "Reload";
            buttonReload.UseVisualStyleBackColor = true;
            buttonReload.Click += buttonReload_Click;
            // 
            // contextMenuStripInputHelper
            // 
            contextMenuStripInputHelper.Name = "contextMenuStripInputHelper";
            contextMenuStripInputHelper.Size = new Size(61, 4);
            // 
            // buttonStreamSkip
            // 
            buttonStreamSkip.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonStreamSkip.Location = new Point(258, 449);
            buttonStreamSkip.Name = "buttonStreamSkip";
            buttonStreamSkip.Size = new Size(100, 40);
            buttonStreamSkip.TabIndex = 28;
            buttonStreamSkip.Text = "Skip streaming";
            buttonStreamSkip.UseVisualStyleBackColor = true;
            buttonStreamSkip.Click += buttonStreamSkip_Click;
            // 
            // prevTextBoxNotes
            // 
            prevTextBoxNotes.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            prevTextBoxNotes.BackColor = SystemColors.Info;
            prevTextBoxNotes.Location = new Point(118, 357);
            prevTextBoxNotes.Multiline = true;
            prevTextBoxNotes.Name = "prevTextBoxNotes";
            prevTextBoxNotes.ReadOnly = true;
            prevTextBoxNotes.ScrollBars = ScrollBars.Vertical;
            prevTextBoxNotes.Size = new Size(346, 86);
            prevTextBoxNotes.TabIndex = 48;
            prevTextBoxNotes.TabStop = false;
            // 
            // prevTextBoxActCast1
            // 
            prevTextBoxActCast1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            prevTextBoxActCast1.BackColor = SystemColors.GradientInactiveCaption;
            prevTextBoxActCast1.Location = new Point(12, 328);
            prevTextBoxActCast1.Name = "prevTextBoxActCast1";
            prevTextBoxActCast1.ReadOnly = true;
            prevTextBoxActCast1.Size = new Size(217, 23);
            prevTextBoxActCast1.TabIndex = 49;
            prevTextBoxActCast1.TabStop = false;
            // 
            // prevTextBoxActCast2
            // 
            prevTextBoxActCast2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            prevTextBoxActCast2.BackColor = SystemColors.GradientInactiveCaption;
            prevTextBoxActCast2.Location = new Point(353, 328);
            prevTextBoxActCast2.Name = "prevTextBoxActCast2";
            prevTextBoxActCast2.ReadOnly = true;
            prevTextBoxActCast2.Size = new Size(217, 23);
            prevTextBoxActCast2.TabIndex = 50;
            prevTextBoxActCast2.TabStop = false;
            // 
            // buttonPreview
            // 
            buttonPreview.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonPreview.Location = new Point(470, 403);
            buttonPreview.Name = "buttonPreview";
            buttonPreview.Size = new Size(100, 40);
            buttonPreview.TabIndex = 31;
            buttonPreview.Text = "Preview";
            buttonPreview.UseVisualStyleBackColor = true;
            buttonPreview.Click += buttonPreview_Click;
            // 
            // buttonDuplicateRow
            // 
            buttonDuplicateRow.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonDuplicateRow.Location = new Point(470, 357);
            buttonDuplicateRow.Name = "buttonDuplicateRow";
            buttonDuplicateRow.Size = new Size(100, 40);
            buttonDuplicateRow.TabIndex = 30;
            buttonDuplicateRow.Text = "Duplicate";
            buttonDuplicateRow.UseVisualStyleBackColor = true;
            buttonDuplicateRow.Click += buttonDuplicateRow_Click;
            // 
            // checkBoxAutoScroll
            // 
            checkBoxAutoScroll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            checkBoxAutoScroll.AutoSize = true;
            checkBoxAutoScroll.Checked = true;
            checkBoxAutoScroll.CheckState = CheckState.Checked;
            checkBoxAutoScroll.Location = new Point(249, 332);
            checkBoxAutoScroll.Name = "checkBoxAutoScroll";
            checkBoxAutoScroll.Size = new Size(83, 19);
            checkBoxAutoScroll.TabIndex = 23;
            checkBoxAutoScroll.Text = "Auto scroll";
            checkBoxAutoScroll.UseVisualStyleBackColor = true;
            // 
            // buttonMoveDown
            // 
            buttonMoveDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonMoveDown.Location = new Point(12, 403);
            buttonMoveDown.Name = "buttonMoveDown";
            buttonMoveDown.Size = new Size(100, 40);
            buttonMoveDown.TabIndex = 25;
            buttonMoveDown.Text = "Move ▼";
            buttonMoveDown.UseVisualStyleBackColor = true;
            buttonMoveDown.Click += buttonMoveDownRow_Click;
            // 
            // buttonMoveUp
            // 
            buttonMoveUp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonMoveUp.Location = new Point(12, 357);
            buttonMoveUp.Name = "buttonMoveUp";
            buttonMoveUp.Size = new Size(100, 40);
            buttonMoveUp.TabIndex = 24;
            buttonMoveUp.Text = "Move ▲";
            buttonMoveUp.UseVisualStyleBackColor = true;
            buttonMoveUp.Click += buttonMoveUpRow_Click;
            // 
            // FormPreviewEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 501);
            Controls.Add(buttonMoveUp);
            Controls.Add(buttonMoveDown);
            Controls.Add(checkBoxAutoScroll);
            Controls.Add(buttonDuplicateRow);
            Controls.Add(buttonPreview);
            Controls.Add(prevTextBoxActCast2);
            Controls.Add(prevTextBoxActCast1);
            Controls.Add(prevTextBoxNotes);
            Controls.Add(buttonStreamSkip);
            Controls.Add(panelPreview);
            Controls.Add(checkBoxWindowHidden);
            Controls.Add(radioButtonTalkCast2);
            Controls.Add(radioButtonTalkCast1);
            Controls.Add(labelSpeechBubble);
            Controls.Add(comboBoxSpeechBubble);
            Controls.Add(labelWindowPosition);
            Controls.Add(comboBoxWindowPosition);
            Controls.Add(checkBoxUseMapLight);
            Controls.Add(checkBoxBillboard2);
            Controls.Add(checkBoxBillboard1);
            Controls.Add(checkBoxMirrorCast2);
            Controls.Add(checkBoxMirrorCast1);
            Controls.Add(labelCast2);
            Controls.Add(textBoxActCast2);
            Controls.Add(comboBoxCast2);
            Controls.Add(textBoxActCast1);
            Controls.Add(labelCast1);
            Controls.Add(comboBoxCast1);
            Controls.Add(textBoxMemo);
            Controls.Add(labelMemo);
            Controls.Add(numericUpDownLipspd);
            Controls.Add(numericUpDownBlrate);
            Controls.Add(numericUpDownBlspd);
            Controls.Add(labelLiprate);
            Controls.Add(labelBlrate);
            Controls.Add(labelBlspd);
            Controls.Add(textBoxText);
            Controls.Add(labelText);
            Controls.Add(labelNPR);
            Controls.Add(labelNPC);
            Controls.Add(textBoxNPR);
            Controls.Add(textBoxNPC);
            Controls.Add(textBoxNPL);
            Controls.Add(labelNPL);
            Controls.Add(labelTagType);
            Controls.Add(comboBoxTagType);
            Controls.Add(pictureBoxCast1);
            Controls.Add(pictureBoxCast2);
            Controls.Add(buttonApply);
            Controls.Add(buttonNextRow);
            Controls.Add(buttonPreviousRow);
            Controls.Add(buttonReload);
            KeyPreview = true;
            MinimumSize = new Size(960, 540);
            Name = "FormPreviewEditor";
            Text = "Preview Editor";
            FormClosing += FormPreviewEditor_FormClosing;
            FormClosed += FormPreviewEditor_FormClosed;
            Shown += UpdatePreview;
            KeyDown += FormPreviewEditor_KeyDown;
            ((System.ComponentModel.ISupportInitialize)numericUpDownBlspd).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownBlrate).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLipspd).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCast1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCast2).EndInit();
            panelPreview.ResumeLayout(false);
            panelPreview.PerformLayout();
            prevTableLayoutPanelBubble.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)prevPictureBoxEvent).EndInit();
            ((System.ComponentModel.ISupportInitialize)prevPictureBoxPc4).EndInit();
            ((System.ComponentModel.ISupportInitialize)prevPictureBoxPc3).EndInit();
            ((System.ComponentModel.ISupportInitialize)prevPictureBoxPc2).EndInit();
            ((System.ComponentModel.ISupportInitialize)prevPictureBoxPc1).EndInit();
            ((System.ComponentModel.ISupportInitialize)prevPictureBoxCast1).EndInit();
            ((System.ComponentModel.ISupportInitialize)prevPictureBoxCast2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonPreviousRow;
        private Button buttonNextRow;
        private Button buttonApply;
        private ComboBox comboBoxTagType;
        private Label labelTagType;
        private Label labelNPL;
        private TextBox textBoxNPL;
        private TextBox textBoxNPC;
        private TextBox textBoxNPR;
        private Label labelNPC;
        private Label labelNPR;
        private TextBox textBoxText;
        private Label labelText;
        private Label labelBlspd;
        private Label labelBlrate;
        private Label labelLiprate;
        private NumericUpDown numericUpDownBlspd;
        private NumericUpDown numericUpDownBlrate;
        private NumericUpDown numericUpDownLipspd;
        private Label labelMemo;
        private TextBox textBoxMemo;
        private ComboBox comboBoxCast1;
        private Label labelCast1;
        private TextBox textBoxActCast1;
        private TextBox textBoxActCast2;
        private ComboBox comboBoxCast2;
        private Label labelCast2;
        private CheckBox checkBoxMirrorCast1;
        private CheckBox checkBoxMirrorCast2;
        private CheckBox checkBoxBillboard1;
        private CheckBox checkBoxBillboard2;
        private CheckBox checkBoxUseMapLight;
        private ComboBox comboBoxWindowPosition;
        private Label labelWindowPosition;
        private ComboBox comboBoxSpeechBubble;
        private Label labelSpeechBubble;
        private RadioButton radioButtonTalkCast1;
        private RadioButton radioButtonTalkCast2;
        private CheckBox checkBoxWindowHidden;
        private PictureBox pictureBoxCast1;
        private PictureBox pictureBoxCast2;
        private Panel panelPreview;
        private Button buttonReload;
        private ContextMenuStrip contextMenuStripInputHelper;
        private PictureBox prevPictureBoxCast2;
        private PictureBox prevPictureBoxCast1;
        private TextBox prevTextBoxNPLCR;
        private Button buttonStreamSkip;
        private TextBox prevTextBoxNotes;
        private TextBox prevTextBoxActCast1;
        private TextBox prevTextBoxActCast2;
        private PictureBox prevPictureBoxEvent;
        private PictureBox prevPictureBoxPc4;
        private PictureBox prevPictureBoxPc3;
        private PictureBox prevPictureBoxPc2;
        private PictureBox prevPictureBoxPc1;
        private Button buttonPreview;
        private TableLayoutPanel prevTableLayoutPanelBubble;
        private Button buttonDuplicateRow;
        private CheckBox checkBoxAutoScroll;
        private Button buttonMoveDown;
        private Button buttonMoveUp;
    }
}