using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EMP.LIB.Data;

namespace EMP.LIB.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _context;
        private DbSet<T> table = null;

        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> Get()
        {
            return table.ToList();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> where)
        {
            return table.Where(where).ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
