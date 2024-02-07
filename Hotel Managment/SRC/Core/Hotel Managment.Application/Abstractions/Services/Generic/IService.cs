using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Managment.Application.Abstractions.Services.Generic
{
    public interface IService<T>
    {
        List<T> GetAll();
        //T GetById(int id);
        void TAdd(T entity);
        void TDelete(T entity);
        void TUpdate(T entity); 
    }
}
