using System.Xml.Serialization;

namespace CodingAssignmentLib.Abstractions;


[XmlRoot(ElementName = "Datas")]
public class DataList : List<Data>
{
}

public struct Data
{
    public Data(string key, string value)
    {
        Key = key;
        Value = value;
    }
    [XmlElement("Key")]
    public string Key { get; set; }
    [XmlElement("Value")]
    public string Value { get; set; }
}