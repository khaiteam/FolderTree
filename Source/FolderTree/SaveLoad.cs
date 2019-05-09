using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace FolderTree
{
    public class SaveLoad : ISave, ILoad
    {
        public string PathToFile { get; }
        public string Type { get; }


        public SaveLoad(string type, string pathToFile)
        {
            PathToFile = pathToFile;
            Type = type;
        }

        public void Serialize(Folder folder)
        {
            switch (Type)
            {
                case "xml":
                    SerializeXml(folder);
                    break;
                case "bin":
                    SerializeBin(folder);
                    break;
                default:
                    throw new Exception("Incorrect format type");
            }
        }

        public Folder Deserialize() //исправлен баг
        {
            switch (Type)
            {
                case "xml":
                    return DeserializeXml();
                case "bin":
                    return DeserializeBin();
                default:
                    return null;
            }
        }

        private void SerializeXml(Folder folder)
        {
            using (var xmlWriteStream = new FileStream(PathToFile, FileMode.Create, FileAccess.Write))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Folder));
                xmlSerializer.Serialize(xmlWriteStream, folder);
            }
        }

        private Folder DeserializeXml()
        {
            using (var xmlReadStream = new FileStream(PathToFile, FileMode.Open))
            {
                XmlSerializer xmlDeSerializer = new XmlSerializer(typeof(Folder));
                return (Folder)xmlDeSerializer.Deserialize(xmlReadStream);
            }
        }

        private void SerializeBin(Folder folder)
        {
            using (var binWriteStream = new FileStream(PathToFile, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter binSerializer = new BinaryFormatter();
                binSerializer.Serialize(binWriteStream, folder);
            }
        }

        private Folder DeserializeBin()
        {
            using (var binReadStream = new FileStream(PathToFile, FileMode.Open))
            {
                BinaryFormatter binDeserializer = new BinaryFormatter();
                return (Folder)binDeserializer.Deserialize(binReadStream);
            }
        }
    }
}
