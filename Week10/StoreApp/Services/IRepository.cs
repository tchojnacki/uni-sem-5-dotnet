using System.Collections.Generic;

namespace StoreApp.Services
{
    public interface IRepository<T>
    {
        IEnumerable<T> All { get; }

        T? this[int id] { get; }

        T Add(T entity);

        T Update(T entity);

        void Delete(int id);
    }
}
