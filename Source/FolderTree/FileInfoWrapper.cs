using System;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace FolderTree
{
    [Serializable]
    public class FileInfoWrapper
    {
        [XmlAttribute]
        public string Name;
        [XmlAttribute]
        public DateTime CreationTime;
        [XmlAttribute]
        public long Size;
        [XmlAttribute]
        public FileAttributes Attributes;
        [XmlAttribute]
        public string FullName;

        private const int WidthName = 40;
        private const int WidthSize = 10;
        private const int WidthTime = 25;

        public FileInfoWrapper() { }

        public FileInfoWrapper(string path)
        {
            FileInfo currentFile = new FileInfo(path);
            Name = currentFile.Name;
            FullName = currentFile.FullName;
            Size = currentFile.Length;
            CreationTime = currentFile.CreationTime;
            Attributes = currentFile.Attributes;
        }

        public override bool Equals(object obj)
        {
            FileInfoWrapper other = (FileInfoWrapper)obj;
            return obj != null && string.Equals(Name, other.Name) && CreationTime.Equals(other.CreationTime) && Size == other.Size && Attributes == other.Attributes && string.Equals(FullName, other.FullName);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return Name + new string(' ', Math.Abs(WidthName - Name.Length)) +
                   Size + " bytes" + new string(' ', Math.Abs(WidthSize - Size.ToString().Length)) +
                   CreationTime + new string(' ', Math.Abs(WidthTime - CreationTime.ToString(CultureInfo.CurrentCulture).Length)) +
                   Attributes + Environment.NewLine;
        }
    }
}
