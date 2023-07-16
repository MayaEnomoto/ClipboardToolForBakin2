namespace ClipboardToolForBakin2
{
    public class RowChangeEventArgs : EventArgs
    {
        public int Change { get; }

        public RowChangeEventArgs(int change)
        {
            Change = change;
        }
    }
}