namespace ClipboardToolForBakin2
{
    public class PreviewEditorEventArgs : EventArgs
    {
        public int Change { get; }

        public PreviewEditorEventArgs(int change)
        {
            this.Change = change;
        }
    }

    public class AddRowEventArgs : EventArgs
    {
        public int Add { get; }

        public AddRowEventArgs(int add)
        {
            this.Add = add;
        }
    }

    public class SwapRowEventArgs : EventArgs
    {
        public int Change { get; }

        public SwapRowEventArgs(int change)
        {
            this.Change = change;
        }
    }
    public class SaveCSVEventArgs : EventArgs
    {
        public bool overwrite { get; }
        public SaveCSVEventArgs(bool overwrite)
        {
            this.overwrite = overwrite;     
        }
    }
}