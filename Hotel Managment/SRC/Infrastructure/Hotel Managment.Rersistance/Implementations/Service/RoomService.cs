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
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repo;

        public RoomService(IRoomRepository repo)
        {
            _repo = repo;
        }
        public List<Room> GetAll()
        {
            return _repo.GetAll();
        }

        public Room GetById(int id)
        {
            return _repo.GetByIdAsync(id).Result;
        }

        public void TAdd(Room entity)
        {
            _repo.AddAsync(entity);
        }

        public void TDelete(Room entity)
        {
            _repo.Delete(entity);
        }

        public void TUpdate(Room entity)
        {
            _repo.Update(entity);
        }
    }
}
