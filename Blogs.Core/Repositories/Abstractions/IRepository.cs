using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Core.Repositories.Abstractions
{
    public interface IRepository<T, TKey> where T : class
    {
        public T Get(TKey id);
        public IEnumerable<T> GetAll();
        public void Insert(T entity);
        public void Update(T entity);
        public void Delete(TKey id);
        public bool Exists(TKey id);
    }
}
