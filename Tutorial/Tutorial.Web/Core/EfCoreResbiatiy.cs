using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial.Web.Data;
using Tutorial.Web.ICore;
using Tutorial.Web.Models;

namespace Tutorial.Web.Core
{
    public class EfCoreResbiatiy : IResbiatiy<Students>
    {
        private readonly DataContext _context;
        public EfCoreResbiatiy(DataContext context)
        {
            _context= context;
        }
        public Students Add(Students model)
        {
            _context.Students.Add(model);
            _context.SaveChanges();
            return model;
        }

        public IEnumerable<Students> GetAll()
        {
            return _context.Students.ToList();
        }

        public Students GetUserById(int id)
        {
            return _context.Students.Find(id);
        }
    }
}
