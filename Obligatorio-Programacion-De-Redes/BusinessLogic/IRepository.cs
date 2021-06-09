using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IRepository<T>
    {
        T Add(T objectToAdd);
        void Delete(T objectToDelete);
        T Find(Predicate<T> condition);
        List<T> Get();
        T Update(T oldObject, T updatedObject);
    }
}