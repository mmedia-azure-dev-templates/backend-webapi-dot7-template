using System;
using System.IO;

namespace Boilerplate.Infrastructure.Configuration;
public class Utils
{
    public static string GetRawSql(string sqlFileName)
    {
        var baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts");
        var path = Path.Combine(baseDirectory, sqlFileName);
        return File.ReadAllText(path);
    }

}