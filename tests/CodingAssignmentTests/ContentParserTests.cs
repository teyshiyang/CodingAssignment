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
        //Search by all caps
        var dataList = _sut.Parse(content, "AAAAA");

        //Returns matching content regardless of text casing
        CollectionAssert.AreEqual(new List<Data>
        {
            new ("aaaaA", "bbbbb")
        }, dataList);
    }

    [Test]
    public void Parse_ReturnWholeDataJson()
    {
        List<string> content = new List<String>() { @"[{""Key"":""abc123"",""Value"":""123abc""},{""Key"":""cba321"",""Value"":""321cba""}]", "json" };
        var dataList = _sut.Parse(content);
        CollectionAssert.AreEqual(new List<Data>
        {
            new ("abc123", "123abc"),
            new ("cba321", "321cba")
        }, dataList);
    }

    [Test]
    public void Parse_ReturnSearchedDataJson()
    {
        List<string> content = new List<String>() { @"[{""Key"":""abc123"",""Value"":""123abc""},{""Key"":""cba321"",""Value"":""321cba""}]", "json" };
        var dataList = _sut.Parse(content, "AbC123");
        CollectionAssert.AreEqual(new List<Data>
        {
            new ("abc123", "123abc")
        }, dataList);
    }

    [Test]
    public void Parse_ReturnWholeDataXml()
    {
        List<string> content = new List<String>() { "<Datas>\n    <Data>\n        <Key>testing123</Key>\n        <Value>123testing</Value>\n    </Data>\n    <Data>\n        <Key>XmlCase1</Key>\n        <Value>Case1Xml</Value>\n    </Data>\n    </Datas>", ".xml" };
        var dataList = _sut.Parse(content);
        CollectionAssert.AreEqual(new List<Data>
        {
            new ("testing123", "123testing"),
            new ("XmlCase1", "Case1Xml")
        }, dataList);
    }

    [Test]
    public void Parse_ReturnSearchedDataXml()
    {
        List<string> content = new List<String>() { "<Datas>\n    <Data>\n        <Key>testing123</Key>\n        <Value>123testing</Value>\n    </Data>\n    <Data>\n        <Key>XmlCase1</Key>\n        <Value>Case1Xml</Value>\n    </Data>\n    </Datas>", ".xml" };
        var dataList = _sut.Parse(content, "TesTinG123");
        CollectionAssert.AreEqual(new List<Data>
        {
            new ("testing123", "123testing")
        }, dataList);s
    }
}