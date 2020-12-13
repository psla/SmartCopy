using NUnit.Framework;

namespace CopyToSubdir.Test
{
    public class Tests
    {
        [Test]
        public void IdenticalFilesAreIdentical()
        {
            Assert.IsTrue(FileComparer.AreIdentical("res/File1.txt", "res/File1.txt"));
        }

        [Test]
        public void DifferentContentSameSizeAreDifferent()
        {
            Assert.IsFalse(FileComparer.AreIdentical("res/File1.txt", "res/File2.txt"));
        }

        [Test]
        public void DifferentSizeFilesAreDifferent()
        {
            Assert.IsFalse(FileComparer.AreIdentical("res/File1.txt", "res/File3.txt"));
        }
    }
}