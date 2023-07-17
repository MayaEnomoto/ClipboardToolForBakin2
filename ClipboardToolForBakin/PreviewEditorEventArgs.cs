namespace ClipboardToolForBakin2
{
    public class PreviewEditorEventArgs : EventArgs
    {
        public int Change { get; }

        public PreviewEditorEventArgs(int change)
        {
            Change = change;
        }
    }

    public class AddRowEventArgs : EventArgs
    {
        public int Add { get; }

        public AddRowEventArgs(int add)
        {
            Add = add;
        }
    }

    public class SwapRowEventArgs : EventArgs
    {
        public int Change { get; }

        public SwapRowEventArgs(int change)
        {
            Change = change;
        }
    }
    public class SaveCSVEventArgs : EventArgs
    {
        public SaveCSVEventArgs()
        {
            ;
        }
    }
}