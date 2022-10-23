public static class Commands
{
    public static void ShowHistory(List<string> history)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (var current in history)
        {
            Console.WriteLine(current);
        }

        Console.ResetColor();
    }

    public static void Help(string[] allCommands)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (var current in allCommands)
        {
            Console.WriteLine(current);
        }

        Console.ResetColor();
    }

    public static void ShowDirectoryInfo(string path)
    {
        var df = new DirectoryInfo(path);
        Console.WriteLine("{0,10}|{1,30}|{2,20}", "Type", "Name", "Number/Size(b)");
        Console.WriteLine("---------------------------------------------------------------");
        foreach (var currentDirectory in df.GetDirectories())
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0,10}|{1,30}|{2,20}", "Directory",
                    currentDirectory.Name,
                    currentDirectory.GetDirectories().Length + currentDirectory.GetFiles().Length);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Access denied");
            }
        }

        foreach (var currentFile in df.GetFiles())
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("{0,10}|{1,30}|{2,20}", "File", currentFile.Name, currentFile.Length);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Access denied");
            }
        }

        Console.ResetColor();
    }

    public static void Open(string path)
    {
        var sr = new StreamReader(path);
        string currentLine;
        while ((currentLine = sr.ReadLine()) != null)
        {
            Console.WriteLine(currentLine);
        }

        sr.Close();
    }

    public static void Tree(string path)
    {
        var di = new DirectoryInfo(path);
        int intIndent = 0;
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine(di.FullName);
        Print(di, intIndent);

        void Print(DirectoryInfo di, int intIndent)
        {
            var strIndent = "";
            for (int i = 0; i < intIndent; i++)
            {
                strIndent += "    ";
            }

            foreach (var currentDirectory in di.GetDirectories())
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(strIndent + currentDirectory.Name);
                    Print(currentDirectory, intIndent + 1);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Access denied");
                }
            }

            foreach (var currentFile in di.GetFiles())
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine(strIndent + currentFile.Name);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Access denied");
                }
            }

            Console.ResetColor();
        }
    }
}