namespace ClipboardToolForBakin
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            dataGridView = new DataGridView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            InsertNewRowToolStripMenuItem = new ToolStripMenuItem();
            copyToClipboardWithSoundToolStripMenuItem = new ToolStripMenuItem();
            copyToClipboardToolStripMenuItem = new ToolStripMenuItem();
            pasteFromClipboardToolStripMenuItem = new ToolStripMenuItem();
            deleteSelectedRowsToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItemFileOpen = new ToolStripMenuItem();
            toolStripMenuItemSaveAs = new ToolStripMenuItem();
            toolStripMenuItemFileSave = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            previewEditorToolStripMenuItem = new ToolStripMenuItem();
            uUIDToolStripMenuItem = new ToolStripMenuItem();
            uUIDDefinitionToolStripMenuItem = new ToolStripMenuItem();
            uUIDReplacerToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            viewChangeToolStripMenuItem = new ToolStripMenuItem();
            shortcutKeysToolStripMenuItem = new ToolStripMenuItem();
            buttonCopyToClipboard = new Button();
            buttonPasteFromClipboard = new Button();
            buttonDeleteRows = new Button();
            buttonInsertNewRow = new Button();
            buttonPreviewEditor = new Button();
            buttonUUIDdefinition = new Button();
            buttonReplaceUUID = new Button();
            toolTip1 = new ToolTip(components);
            buttonCopyToClipboardWithSound = new Button();
            textToSoundsToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            contextMenuStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.AllowDrop = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView.ColumnHeadersHeight = 46;
            dataGridView.ContextMenuStrip = contextMenuStrip1;
            dataGridView.Location = new Point(12, 27);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 82;
            dataGridView.RowTemplate.Height = 25;
            dataGridView.Size = new Size(764, 466);
            dataGridView.TabIndex = 1;
            dataGridView.CellEndEdit += DataGridView_CellEndEdit;
            dataGridView.CellValidating += DataGridView_CellValidating;
            dataGridView.SelectionChanged += dataGridView_SelectionChanged;
            dataGridView.DragDrop += dataGridView_DragDrop;
            dataGridView.DragOver += dataGridView_DragOver;
            dataGridView.MouseDown += dataGridView_MouseDown;
            dataGridView.MouseMove += dataGridView_MouseMove;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(32, 32);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { InsertNewRowToolStripMenuItem, copyToClipboardWithSoundToolStripMenuItem, copyToClipboardToolStripMenuItem, pasteFromClipboardToolStripMenuItem, deleteSelectedRowsToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(238, 114);
            contextMenuStrip1.Opening += contextMenuStrip_Opening;
            // 
            // InsertNewRowToolStripMenuItem
            // 
            InsertNewRowToolStripMenuItem.Name = "InsertNewRowToolStripMenuItem";
            InsertNewRowToolStripMenuItem.Size = new Size(237, 22);
            InsertNewRowToolStripMenuItem.Text = "Insert new Row";
            InsertNewRowToolStripMenuItem.Click += addNewRowToolStripMenuItem_Click;
            // 
            // copyToClipboardWithSoundToolStripMenuItem
            // 
            copyToClipboardWithSoundToolStripMenuItem.Name = "copyToClipboardWithSoundToolStripMenuItem";
            copyToClipboardWithSoundToolStripMenuItem.Size = new Size(237, 22);
            copyToClipboardWithSoundToolStripMenuItem.Text = "Copy to Clipboard with Sounds";
            copyToClipboardWithSoundToolStripMenuItem.Click += buttonCopyToClipboardWithSound_Click;
            // 
            // copyToClipboardToolStripMenuItem
            // 
            copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            copyToClipboardToolStripMenuItem.Size = new Size(237, 22);
            copyToClipboardToolStripMenuItem.Text = "Copy to Clipboard";
            copyToClipboardToolStripMenuItem.Click += copyToClipboardToolStripMenuItem_Click;
            // 
            // pasteFromClipboardToolStripMenuItem
            // 
            pasteFromClipboardToolStripMenuItem.Name = "pasteFromClipboardToolStripMenuItem";
            pasteFromClipboardToolStripMenuItem.Size = new Size(237, 22);
            pasteFromClipboardToolStripMenuItem.Text = "Pasete from Clipboard";
            pasteFromClipboardToolStripMenuItem.Click += pasteFromClipboardToolStripMenuItem_Click;
            // 
            // deleteSelectedRowsToolStripMenuItem
            // 
            deleteSelectedRowsToolStripMenuItem.Name = "deleteSelectedRowsToolStripMenuItem";
            deleteSelectedRowsToolStripMenuItem.Size = new Size(237, 22);
            deleteSelectedRowsToolStripMenuItem.Text = "Delete selected Rows";
            deleteSelectedRowsToolStripMenuItem.Click += deleteSelectedRowsToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, uUIDToolStripMenuItem, settingsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(944, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, toolStripMenuItemFileOpen, toolStripMenuItemSaveAs, toolStripMenuItemFileSave, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newToolStripMenuItem.Size = new Size(194, 22);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // toolStripMenuItemFileOpen
            // 
            toolStripMenuItemFileOpen.Name = "toolStripMenuItemFileOpen";
            toolStripMenuItemFileOpen.ShortcutKeys = Keys.Control | Keys.O;
            toolStripMenuItemFileOpen.Size = new Size(194, 22);
            toolStripMenuItemFileOpen.Text = "Open";
            toolStripMenuItemFileOpen.Click += OpenToolStripMenuItem_Click;
            // 
            // toolStripMenuItemSaveAs
            // 
            toolStripMenuItemSaveAs.Name = "toolStripMenuItemSaveAs";
            toolStripMenuItemSaveAs.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            toolStripMenuItemSaveAs.Size = new Size(194, 22);
            toolStripMenuItemSaveAs.Text = "Save As...";
            toolStripMenuItemSaveAs.Click += SaveAsToolStripMenuItem_Click;
            // 
            // toolStripMenuItemFileSave
            // 
            toolStripMenuItemFileSave.Name = "toolStripMenuItemFileSave";
            toolStripMenuItemFileSave.ShortcutKeys = Keys.Control | Keys.S;
            toolStripMenuItemFileSave.Size = new Size(194, 22);
            toolStripMenuItemFileSave.Text = "Save";
            toolStripMenuItemFileSave.Click += SaveToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            exitToolStripMenuItem.Size = new Size(194, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { previewEditorToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // previewEditorToolStripMenuItem
            // 
            previewEditorToolStripMenuItem.Name = "previewEditorToolStripMenuItem";
            previewEditorToolStripMenuItem.ShortcutKeys = Keys.F11;
            previewEditorToolStripMenuItem.Size = new Size(180, 22);
            previewEditorToolStripMenuItem.Text = "Preview Editor";
            previewEditorToolStripMenuItem.Click += buttonPreviewEditor_Click;
            // 
            // uUIDToolStripMenuItem
            // 
            uUIDToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { uUIDDefinitionToolStripMenuItem, uUIDReplacerToolStripMenuItem });
            uUIDToolStripMenuItem.Name = "uUIDToolStripMenuItem";
            uUIDToolStripMenuItem.Size = new Size(46, 20);
            uUIDToolStripMenuItem.Text = "UUID";
            // 
            // uUIDDefinitionToolStripMenuItem
            // 
            uUIDDefinitionToolStripMenuItem.Name = "uUIDDefinitionToolStripMenuItem";
            uUIDDefinitionToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.U;
            uUIDDefinitionToolStripMenuItem.Size = new Size(210, 22);
            uUIDDefinitionToolStripMenuItem.Text = "UUID definition";
            uUIDDefinitionToolStripMenuItem.Click += buttonUUIDDefinition_Click;
            // 
            // uUIDReplacerToolStripMenuItem
            // 
            uUIDReplacerToolStripMenuItem.Name = "uUIDReplacerToolStripMenuItem";
            uUIDReplacerToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Alt | Keys.U;
            uUIDReplacerToolStripMenuItem.Size = new Size(210, 22);
            uUIDReplacerToolStripMenuItem.Text = "UUID replacer";
            uUIDReplacerToolStripMenuItem.Click += buttonReplaceUUID_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { viewChangeToolStripMenuItem, shortcutKeysToolStripMenuItem, textToSoundsToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // viewChangeToolStripMenuItem
            // 
            viewChangeToolStripMenuItem.Name = "viewChangeToolStripMenuItem";
            viewChangeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D;
            viewChangeToolStripMenuItem.Size = new Size(185, 22);
            viewChangeToolStripMenuItem.Text = "View Change";
            viewChangeToolStripMenuItem.Click += buttonViewChange_Click;
            // 
            // shortcutKeysToolStripMenuItem
            // 
            shortcutKeysToolStripMenuItem.Name = "shortcutKeysToolStripMenuItem";
            shortcutKeysToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.K;
            shortcutKeysToolStripMenuItem.Size = new Size(185, 22);
            shortcutKeysToolStripMenuItem.Text = "Shortcut keys";
            shortcutKeysToolStripMenuItem.Click += shortcutKeysToolStripMenuItem_Click;
            // 
            // buttonCopyToClipboard
            // 
            buttonCopyToClipboard.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCopyToClipboard.Enabled = false;
            buttonCopyToClipboard.Location = new Point(782, 387);
            buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            buttonCopyToClipboard.Size = new Size(150, 50);
            buttonCopyToClipboard.TabIndex = 8;
            buttonCopyToClipboard.Text = "Copy to Clipboard";
            buttonCopyToClipboard.UseVisualStyleBackColor = true;
            buttonCopyToClipboard.Click += buttonCopyToClipboard_Click;
            // 
            // buttonPasteFromClipboard
            // 
            buttonPasteFromClipboard.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonPasteFromClipboard.Enabled = false;
            buttonPasteFromClipboard.Location = new Point(782, 443);
            buttonPasteFromClipboard.Name = "buttonPasteFromClipboard";
            buttonPasteFromClipboard.Size = new Size(150, 50);
            buttonPasteFromClipboard.TabIndex = 9;
            buttonPasteFromClipboard.Text = "Paste from Clipboard";
            buttonPasteFromClipboard.UseVisualStyleBackColor = true;
            buttonPasteFromClipboard.Click += buttonPasteFromClipboard_Click;
            // 
            // buttonDeleteRows
            // 
            buttonDeleteRows.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonDeleteRows.Location = new Point(782, 275);
            buttonDeleteRows.Name = "buttonDeleteRows";
            buttonDeleteRows.Size = new Size(150, 50);
            buttonDeleteRows.TabIndex = 6;
            buttonDeleteRows.Text = "Delete selected Rows";
            buttonDeleteRows.UseVisualStyleBackColor = true;
            buttonDeleteRows.Click += buttonDeleteRows_Click;
            // 
            // buttonInsertNewRow
            // 
            buttonInsertNewRow.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonInsertNewRow.Location = new Point(782, 219);
            buttonInsertNewRow.Name = "buttonInsertNewRow";
            buttonInsertNewRow.Size = new Size(150, 50);
            buttonInsertNewRow.TabIndex = 5;
            buttonInsertNewRow.Text = "Insert new Row";
            buttonInsertNewRow.UseVisualStyleBackColor = true;
            buttonInsertNewRow.Click += buttonInsertNewRowsBeforeSelection_Click;
            // 
            // buttonPreviewEditor
            // 
            buttonPreviewEditor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonPreviewEditor.Location = new Point(782, 27);
            buttonPreviewEditor.Name = "buttonPreviewEditor";
            buttonPreviewEditor.Size = new Size(150, 50);
            buttonPreviewEditor.TabIndex = 2;
            buttonPreviewEditor.Text = "Preview Editor";
            buttonPreviewEditor.UseVisualStyleBackColor = true;
            buttonPreviewEditor.Click += buttonPreviewEditor_Click;
            // 
            // buttonUUIDdefinition
            // 
            buttonUUIDdefinition.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonUUIDdefinition.Location = new Point(782, 83);
            buttonUUIDdefinition.Name = "buttonUUIDdefinition";
            buttonUUIDdefinition.Size = new Size(150, 50);
            buttonUUIDdefinition.TabIndex = 3;
            buttonUUIDdefinition.Text = "UUID definition";
            buttonUUIDdefinition.UseVisualStyleBackColor = true;
            buttonUUIDdefinition.Click += buttonUUIDDefinition_Click;
            // 
            // buttonReplaceUUID
            // 
            buttonReplaceUUID.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonReplaceUUID.Location = new Point(782, 139);
            buttonReplaceUUID.Name = "buttonReplaceUUID";
            buttonReplaceUUID.Size = new Size(150, 50);
            buttonReplaceUUID.TabIndex = 4;
            buttonReplaceUUID.Text = "UUID replacer";
            buttonReplaceUUID.UseVisualStyleBackColor = true;
            buttonReplaceUUID.Click += buttonReplaceUUID_Click;
            // 
            // buttonCopyToClipboardWithSound
            // 
            buttonCopyToClipboardWithSound.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCopyToClipboardWithSound.Enabled = false;
            buttonCopyToClipboardWithSound.Location = new Point(782, 331);
            buttonCopyToClipboardWithSound.Name = "buttonCopyToClipboardWithSound";
            buttonCopyToClipboardWithSound.Size = new Size(150, 50);
            buttonCopyToClipboardWithSound.TabIndex = 7;
            buttonCopyToClipboardWithSound.Text = "Copy to Clipboard with Sounds";
            buttonCopyToClipboardWithSound.UseVisualStyleBackColor = true;
            buttonCopyToClipboardWithSound.Click += buttonCopyToClipboardWithSound_Click;
            // 
            // textToSoundsToolStripMenuItem
            // 
            textToSoundsToolStripMenuItem.Name = "textToSoundsToolStripMenuItem";
            textToSoundsToolStripMenuItem.Size = new Size(185, 22);
            textToSoundsToolStripMenuItem.Text = "Text to Sounds";
            textToSoundsToolStripMenuItem.Click += textToSoundsToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 501);
            Controls.Add(buttonCopyToClipboardWithSound);
            Controls.Add(buttonReplaceUUID);
            Controls.Add(buttonUUIDdefinition);
            Controls.Add(buttonPreviewEditor);
            Controls.Add(buttonInsertNewRow);
            Controls.Add(buttonDeleteRows);
            Controls.Add(buttonPasteFromClipboard);
            Controls.Add(buttonCopyToClipboard);
            Controls.Add(dataGridView);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(956, 540);
            Name = "MainForm";
            Text = "Clipboard Tool for Bakin 2";
            KeyDown += MainForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView;
        private MenuStrip menuStrip1;
        private Button buttonCopyToClipboard;
        private Button buttonPasteFromClipboard;
        private Button buttonDeleteRows;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem pasteFromClipboardToolStripMenuItem;
        private ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private ToolStripMenuItem deleteSelectedRowsToolStripMenuItem;
        private Button buttonInsertNewRow;
        private ToolStripMenuItem InsertNewRowToolStripMenuItem;
        private Button buttonPreviewEditor;
        private Button buttonUUIDdefinition;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItemFileOpen;
        private ToolStripMenuItem toolStripMenuItemFileSave;
        private ToolStripMenuItem toolStripMenuItemSaveAs;
        private Button buttonReplaceUUID;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem shortcutKeysToolStripMenuItem;
        private ToolStripMenuItem viewChangeToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem uUIDToolStripMenuItem;
        private ToolStripMenuItem uUIDDefinitionToolStripMenuItem;
        private ToolStripMenuItem uUIDReplacerToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem previewEditorToolStripMenuItem;
        private ToolTip toolTip1;
        private ToolStripMenuItem copyToClipboardWithSoundToolStripMenuItem;
        private Button buttonCopyToClipboardWithSound;
        private ToolStripMenuItem textToSoundsToolStripMenuItem;
    }
}