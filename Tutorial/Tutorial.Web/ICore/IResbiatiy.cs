using System.Collections.Generic;
using Tutorial.Web.Models;

namespace Tutorial.Web.ICore
{
    //接口类
    public interface IResbiatiy<T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetUserById(int id);
        T Add(T model);
    }
}
