using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EMP.LIB.Infrastructure
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> where);
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}
