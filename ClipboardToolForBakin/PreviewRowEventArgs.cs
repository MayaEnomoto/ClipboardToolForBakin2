namespace ClipboardToolForBakin2
{
    public class PreviewRowEventArgs : EventArgs
    {
        public int Change { get; }

        public PreviewRowEventArgs(int change)
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
}