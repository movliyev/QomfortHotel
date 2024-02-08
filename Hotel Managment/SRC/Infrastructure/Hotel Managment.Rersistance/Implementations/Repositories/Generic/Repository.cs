using Hotel_Managment.Application.Abstractions.Repositories.Generic;
using Hotel_Managment.Rersistance.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Hotel_Managment.Rersistance.Implementations.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext context)
        {

            _context = context;
            _dbSet = context.Set<T>();
        }
        public List<T> GetAll()
        {
            IQueryable<T> query = _dbSet;
           
           return query.ToList();

        }
        

        public List<T> GetListByFilter(Expression<Func<T, bool>> filter)
        {
           return _dbSet.Where(filter).ToList(); 
        }    

        //public async Task<T> GetByIdAsync(int id)
        //{
        //   return await  _dbSet.FirstOrDefaultAsync(id);


           
        //}




        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            _context.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public async void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        //public void SoftDelete(T entity)
        //{
        //    entity.IsDeleted = true;

        //}
        //public void ReverseDelete(T entity)
        //{
        //    entity.IsDeleted = false;
        //}


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
