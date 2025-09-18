using System.Collections.ObjectModel;

namespace Hermes.Application.Utils;

public static class ListOptionCollectionUtils
{
    public static void RemoveAllAfter<T>(this ObservableCollection<T> collection, int index)
    {
        var timesToRemove = collection.Count - index;
        for (var i = 0; i < timesToRemove; i++)
        {
            collection.RemoveAt(index);
        }
    }
}