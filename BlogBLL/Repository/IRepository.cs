﻿using BlogDAL.Models;

namespace BlogBLL.Repository
{
    public interface IRepository<T> where T : class 
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
