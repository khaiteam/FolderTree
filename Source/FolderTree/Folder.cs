using System;
using System.IO;
using System.Xml.Serialization;

namespace FolderTree
{
    [Serializable]
    public class Folder
    {
        [XmlAttribute] public string FolderName;
        public FileInfoWrapper[] Files;
        public Folder[] Folders;

        public Folder() { }

        public Folder(string path)
        {
            try
            {
                DirectoryInfo currentDirectory = new DirectoryInfo(path);
                FolderName = currentDirectory.Name;
                GetSubFiles(currentDirectory);
                GetSubFolders(currentDirectory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void GetSubFolders(DirectoryInfo info)
        {
            DirectoryInfo[] directoryInfos = info.GetDirectories();

            if (directoryInfos.Length > 0)
            {
                Folders = new Folder[directoryInfos.Length];

                for (int i = 0; i < directoryInfos.Length; i++)
                {
                    Folders[i] = new Folder(directoryInfos[i].FullName);
                }
            }
            else
                Folders = null;
        }

        private void GetSubFiles(DirectoryInfo info)
        {
            FileInfo[] fileInfos = info.GetFiles();
            Files = new FileInfoWrapper[fileInfos.Length];

            for (int i = 0; i < fileInfos.Length; i++)
            {
                Files[i] = new FileInfoWrapper(fileInfos[i].FullName);
            }
        }

        public void Show()
        {
            PrintTree(this, 0);
        }

        private void PrintTree(Folder folder, int tab)
        {
            for (int i = 0; i < tab; i++)
            {
                Console.Write(@"   │");
            }

            Console.WriteLine(folder.FolderName);

            tab++;

            if (folder.Folders != null)
            {
                foreach (var item in folder.Folders)
                {
                    PrintTree(item, tab);
                }
            }

            if (folder.Files != null)
            {
                foreach (var item in folder.Files)
                {
                    for (int i = 0; i < tab; i++)
                    {
                        Console.Write(@"   │");
                    }

                    Console.Write(item.ToString());
                }
            }
        }

        public override bool Equals(object obj)
        {
            Folder other = (Folder)obj;

            if (other == null)
                return false;

            if (FolderName != other.FolderName)
                return false;

            if (Files != null && other.Files != null)
            {
                if (Files.Length != other.Files.Length)
                    return false;

                for (int i = 0; i < Files.Length; i++)
                {
                    if (!Files[i].Equals(other.Files[i]))
                        return false;
                }
            }

            if (Folders != null && other.Folders != null)
            {
                if (Folders.Length != other.Folders.Length)
                    return false;

                for (int i = 0; i < Folders.Length; i++)
                {
                    if (!Folders[i].Equals(other.Folders[i]))
                        return false;
                }
            }

            return true;
        }



        public override int GetHashCode()
        {
            return FolderName.GetHashCode();
        }
    }
}
