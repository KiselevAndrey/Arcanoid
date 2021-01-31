using System;
using System.Collections.Generic;

public static class Helper
{
    static Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    /// <summary>
    /// Определяет элемент от любого числа без IndexOutRange
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static T Ind<T>(this List<T> list, int index)
    {
        index = Math.Abs(index);
        return list[index % list.Count];
    }
}
