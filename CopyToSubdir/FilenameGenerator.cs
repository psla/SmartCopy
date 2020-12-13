using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace CopyToSubdir
{
    public static class FilenameGenerator
    {
        /// <summary>
        /// Given the filename "Foo.jpg", and a directory, it finds all files with the pattern "Foo.jpg" and "Foo-integer.jpg".
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetListOfCopies(string directoryPath, string baseFilename)
        {
            if (File.Exists(Path.Combine(directoryPath, baseFilename)))
            {
                yield return Path.Combine(directoryPath, baseFilename);
            }

            int indexOfDot = baseFilename.LastIndexOf(".");
            if (indexOfDot == -1)
            {
                // We require extension, although technically it shouldn't be needed.
                throw new ArgumentException("Extension is required");
            }


            string filenameNoExtension = baseFilename.Substring(0, indexOfDot);
            string extension = baseFilename.Substring(indexOfDot + 1);

            foreach(var file in Directory.EnumerateFiles(directoryPath, string.Format(CultureInfo.InvariantCulture, "{0}-*.{1}", filenameNoExtension, extension)))
            {
                yield return file;
            }
        }

        /// <summary>
        /// Returns a first available name for the new version of the file.
        /// </summary>
        public static string GetFirstAvailableName(string directoryPath, string baseFilename)
        {
            if (!File.Exists(Path.Combine(directoryPath, baseFilename)))
            {
                return Path.Combine(directoryPath, baseFilename);
            }

            int indexOfDot = baseFilename.LastIndexOf(".");
            if (indexOfDot == -1)
            {
                // We require extension, although technically it shouldn't be needed.
                throw new ArgumentException("Extension is required");
            }

            string filenameNoExtension = baseFilename.Substring(0, indexOfDot);
            string extension = baseFilename.Substring(indexOfDot + 1);

            int startingIndex = 2;

            while (File.Exists(Path.Combine(directoryPath, CombineFilename(filenameNoExtension, startingIndex, extension))))
            {
                startingIndex++;
            }

            return Path.Combine(directoryPath, CombineFilename(filenameNoExtension, startingIndex, extension));
        }

        private static string CombineFilename(string prefix, int index, string extension)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}-{1}.{2}", prefix, index, extension);
        }
    }
}
