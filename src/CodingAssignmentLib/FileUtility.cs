using System.IO.Abstractions;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib;

public class FileUtility : IFileUtility
{
    private readonly IFileSystem _fileSystem;

    public FileUtility(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public string GetExtension(string fileName)
    {
        return _fileSystem.FileInfo.New(fileName).Extension;
    }

    public List<string> GetContent(string fileName)
    {
        try
        {
            return new List<string>() { _fileSystem.File.ReadAllText(fileName), _fileSystem.FileInfo.New(fileName).Extension.Trim('.') };
        }
        catch(Exception ex)
        {
            throw new FileNotFoundException($"File is not found {fileName}");
        }
    }

    public static bool IsSupportedFile(string fileExtension)
    {
        return Enum.IsDefined(typeof(FileUtility.SupportedFiles), fileExtension.Trim('.'));
    }

    public enum SupportedFiles
    {
        csv,
        xml,
        json
    }
}