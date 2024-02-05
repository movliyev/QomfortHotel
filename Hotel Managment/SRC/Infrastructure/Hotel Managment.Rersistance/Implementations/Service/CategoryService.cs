using Hotel_Managment.Application.Abstractions.Repositories;
using Hotel_Managment.Application.Abstractions.Services;
using Hotel_Managment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Managment.Rersistance.Implementations.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public List<Category> GetAll()
        {
          return  _repo.GetAll();       
        }

        public Category GetById(int id)
        {
            return _repo.GetByIdAsync(id).Result;
        }

        public void TAdd(Category entity)
        {
            _repo.AddAsync(entity);
        }

        public void TDelete(Category entity)
        {
           _repo.Delete(entity);   
        }

        public void TUpdate(Category entity)
        {
           _repo.Update(entity);
        }
    }
}
