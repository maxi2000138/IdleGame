using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Extensions
{
  public static class CollectionExtension
  {
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
    {
      if (enumerable == null)
        return true;

      if (enumerable is ICollection<T> collection)
        return collection.Count == 0;

      return !enumerable.Any();
    }

    public static void Foreach<T>(this IEnumerable<T> collection, Action<T> action)
    {
      foreach (var element in collection) action.Invoke(element);
    }

    public static void Foreach<T>(this IReadOnlyList<T> collection, Action<T> action)
    {
      for (var i = collection.Count - 1; i >= 0; i--) action.Invoke(collection[i]);
    }

  }
}