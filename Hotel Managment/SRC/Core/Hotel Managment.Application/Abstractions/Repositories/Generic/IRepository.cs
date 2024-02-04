

using Hotel_Managment.Domain.Entities;
using System.Linq.Expressions;

namespace Hotel_Managment.Application.Abstractions.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity,new()
    {
        List<T> GetAll( );
       
        Task<T> GetByIdAsync(int id);
       
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
        void ReverseDelete(T entity);
        Task SaveChangesAsync();

      
    }
}
