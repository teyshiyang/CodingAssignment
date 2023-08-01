namespace CodingAssignmentLib.Abstractions;

public interface IContentParser
{
    IEnumerable<Data> Parse(List<string> content, string KeyToSearch);
}