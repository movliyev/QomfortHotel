
using System.Linq.Expressions;

namespace Hotel_Managment.Application.Abstractions.Repositories.Generic
{
    public interface IRepository<T> where T : class
    {
        List<T>GetListByFilter(Expression<Func<T, bool>> filter);
        List<T> GetAll( );

        //Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        //void SoftDelete(T entity);
        //void ReverseDelete(T entity);
        Task SaveChangesAsync();

      
    }
}
