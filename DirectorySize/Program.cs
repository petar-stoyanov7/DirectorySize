using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorySize
{
    class Program
    {        
        static void Main(string[] args)
        {
            string currentPath = Directory.GetCurrentDirectory();            
            
            bool desc = true;
            bool unsort = false;

            string hi = "This is a simple application inspired by UNIX's command 'du' - it allows you to see directory sizes.";
            string help = @"Syntax: DS [drive][path] [/S][/s][/u][/?]
    [drive][path]   Drive in path to check 
    /s              Sort by size (ascending)
    /S              Sort by size (descending)
    /u              Ungroupped - Files and directories will be mixed
    /?              Display this info
    By default the program will display results groupped, sorted descending
";
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

            if (args.Length == sanityCheck && args.Length != 0 && !Directory.Exists(args[0]))
            {
                Console.WriteLine("Invalid arguments supplied!\n");
                Console.WriteLine(help);
            }

            if (duplicateCheck > 1)
            {
                Console.WriteLine("You need to specify either ascending [/s] or descending [/S], not both\n");
                Console.WriteLine(help);
            }

            else
            {
                foreach (var arg in args)
                {
                    if (arg == "/s" || arg == "/S")
                    {
                        duplicateCheck++;
                    }
                    switch (arg)
                    {
                        case "/s":
                            desc = false;
                            break;
                        case "/S":
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
            }
            List<FileSystem> allItems = FileSystemManage.ListAllItems(currentPath, desc, unsort);
            
            Console.WriteLine("{0,-50}\t{1,-10}\t{2}", "Name", "Size", "Type");
            foreach (var item in allItems)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                if (item.IsDir)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("{0,-50}\t{1,-10}\t{2}", item.Name, Calculations.HumanReadable(item.Size), "D");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0,-50}\t{1,-10}\t{2}", item.Name, Calculations.HumanReadable(item.Size),"F");
                }
                Console.ResetColor();
            }
        }
    }
}
