using System;
using System.IO;

namespace CopyToSubdir
{
    class Program
    {

        private enum OperationType
        {
            Move,
            Copy
        }
        static void Main(string[] args)
        {
            if(args.Length < 3)
            {
                Console.WriteLine("Missing argument");
                Console.WriteLine("");
                Console.WriteLine();
                Console.WriteLine("args[0]: filename to copy");
                Console.WriteLine("args[1]: target directory name (relative to the parent)");
                return;
            }

            OperationType operation = OperationType.Copy;
            if(args.Length == 3)
            {
                operation = args[0] == "copy" ? OperationType.Copy : OperationType.Move;
            }

            string filenameToCopy = args[1];
            if (!File.Exists(filenameToCopy))
            {
                Console.WriteLine($"File {filenameToCopy} does not exist");
                return;
            }

            if(string.IsNullOrEmpty(args[2]))
            {
                Console.WriteLine($"Target directory name is empty");
            }

            string fileDirectory = Path.GetDirectoryName(filenameToCopy);
            string targetDirectory = Path.Combine(fileDirectory, args[1]);

            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            string filename = new FileInfo(filenameToCopy).Name;
            string targetFilename = Path.Combine(targetDirectory, filename);
            if(File.Exists(targetFilename))
            {
                Console.WriteLine($"Skipping {filename} because it already exists in destination {targetFilename}");
                return;
            }

            File.Copy(filenameToCopy, targetFilename);
        }
    }
}
