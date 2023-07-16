namespace ClipboardToolForBakin2
{
    public class ResourceItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string ImagePath { get; set; }

        public ResourceItem(string key, string value, string imagePath)
        {
            Key = key;
            Value = value;
            ImagePath = imagePath;
        }
    }
}
