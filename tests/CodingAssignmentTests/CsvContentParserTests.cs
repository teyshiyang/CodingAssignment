using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentTests;

public class ContentParserTests
{
    private ContentParser _sut = null!;

    [SetUp]
    public void Setup()
    {
        _sut = new ContentParser();
    }

    [Test]
    public void Parse_ReturnWholeDataCsv()
    {
        List<string> content = new List<String>() { "a,b" + "\n" + "c,d" + "\n", ".csv" };
        var dataList = _sut.Parse(content);

        CollectionAssert.AreEqual(new List<Data>
        {
            new ("a", "b"),
            new ("c", "d")
        }, dataList);
    }

    [Test]
    public void Parse_ReturnSearchedDataCsv()
    {
        List<string> content = new List<String>() { "aaaaA,bbbbb" + "\n" + "Ccccc,dddddd" + "\n", ".csv" };
        var dataList = _sut.Parse(content, "AAAAA");

        CollectionAssert.AreEqual(new List<Data>
        {
            new ("aaaaA", "bbbbb")
        }, dataList);
    }

    [Test]
    public void Parse_ReturnWholeDataJson()
    {
        List<string> content = new List<String>() { "a,b" + "\n" + "c,d" + "\n", ".csv" };
        var dataList = _sut.Parse(content);

        CollectionAssert.AreEqual(new List<Data>
        {
            new ("a", "b"),
            new ("c", "d")
        }, dataList);
    }

    [Test]
    public void Parse_ReturnSearchedDataJson()
    {
        List<string> content = new List<String>() { "aaaaA,bbbbb" + "\n" + "Ccccc,dddddd" + "\n", ".csv" };
        var dataList = _sut.Parse(content, "AAAAA");

        CollectionAssert.AreEqual(new List<Data>
        {
            new ("aaaaA", "bbbbb")
        }, dataList);
    }

    [Test]
    public void Parse_ReturnWholeDataXml()
    {
        List<string> content = new List<String>() { "a,b" + "\n" + "c,d" + "\n", ".csv" };
        var dataList = _sut.Parse(content);

        CollectionAssert.AreEqual(new List<Data>
        {
            new ("a", "b"),
            new ("c", "d")
        }, dataList);
    }

    [Test]
    public void Parse_ReturnSearchedDataXml()
    {
        List<string> content = new List<String>() { "aaaaA,bbbbb" + "\n" + "Ccccc,dddddd" + "\n", ".csv" };
        var dataList = _sut.Parse(content, "AAAAA");

        CollectionAssert.AreEqual(new List<Data>
        {
            new ("aaaaA", "bbbbb")
        }, dataList);
    }
}