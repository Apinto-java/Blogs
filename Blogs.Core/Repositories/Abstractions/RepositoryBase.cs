using Blogs.Core.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Core.Repositories.Abstractions
{
    public abstract class RepositoryBase<T, TKey> : IRepository<T, TKey> where T : class 
    {
        private readonly BlogPostsApiContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(BlogPostsApiContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Get(TKey id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public void Insert(T item)
        {
            _dbSet.Add(item);
        }

        public void InsertRange(IEnumerable<T> items)
        {
            _dbSet.AddRange(items);
        }

        public void Update(T item)
        {
            _dbSet.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }
        public void UpdateRange(IEnumerable<T> items)
        {
            _dbSet.AttachRange(items);
            foreach(var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
        }


        public void Delete(TKey id)
        {
            T entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        private void Delete(T item)
        {
            if(_context.Entry(item).State == EntityState.Detached)
            {
                _dbSet.Attach(item);
            }

            _dbSet.Remove(item);
        }

        public bool Exists(TKey id)
        {
            return _dbSet.Find(id) != null;
        }
    }
}
