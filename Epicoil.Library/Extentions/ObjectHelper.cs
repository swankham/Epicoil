using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class ObjectHelper
{
    public static IEnumerable Append(this IEnumerable first, params object[] second)
    {
        return first.OfType<object>().Concat(second);
    }
    public static IEnumerable<T> Append<T>(this IEnumerable<T> first, params T[] second)
    {
        return first.Concat(second);
    }
    public static IEnumerable Prepend(this IEnumerable first, params object[] second)
    {
        return second.Concat(first.OfType<object>());
    }
    public static IEnumerable<T> Prepend<T>(this IEnumerable<T> first, params T[] second)
    {
        return second.Concat(first);
    }

    public static T GetValueWithDefault<T>(this Object obj, T defaultValue)
    {
        T value = default(T);

        if (obj == null || obj is System.DBNull)
        {
            return value;
        }
        else
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }

    public static decimal GetDecimal(this Object obj)
    {
        return obj.GetValueWithDefault<decimal>(0M);
    }

    public static bool GetBoolean(this Object obj)
    {
        return obj.GetValueWithDefault<bool>(false);
    }

    public static string GetString(this Object obj)
    {
        return obj.GetValueWithDefault<string>(string.Empty);
    }

    public static int GetInt(this Object obj)
    {
        return obj.GetValueWithDefault<int>(0);
    }

    public static DateTime GetDate(this Object obj)
    {
        return obj.GetValueWithDefault<DateTime>(DateTime.MinValue);
    }
}