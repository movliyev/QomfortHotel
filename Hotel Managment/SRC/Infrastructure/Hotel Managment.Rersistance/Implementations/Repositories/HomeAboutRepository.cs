using Hotel_Managment.Application.Abstractions.Repositories;
using Hotel_Managment.Domain.Entities;
using Hotel_Managment.Rersistance.DAL;
using Hotel_Managment.Rersistance.Implementations.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Managment.Rersistance.Implementations.Repositories
{
    internal class HomeAboutRepository : Repository<HomeAbout>, IHomeAboutRepository
    {
        public HomeAboutRepository(AppDbContext context) : base(context)
        {

        }
    }
}
