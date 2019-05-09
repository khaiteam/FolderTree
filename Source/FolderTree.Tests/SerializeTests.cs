using System;
using System.IO;
using NUnit.Framework;


namespace FolderTree.Tests
{
    [SetUpFixture]
    public class SerializedStructure
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Folder");
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Folder\NestedFolder");
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Folder\1.txt", "12");
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Folder\NestedFolder\a.txt", "asdda");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Folder", true);
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\test.dat");
        }


    }

    [TestFixture]
    public class SerializeTests
    {
        [TestCase("xml")]
        [TestCase("bin")]
        public void TestSerialize(string type)
        {
            Folder expected = new Folder(AppDomain.CurrentDomain.BaseDirectory + @"Folder");
            ISave saver = new SaveLoad(type, AppDomain.CurrentDomain.BaseDirectory + @"test.dat");
            saver.Serialize(expected);
            ILoad loader = new SaveLoad(type, AppDomain.CurrentDomain.BaseDirectory + @"test.dat");
            Folder current = loader.Deserialize();

            Assert.AreEqual(expected, current);
        }
    }
}
