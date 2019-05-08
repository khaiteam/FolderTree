
namespace FolderTree
{
    public class ArgumentHandler
    {
        private static ArgumentHandler _singleInstance;
        private readonly string[] _args;
        public string Mode { get; private set; }
        public string FolderPath { get; private set; }
        public string FilePath { get; private set; }
        public string Type { get; private set; }

        public static ArgumentHandler GetHandlerInstance(string[] args)
        {
            return _singleInstance ?? (_singleInstance = new ArgumentHandler(args));
        }

        private ArgumentHandler(string[] args)
        {
            _args = args;
        }

        public void ParseArgs()
        {
            foreach (string arg in _args)
            {
                if (arg == "/s")
                    Mode = "s";

                if (arg == "/o")
                    Mode = "o";

                if (arg.StartsWith("/path="))
                    FolderPath = arg.Substring(6);

                if (arg.StartsWith("/type="))
                    Type = arg.Substring(6);

                if (arg.StartsWith("/file="))
                    FilePath = arg.Substring(6);
            }
        }
    }
}
