using System;
using System.IO;
using System.Linq;
using System.Threading;

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
            if (args.Length != 3)
            {
                Console.WriteLine("Missing argument");
                Console.WriteLine("");
                Console.WriteLine();
                Console.WriteLine("args[0]: copy | move");
                Console.WriteLine("args[1]: filename to copy");
                Console.WriteLine("args[2]: target directory name (relative to the parent)");
                return;
            }

            OperationType operation = OperationType.Copy;
            if (args.Length == 3)
            {
                operation = args[0] == "copy" ? OperationType.Copy : OperationType.Move;
            }

            string filenameToCopy = args[1];
            if (!File.Exists(filenameToCopy))
            {
                Console.WriteLine($"File {filenameToCopy} does not exist");
                return;
            }

            if (string.IsNullOrEmpty(args[2]))
            {
                Console.WriteLine($"Target directory name is empty");
                return;
            }

            string fileDirectory = Path.GetDirectoryName(filenameToCopy);
            string targetDirectory = Path.Combine(fileDirectory, args[2]);

            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            string filename = new FileInfo(filenameToCopy).Name;


            string firstMatchingName = FilenameGenerator.GetListOfCopies(targetDirectory, filename).FirstOrDefault(file => FileComparer.AreIdentical(filenameToCopy, file));
            if (firstMatchingName != null)
            {
                Console.WriteLine($"Skipping {filename} because it already exists in destination {firstMatchingName}");
                return;
            }

            string targetFilename = FilenameGenerator.GetFirstAvailableName(targetDirectory, filename);

            Action<string, string> action = operation switch
            {
                OperationType.Copy => File.Copy,
                OperationType.Move => File.Move,
                _ => throw new NotImplementedException(),
            };

            ExecuteWithRetry(() => action(filenameToCopy, targetFilename));
        }

        private static void ExecuteWithRetry(Action action)
        {
            for (int i = 0; i < 3; i++)
            {
                try { action(); break; }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
