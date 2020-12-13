using NUnit.Framework;

namespace CopyToSubdir.Test
{
    public class FilenameGeneratorTests
    {
        [Test]
        public void GetsTheSameNameForNonexistingFile()
        {
            Assert.AreEqual("res\\NonExistingFile.txt", FilenameGenerator.GetFirstAvailableName("res", "NonExistingFile.txt")); 
        }

        [Test]
        public void GetsTheIteratedNameForExistingOne()
        {
            Assert.AreEqual("res\\File2-2.txt", FilenameGenerator.GetFirstAvailableName("res", "File2.txt"));
        }

        [Test]
        public void GetsTheIteratedNameIfAnotherOneAlreadyExists()
        {
            Assert.AreEqual("res\\File1-3.txt", FilenameGenerator.GetFirstAvailableName("res", "File1.txt"));
        }


        [Test]
        public void GetAllFilesWithNoGap()
        {
            CollectionAssert.AreEqual(new[] { @"res\File1.txt", @"res\File1-2.txt" }, FilenameGenerator.GetListOfCopies("res", "File1.txt"));
        }
    }
}