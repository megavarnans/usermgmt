using System;
using System.Collections.Generic;
using System.Text;
using RP.UserManagement.Core.Entities;

namespace RP.UserManagement.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
