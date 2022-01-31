using Godot;
using System.Collections.Generic;
using System.Linq;
using System;

public static class FileUtil
{
    public static IEnumerable<string> GetAllFiles(string path, ICollection<string> fileExtensions = null)
    {
        var files = new List<string>();
        var dir = new Directory();
        dir.Open(path);
        dir.ListDirBegin();

        while (true)
        {
            var file = dir.GetNext();

            if (file == null || file == "")
                break;

            var fileExtensionCondition = fileExtensions == null || HasFileExtension(file, fileExtensions);

            if (!file.BeginsWith(".") && fileExtensionCondition)
                files.Add(file);
        }

        dir.ListDirEnd();
        return files.Select(f => $"{path}/{f}");
    }

    private static bool HasFileExtension(string file, ICollection<string> fileExtensions)
    {
        if (fileExtensions == null || fileExtensions == null || fileExtensions.Count == 0)
            return true;

        return fileExtensions.Contains(file.Split(".").Last(), StringComparer.OrdinalIgnoreCase);
    }

}
