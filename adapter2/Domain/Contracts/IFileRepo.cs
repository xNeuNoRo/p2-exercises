using System.Collections.Generic;

namespace App.Domain.Contracts;

public interface IFileRepo<T>
{
    List<T> ReadData();
    void SaveData(List<T> items);
    void Append(T item);
    T? Find(Func<T, bool> cb);
}
