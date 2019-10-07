using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Note;

namespace NoteUnitTests
{
    [TestClass]
    public class StringUtilsTest
    {
        [TestMethod]
        public void IsWellFormedTest()
        {
            var dt = new Dictionary<char, char>
            {
                ['<'] = '>',
                ['('] = ')'
            };

            Assert.AreEqual(true, StringUtils.IsWellFormed("<<((Manu))>>", dt));
            Assert.AreEqual(false, StringUtils.IsWellFormed("<<((Manu)>>", dt));
        }

        [TestMethod]
        public void ContainsDuplicateInnerStringTest()
        {
           
        }
    }
}
