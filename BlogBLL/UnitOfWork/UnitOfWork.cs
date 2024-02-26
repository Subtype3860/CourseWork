using BlogBLL.Repository;
using BlogDAL;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BlogBLL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        private Dictionary<Type, object>? _repositories;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }
        public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class
        {
            _repositories ??= new Dictionary<Type, object>();

            if (hasCustomRepository)
            {
                var customRepo = _db.GetService<IRepository<TEntity>>();
                return customRepo;
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_db);
            }

            return (IRepository<TEntity>)_repositories[type];
           
        }
        public int SaveChanges(bool ensureAutoHistory = false)
        {
            throw new NotImplementedException();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
