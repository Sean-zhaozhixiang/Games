using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial.Web.ICore;
using Tutorial.Web.Models;

namespace Tutorial.Web.Core
{
    //实现类
    public class Resbiatiy : IResbiatiy<Students>
    {
        private readonly List<Students> _suduents;
        public Resbiatiy()
        {
            _suduents= new List<Students>()
             {
              new Students{ id=1,username="zzx01",birthDate=new DateTime(1995,6,3) },
              new Students{ id=2,username="zzx02",birthDate=new DateTime(1992,6,3) },
              new Students{ id=3,username="zzx03",birthDate=new DateTime(1993,6,3)},
              new Students{ id=4,username="zzx04",birthDate=new DateTime(1998,6,3) },
              new Students{ id=5,username="zzx05",birthDate=new DateTime(1997,6,3)},
              new Students{ id=6,username="zzx06",birthDate=new DateTime(1999,6,3) },
              new Students{ id=7,username="zzx07",birthDate=new DateTime(1990,6,3) },
               };
        }

        public Students Add(Students newModel)
        {
            var maxId = _suduents.Max(s => s.id);
            newModel.id = maxId + 1;
            _suduents.Add(newModel);
            return newModel;
        }

        public IEnumerable<Students> GetAll()
        {
            return _suduents;
        }

        public Students GetUserById(int id)
        {
            return _suduents.FirstOrDefault(s => s.id == id);
        }
    }
}
