using Hotel_Managment.Application.Abstractions.Repositories;
using Hotel_Managment.Domain.Entities;
using Hotel_Managment.Rersistance.DAL;
using Hotel_Managment.Rersistance.Implementations.Repositories.Generic;


namespace Hotel_Managment.Rersistance.Implementations.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context)
        {

        }
    }
}
