using System.Drawing;
using System.Drawing.Imaging;
using Image = SixLabors.ImageSharp.Image;
using Point = SixLabors.ImageSharp.Point;
using Size = SixLabors.ImageSharp.Size;

namespace AsImageProcessingLibrary
{
    public class ImageProcessor : IDisposable
    {
        private Dictionary<string, Image<Rgba32>> originalImageCache = new Dictionary<string, Image<Rgba32>>();
        private Dictionary<ImageConfiguration, Image<Rgba32>> processedImageCache = new Dictionary<ImageConfiguration, Image<Rgba32>>();

        public void CacheImage(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filepath));
            }
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException("The specified file does not exist.", filepath);
            }
            if (!originalImageCache.ContainsKey(filepath))
            {
                var image = Image.Load<Rgba32>(filepath);
                originalImageCache[filepath] = image;
            }
        }

        public void CacheImageFromBitmap(string resourceName, Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }
            using var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Png);
            memoryStream.Position = 0;
            var image = Image.Load<Rgba32>(memoryStream);
            originalImageCache[resourceName] = image;
        }

        public Image<Rgba32> GetImage(string filepathOrResourceName, bool flipHorizontal, float brightness, bool isResource = false)
        {
            Image<Rgba32> image;

            if (string.IsNullOrEmpty(filepathOrResourceName))
            {
                throw new ArgumentException("File path or resource name cannot be null or empty.", nameof(filepathOrResourceName));
            }

            if (originalImageCache.ContainsKey(filepathOrResourceName))
            {
                image = originalImageCache[filepathOrResourceName].Clone();
            }
            else
            {
                if (!File.Exists(filepathOrResourceName))
                {
                    throw new FileNotFoundException("The specified file does not exist.", filepathOrResourceName);
                }
                image = Image.Load<Rgba32>(filepathOrResourceName);
                originalImageCache[filepathOrResourceName] = image.Clone();
            }

            if (flipHorizontal)
            {
                image.Mutate(x => x.Flip(FlipMode.Horizontal));
            }

            if (brightness != 1.0f)
            {
                image.Mutate(x => x.Brightness(brightness));
            }
            return image;
        }

        public class ImagePositionAndSize
        {
            public Point Position { get; set; }
            public Size Size { get; set; }
        }

        public Image<Rgba32> MergeImages(List<(Image<Rgba32> Image, ImagePositionAndSize PositionAndSize)> images)
        {
            var finalImage = new Image<Rgba32>(1280, 768, new Rgba32(180, 190, 210, 255));
            foreach (var (image, positionAndSize) in images)
            {
                var resizedImage = image.Clone(ctx => ctx.Resize(positionAndSize.Size));
                finalImage.Mutate(x => x.DrawImage(resizedImage, positionAndSize.Position, 1f));
            }
            return finalImage;
        }

        public Image<Rgba32> MergePictureBoxImages(List<(Image<Rgba32> Image, ImagePositionAndSize PositionAndSize)> images)
        {
            var finalImage = new Image<Rgba32>(52, 52, new Rgba32(180, 190, 210, 255));
            foreach (var (image, positionAndSize) in images)
            {
                var resizedImage = image.Clone(ctx => ctx.Resize(positionAndSize.Size));
                finalImage.Mutate(x => x.DrawImage(resizedImage, positionAndSize.Position, 1f));
            }
            return finalImage;
        }

        public System.Drawing.Image GetAsDrawingImage(Image<Rgba32> image)
        {
            using (var ms = new MemoryStream())
            {
                image.SaveAsBmp(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return new System.Drawing.Bitmap(ms);
            }
        }

        public void ClearProcessedImageCache()
        {
            foreach (var image in processedImageCache.Values)
            {
                image.Dispose();
            }
            processedImageCache.Clear();
        }

        public void Dispose()
        {
            foreach (var image in originalImageCache.Values)
            {
                image.Dispose();
            }

            foreach (var image in processedImageCache.Values)
            {
                image.Dispose();
            }
        }
    }
}