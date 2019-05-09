using System;
using System.IO;

namespace FolderTree
{
    class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
                ShowHelp();
            else
            {
                ArgumentHandler handler = ArgumentHandler.GetHandlerInstance(args);
                handler.ParseArgs();
                RunWithArgs(handler);
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine(
@"Folder structure serializer parameters:
   ·For serialize directory structure:
       SerializeFolder.exe /s /path=<path> /type=<xml|bin> /file=<filename.bin|xml>
   ·For open serialized structure and show it:
       SerializeFolder.exe /o /type=<xml|bin> /file=<filename.bin|xml>");
        }

        private static void RunWithArgs(ArgumentHandler handler)
        {
            switch (handler.Mode)
            {
                case "s":
                    {
                        Folder folder;

                        if (Directory.Exists(handler.FolderPath))
                            folder = new Folder(handler.FolderPath);
                        else
                            throw new Exception("Directory not exists!");

                        ISave saver = new SaveLoad(handler.Type, handler.FilePath);
                        saver.Serialize(folder);
                        Console.WriteLine("Successfully serialized");
                        break;
                    }
                case "o":
                    {
                        if (!File.Exists(handler.FilePath))
                            throw new Exception("File not exists!");

                        ILoad loader = new SaveLoad(handler.Type, handler.FilePath);
                        loader.Deserialize().Show();

                        break;
                    }
                default:
                    throw new Exception("Incorrect start arguments!");
            }
        }
    }
}
