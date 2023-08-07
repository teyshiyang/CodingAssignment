// See https://aka.ms/new-console-template for more information

using System.Collections.Specialized;
using System.IO.Abstractions;
using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentApp;

public class Program
{
    static int ExitCode = 0;
    public static int Main()
    {
        try
        {
            do
            {
                Console.WriteLine("\n---------------------------------------\n");
                Console.WriteLine("Coding Assignment!");
                Console.WriteLine("\n---------------------------------------\n");
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\t1 - Display");
                Console.WriteLine("\t2 - Search");
                Console.WriteLine("\t3 - Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        Display();
                        break;
                    case "2":
                        Search();
                        break;
                    case "3":
                        return ExitCode;
                    default:
                        return Main();
                }
            } while (true);
        }
        catch(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            ExitCode = -1;
            Console.WriteLine(ex);
            return ExitCode;
        }
    }

    static void Display(string? KeyToSearch = null, string? filePath = null)
    {
        var fileUtility = new FileUtility(new FileSystem());
        var dataList = Enumerable.Empty<Data>();
        string filePath1;
        if (filePath == null)
        {
            Console.WriteLine("Enter the name of the file to display its content:");
            var item = Console.ReadLine()!;
            filePath1 = Path.GetFullPath(@"..\..\..\", Directory.GetCurrentDirectory()) + @"data\" + item;
        }
        else
        {
            filePath1 = filePath;
        }

        if (FileUtility.IsSupportedFile(fileUtility.GetExtension(filePath1)))
        {
            dataList = new ContentParser().Parse(fileUtility.GetContent(filePath1), KeyToSearch);
        }
        else
        {
            Console.WriteLine("Please enter valid file extension type of : .csv, .json, .xml");
            Console.WriteLine("Press anything and enter to try again");
            var temp = Console.ReadLine()!;
            return;
        }

        if (dataList.Count() > 0 && filePath == null)
        {
            Console.WriteLine("Data:");
            foreach (var data in dataList)
            {
                Console.WriteLine($"Key:{data.Key} Value:{data.Value}");
            }
        }
        else if (dataList.Count() > 0 && filePath != null)
        {
            foreach (var data in dataList)
            {
                Console.WriteLine($"Key:{data.Key} Value:{data.Value} FileName:{filePath!.Split(@"CodingAssignmentApp\").LastOrDefault()}");
            }
        }
    }

    static void Search()
    {
        Console.WriteLine("Enter the key to search.");
        var keyToSearch = Console.ReadLine()!;
        string rootDataPath = Path.GetFullPath(@"..\..\..\", Directory.GetCurrentDirectory()) + @"data\";
        List<string> allFiles = PopulateAllItemsInDirectory(rootDataPath, new List<string>());
        Console.WriteLine("\n------------------Search results---------------------\n");
        //foreach (var file in allFiles)
        //{
        //    Display(keyToSearch, file);
        //}
        Parallel.ForEach(allFiles, file =>
        {
            Display(keyToSearch, file);
        });
        Console.WriteLine("\n------------------End of search---------------------\n");
    }

    static List<string> PopulateAllItemsInDirectory(string dataPath, List<string> result)
    {
        var fileUtility = new FileUtility(new FileSystem());
        foreach (var item in Directory.GetFileSystemEntries(dataPath))
        {
            FileAttributes attribute = File.GetAttributes(item);
            if (FileUtility.IsSupportedFile(fileUtility.GetExtension(item)))
            {
                result.Add(item.Trim());
            }
            else
            {
                PopulateAllItemsInDirectory(item, result);
            }
        }
        return result;
    }
}