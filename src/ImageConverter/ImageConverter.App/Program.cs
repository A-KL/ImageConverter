using System;
using System.IO;
using System.Reflection;
using ImageMagick;

namespace ImageConverter.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = args.Length > 0 ? args : Directory.GetFiles(Path.Combine(AssemblyDirectory, "Images"));

            foreach (var file in files)
            {
                Convert(file);
            }
        }

        public static void Convert(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (var magickImage = new MagickImage(filePath))
            {
                using (var output = File.OpenWrite(Path.Combine(AssemblyDirectory, filePath.Insert(filePath.Length - 4, "_viesus"))))
                {
                    magickImage.AddProfile(ColorProfile.SRGB);
                    magickImage.Write(output);
                }
            }
        }

        private static string AssemblyDirectory => Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
    }
}
