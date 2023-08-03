using CodingAssignmentLib.Abstractions;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Xml.Serialization;

namespace CodingAssignmentLib;

public class ContentParser : IContentParser
{
    public IEnumerable<Data> Parse(List<string> content, string? KeyToSearch = null)
    {
        if (content[1].Contains("csv"))
        {
            if (KeyToSearch == null)
            {
                return content[0].Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(line =>
                {
                    var items = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    return new Data(items[0], items[1].Replace("\r", ""));
                });
            }
            else
            {
                return content[0].Split("\n", StringSplitOptions.RemoveEmptyEntries)
                    .Where(line => line.Split(',', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()!.ToLower() == KeyToSearch.ToLower())
                    .Select(line =>
                    {
                        var items = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        return new Data(items[0], items[1].Replace("\r",""));
                    });
            }
        }
        else if (content[1].Contains("json"))
        {
            var data = JsonSerializer.Deserialize<List<Data>>(content[0])!;
            if (KeyToSearch == null)
            {
                return data;
            }
            else
            {
                return data.Where(x => x.Key.ToLower() == KeyToSearch.ToLower());
            }
        }
        else if (content[1].Contains("xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DataList));
            using (var reader = new StringReader(content[0]))
            {
                DataList data = (DataList)serializer.Deserialize(reader)!;
                if (KeyToSearch == null)
                {
                    return data.Select(line => { return new Data(line.Key, line.Value); });
                }
                else
                {
                    return data.Where(x => x.Key.ToLower() == KeyToSearch.ToLower()).Select(line => { return new Data(line.Key, line.Value); });
                }
            }
        }
        else
        {
            return new List<Data>();
        }
    }
}