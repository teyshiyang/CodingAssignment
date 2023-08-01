// See https://aka.ms/new-console-template for more information

using System.Collections.Specialized;
using System.IO.Abstractions;
using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentApp;

public class Program
{
    public static void Main()
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
                    return;
                default:
                    return;
            }
        } while (true);
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
            string path = Directory.GetCurrentDirectory();
            filePath1 = Path.GetFullPath(@"..\..\..\", path) + @"data\" + item;
        }
        else
        {
            filePath1 = filePath;
        }

        if (fileUtility.GetExtension(filePath1) == ".csv" || fileUtility.GetExtension(filePath1) == ".xml" || fileUtility.GetExtension(filePath1) == ".json")
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
        string path = Directory.GetCurrentDirectory();
        string rootDataPath = Path.GetFullPath(@"..\..\..\", path) + @"data\";
        List<string> allFiles = PopulateAllItemsInDirectory(rootDataPath, new List<string>());
        Console.WriteLine("\n------------------Search results---------------------\n");
        foreach (var file in allFiles)
        {
            Display(keyToSearch, file);
        }
        Console.WriteLine("\n------------------End of search---------------------\n");
    }

    static List<string> PopulateAllItemsInDirectory(string dataPath, List<string> result)
    {
        var fileUtility = new FileUtility(new FileSystem());
        foreach (var item in Directory.GetFileSystemEntries(dataPath))
        {
            FileAttributes attribute = File.GetAttributes(item);
            if (fileUtility.GetExtension(item) == ".csv" || fileUtility.GetExtension(item) == ".xml" || fileUtility.GetExtension(item) == ".json")
            {
                result.Add(item.Trim());
            }
            else
            {
                var inner_result = PopulateAllItemsInDirectory(item, result);
            }
        }
        return result;
    }
}