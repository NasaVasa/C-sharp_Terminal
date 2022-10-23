string inputCommand;
Console.Write("Enter command: ");
while ((inputCommand = Console.ReadLine()) != "exit")
{
    try
    {
        Terminal.DoCommand(inputCommand);
    }
    catch (ArgumentException e)
    {
        Console.WriteLine(e.Message);
    }

    Console.Write("Enter command: ");
}

Console.WriteLine("BYE");