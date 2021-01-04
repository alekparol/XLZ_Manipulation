using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XLZ_Manipulation;

namespace XLZ_Manipulation_Tests
{
    [TestClass]
    public class ReadFromXLZ_Tests
    {
        [TestMethod]
        public void ReadFileFromXLZ_Method_Test_1()
        {
            string xlzFilePath = @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\001-20339-AVI0320_DuPont Delrin 300CPE 2pager_A4_v1-digital@3868119430.pdf.idml.xlz";
            string contentXLF = Utilities.ReadFileFromXLZ(xlzFilePath, "xlf");

            Assert.AreEqual("<?xml vers", contentXLF.Substring(0, 10));
        }

        [TestMethod]
        public void ReadFileFromXLZ_Method_Test_2()
        {
            string xlzFilePath = @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\001-20339-AVI0320_DuPont Delrin 300CPE 2pager_A4_v1-digital@3868119430.pdf.idml.xlz";
            string contentXLF = Utilities.ReadFileFromXLZ(xlzFilePath, "skl");

            Assert.AreEqual("<?xml vers", contentXLF.Substring(0, 10));
        }

        [TestMethod]
        public void ReadContentXLF_Method_Test_1()
        {
            string xlzFilePath = @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\001-20339-AVI0320_DuPont Delrin 300CPE 2pager_A4_v1-digital@3868119430.pdf.idml.xlz";
            string contentXLF = Utilities.ReadContentXLF(xlzFilePath);

            Assert.AreEqual("<?xml vers", contentXLF.Substring(0, 10));
        }

        [TestMethod]
        public void ReadSkeletonSKL_Method_Test_2()
        {
            string xlzFilePath = @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\001-20339-AVI0320_DuPont Delrin 300CPE 2pager_A4_v1-digital@3868119430.pdf.idml.xlz";
            string contentXLF = Utilities.ReadSkeletonSKL(xlzFilePath);

            Assert.AreEqual("<?xml vers", contentXLF.Substring(0, 10));
        }
    }
}
