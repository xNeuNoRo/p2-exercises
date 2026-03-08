namespace App.Domain.Interfaces;

public interface ICrudRepo<T> : IReadable<T>, IWritable<T>, IRemovable
    where T : class { }
