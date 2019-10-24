using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Note.File.FileUtils;

//USE THIS NAMESPACE FOR TESTING METHODS
namespace NoteUnitTests
{
    [TestClass]
    public class FileUtilsTest
    {
        [TestMethod]
        public void GetRootPathTest()
        {
            Assert.AreEqual(System.IO.Path.GetPathRoot(System.Environment.SystemDirectory), GetRootPath());
        }
    }
}
