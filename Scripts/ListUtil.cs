using System.Collections.Generic;
using System;

public static class ListUtil
{
    public static IList<T> Shuffle<T>(this IList<T> list)
    {
        var current = new List<T>(list);
        var shuffled = new List<T>();

        var rnd = new Random();
        while (current.Count > 0)
        {
            var index = rnd.Next(0, current.Count);
            shuffled.Add(current[index]);
            current.RemoveAt(index);
        }

        return shuffled;
    }
}