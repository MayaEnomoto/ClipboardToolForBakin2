namespace AsImageProcessingLibrary
{
    public class ImageConfiguration
    {
        public string Filepath { get; set; } = string.Empty;
        public string ResourceName { get; set; } = string.Empty;
        public bool FlipHorizontal { get; set; }
        public float Brightness { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is ImageConfiguration conf)
            {
                return (Filepath == conf.Filepath || (Filepath != null && Filepath.Equals(conf.Filepath)) ||
                        (ResourceName != null && ResourceName.Equals(conf.ResourceName))) &&
                       FlipHorizontal == conf.FlipHorizontal &&
                       Brightness == conf.Brightness;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Filepath, ResourceName, FlipHorizontal, Brightness);
        }
    }
}
