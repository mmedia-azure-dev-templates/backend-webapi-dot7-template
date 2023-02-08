using System.Collections.Generic;
using System.Text.Json;

namespace Boilerplate.Domain.Entities;

public class DefaultModel
{
    public string Content { get; set; }
    public Dictionary<string, string> Data { get; set; }
    public DefaultModel(string content, object data)
    {
        var TempData = JsonSerializer.Deserialize<Dictionary<string, string>>(JsonSerializer.Serialize(data));
        Content = content;
        Data = TempData;
    }

}
