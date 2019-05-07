using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ResizeImage
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Resize(args[0], int.Parse(args[1]), int.Parse(args[2]));
            ResizeImageWithAspectRatio(args[0], int.Parse(args[1]), int.Parse(args[2]));
        }

        /// <summary>
        ///     Resize a given image
        /// </summary>
        /// <param name="filePath">input file with path</param>
        /// <param name="width">width of resized image</param>
        /// <param name="height">height of resized image</param>
        private static void Resize(string filePath, int width, int height)
        {
            var file = filePath;
            Console.WriteLine($"Loading {file}");
            using (var imageStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            using (var image = new Bitmap(imageStream))
            {
                var resizedImage = new Bitmap(width, height);
                using (var graphics = Graphics.FromImage(resizedImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.DrawImage(image, 0, 0, width, height);
                    var newFilePath = $"{Path.GetDirectoryName(file)}\\{Path.GetFileNameWithoutExtension(file)}_{width}x{height}.png";
                    resizedImage.Save(
                        newFilePath,
                        ImageFormat.Png);
                    Console.WriteLine($"Saving {newFilePath}");
                }
            }
        }
        /// <summary>
        ///  Resize a given image by maintaining the aspect ratio.
        /// </summary>
        /// <param name="filePath">input file with path</param>
        /// <param name="width">width of resized image</param>
        /// <param name="height">height of resized image</param>
        private static void ResizeImageWithAspectRatio(string filePath, int width, int height)
        {
            Console.WriteLine($"Loading {filePath}");
            using (var imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var image = new Bitmap(imageStream))
            {
                var thumbnail = new Bitmap(width, height);
                using (var graphic = Graphics.FromImage(thumbnail))
                {
                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphic.CompositingQuality = CompositingQuality.HighQuality;
                    graphic.SmoothingMode = SmoothingMode.HighQuality;
                    graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    var ratioX = width / (double)image.Width;
                    var ratioY = height / (double)image.Height;

                    var ratio = ratioX < ratioY ? ratioX : ratioY;

                    var newHeight = Convert.ToInt32(image.Height * ratio);
                    var newWidth = Convert.ToInt32(image.Width * ratio);

                    var posX = Convert.ToInt32((width - image.Width * ratio) / 2);
                    var posY = Convert.ToInt32((height - image.Height * ratio) / 2);

                    graphic.Clear(Color.White);
                    graphic.DrawImage(image, posX, posY, newWidth, newHeight);
                    var newFilePath =
                        $"{Path.GetDirectoryName(filePath)}\\{Path.GetFileNameWithoutExtension(filePath)}_{width}x{height}_ratio.png";
                    thumbnail.Save(newFilePath,
                        ImageFormat.Png);
                    Console.WriteLine($"Saving {newFilePath}");
                }
            }
        }
    }
}