using BlogDAL;
using Microsoft.EntityFrameworkCore;

namespace BlogBLL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private Microsoft.EntityFrameworkCore.DbContext _db;

        public DbSet<T> Set { get; private set; }

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            var set = _db.Set<T>();
            set.Load();
            Set = set;
        }

        public void Create(T item)
        {
            Set.Add(item);
            _db.SaveChanges();
        }

        public void Delete(T item)
        {
            Set.Remove(item);
            _db.SaveChanges();
        }

        public T Get(string id)
        {
            return null;
        }

        public IEnumerable<T> GetAll()
        {
            return Set.AsQueryable().AsNoTracking();
        }

        public void Update(T item)
        {
            Set.Entry(item).State = EntityState.Modified;
        }
    }
}
