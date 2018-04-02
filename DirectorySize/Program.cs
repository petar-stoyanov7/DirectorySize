using System;
using System.IO;
using System.Collections.Generic;

namespace DirectorySize
{
    class Program
    {
        private static void DisplayWarning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            string currentPath = Directory.GetCurrentDirectory();            

            //whether to sort by size or by file count
            bool size = true;
            bool desc = true;
            bool unsort = false;

            string hi = "This is a simple application inspired by UNIX's command 'du' - it allows you to see directory sizes.";
            string help = @"Syntax: DS [drive][path] [/S][/s][/u][/?]
    [drive][path]   Drive in path to check 
    /s              Sort by size (ascending)
    /S              Sort by size (descending)
    /c              Sort by file count (ascending)
    /C              Sort by file count (descending)
    /u              Ungroupped - Files and directories will be mixed
    /?              Display this info
    By default the program will display results groupped, sorted descending
";
            //Figuring out arguments
            if (args.Length == 0)
            {
                Console.WriteLine(hi+"\n"+help);
            }

            if (args.Length != 0 && Directory.Exists(args[0]))
            {
                currentPath = args[0];
            }

            int sanityCheck = 0;
            int duplicateCheck = 0;

            foreach (var arg in args)
            {
                if (arg == "/s" || arg == "/S" || arg == "/c" || arg == "/C")
                {
                    duplicateCheck++;
                }
                switch (arg)
                {
                    case "/s":
                        size = true;
                        desc = false;
                        break;
                    case "/S":
                        size = true;
                        desc = true;
                        break;
                    case "/c":
                        size = false;
                        desc = false;
                        break;
                    case "/C":
                        size = false;
                        desc = true;
                        break;
                    case "/u":
                        unsort = true;
                        break;
                    case "/?":
                        Console.WriteLine(hi + "\n" + help);
                        break;
                    default:
                        sanityCheck++;
                        break;
                }
            }

            if (args.Length == sanityCheck && args.Length != 0 && !Directory.Exists(args[0]))
            {
                DisplayWarning("Invalid arguments supplied!\n");
                Console.WriteLine(help);
            }

            else if (duplicateCheck > 1)
            {
                DisplayWarning("You can not sort descending and ascending at the same time!\n" +
                    "You can't sort by size and by count at the same time!\n");
                Console.WriteLine(help);
            }

            //actual display logic
            List<FileSystem> allItems = FileSystemManage.ListAllItems(currentPath, size, desc, unsort);

            Console.WriteLine("{0,-50}\t{1,-10}\t{2}\t{3}", "Name", "Size", "Count", "Type");
            foreach (var item in allItems)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                if (item.IsDir)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("{0,-50}\t{1,-10}\t{2}\t{3}", item.Name, Calculations.HumanReadable(item.Size), item.Count, "Dir");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0,-50}\t{1,-10}\t{2}\t{3}", item.Name, Calculations.HumanReadable(item.Size), item.Count, "File");
                }
                Console.ResetColor();
            }
        }
    }
}
