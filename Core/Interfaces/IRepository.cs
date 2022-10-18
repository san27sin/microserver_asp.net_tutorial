﻿using System.Collections.Generic;

namespace Core
{
    //создаем абстракцию (интерфейс)
    public interface IRepository<T>where T : class
    {        
        IList<T> GetAll();
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
