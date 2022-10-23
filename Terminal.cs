public static class Terminal
{

    private static string _path = "../../../";

    public static string Path
    {
        get => _path;
        set
        {
            if (new DirectoryInfo(value).Exists || new FileInfo(value).Exists)
            {
                _path = value;
            }
            else
            {
                throw new ArgumentException("Wrong path: " + value);
            }
        }
    }

    private static string _сommand;
    private static string[] _availableCommands = new string[] { "help", "history", "cd", "ls", "nano", "tree" };

    private static List<string> _historyOfCommands = new List<string>();

    public static string Command
    {
        get => _сommand;

        set
        {
            if (_availableCommands.Contains(value))
            {
                _сommand = value;
            }
            else
            {
                throw new ArgumentException("Wrong command: " + value);
            }
        }
    }

    public static void DoCommand(string command)
    {
        Command = command.Split(" ")[0];
        _historyOfCommands.Add(command);
        string new_path;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("---------START----------");
        Console.ResetColor();
        switch (Command)
        {
            case "history":
                Commands.ShowHistory(_historyOfCommands);
                break;
            case "help":
                Commands.Help(_availableCommands);
                break;
            case "ls":
                Commands.ShowDirectoryInfo(Path);
                break;
            case "cd":
                new_path = command.Split(" ")[1];
                if (new_path == "../")
                {
                    Path = new DirectoryInfo(Path).Parent.FullName;
                }
                else if ( new_path[..2] == "C:")
                {
                    Path = new DirectoryInfo(new_path).FullName;
                }
                else
                {
                    Path = Path + "/" + new_path;
                }

                Console.WriteLine("Directory has been changed to " + Path);
                Commands.ShowDirectoryInfo(Path);
                break;
            case "nano":
                new_path = command.Split(" ")[1];
                Commands.Open(Path + '/' + new_path);
                break;
            case "tree":
                Commands.Tree(Path);
                break;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("---------FINISH---------");
        Console.ResetColor();
    }
}