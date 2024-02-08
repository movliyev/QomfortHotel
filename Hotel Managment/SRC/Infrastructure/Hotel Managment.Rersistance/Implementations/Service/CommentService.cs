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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repo;

        public CommentService(ICommentRepository repo)
        {
            _repo = repo;
        }
    
        public List<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public void TAdd(Comment entity)
        {
            _repo.AddAsync(entity); 
            _repo.SaveChangesAsync();
        }

        public void TDelete(Comment entity)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Comment entity)
        {
            throw new NotImplementedException();
        }
        public List<Comment>TGetByBlogId(int id)
        {
            return _repo.GetListByFilter(x=>x.BlogId==id);
        }
    }
}
