using CodingAssignmentLib;
using System.IO.Abstractions;

namespace CodingAssignmentTests;

public class FileUtilityTests
{
    public FileUtility _fileUtility = null!;

    [SetUp]
    public void Setup()
    {
        _fileUtility = new FileUtility(new FileSystem());
    }

    [Test]
    public void TestGetFileExtensionCSV()
    {
        var extension = _fileUtility.GetExtension(@"Examplefiletest\verylongpath\data123321.csv");
        CollectionAssert.AreEqual(".csv",extension);
    }

    [Test]
    public void TestGetFileExtensionXML()
    {
        var extension = _fileUtility.GetExtension(@"VeryTestablePath\Examplefiletest.xml");
        CollectionAssert.AreEqual(".xml", extension);
    }

    [Test]
    public void TestGetFileExtensionJson()
    {
        var extension = _fileUtility.GetExtension(@"VeryVeryVeryTestablePath\HelloWorld\Examplefiletest.json");
        CollectionAssert.AreEqual(".json", extension);
    }

    [Test]
    public void TestGetContentCSV()
    {
        string path = Directory.GetCurrentDirectory();
        string filePath1 = Path.GetFullPath(@"..\..\..\", path) + @"testData\testData.csv";
        var content = _fileUtility.GetContent(filePath1);
        CollectionAssert.AreEqual(new List<string>
        {
            "aaaaa,bbbbb\r\nCcCcc,DdddD",".csv"
        },content);
    }

    [Test]
    public void TestGetContentXML()
    {
        string path = Directory.GetCurrentDirectory();
        string filePath1 = Path.GetFullPath(@"..\..\..\", path) + @"testData\testData.xml";
        var content = _fileUtility.GetContent(filePath1);
        CollectionAssert.AreEqual(new List<string>
        {
            "<Datas>\r\n    <Data>\r\n        <Key>testxml1</Key>\r\n        <Value>xmlvalue1</Value>\r\n    </Data>\r\n    <Data>\r\n        <Key>testxml2</Key>\r\n        <Value>xmlvalue2</Value>\r\n    </Data>\r\n</Datas>",".xml"
        }, content);
    }

    [Test]
    public void TestGetContentJson()
    {
        string path = Directory.GetCurrentDirectory();
        string filePath1 = Path.GetFullPath(@"..\..\..\", path) + @"testData\testData.json";
        var content = _fileUtility.GetContent(filePath1);
        CollectionAssert.AreEqual(new List<string>
        {
            @"[{""Key"": ""testjson1"",""Value"": ""valuejson1""},{""Key"": ""testjson2"",""Value"": ""valuejson2""}]","json"
        }, content);
    }
}