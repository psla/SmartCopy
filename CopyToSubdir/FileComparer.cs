using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CopyToSubdir
{
    public static class FileComparer
    {
        // We will be reading 64KB blocks
        const int blockSize = 1024 * 64;

        public static bool AreIdentical(string filePath1, string filePath2)
        {
            // This method reads files in parallel, which may be not the most efficient on spinning drives. On spinning drives, computing hash 
            // for files in order could be better.
            //
            using (var memoryStream1 = new MemoryStream(blockSize))
            using (var memoryStream2 = new MemoryStream(blockSize))
            using (var file1Stream = File.OpenRead(filePath1))
            using (var file2Stream = File.OpenRead(filePath2))
            {
                if (file1Stream.Length != file2Stream.Length)
                {
                    return false;
                }

                while (file1Stream.Position != file1Stream.Length)
                {
                    memoryStream1.Seek(0, SeekOrigin.Begin);
                    memoryStream2.Seek(0, SeekOrigin.Begin);
                    file1Stream.CopyTo(memoryStream1, blockSize);
                    file2Stream.CopyTo(memoryStream2, blockSize);

                    if (memoryStream1.GetBuffer().AsSpan().SequenceCompareTo(memoryStream2.GetBuffer()) != 0)  {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
