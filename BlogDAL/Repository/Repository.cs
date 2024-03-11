using Microsoft.EntityFrameworkCore;

namespace BlogDAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _db;

        public DbSet<T> Set { get; private set; }

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            var set = _db.Set<T>();
            set.Load();
            Set = set;
        }

        public IEnumerable<T> GetAll()
        {
            return Set;
        }

        public T Get(string id)
        {
            return Set.Find(id)!;
        }

        public void Create(T item)
        {
            Set.Add(item);
            _db.SaveChanges();
        }

        public void Update(T item)
        {
            Set.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(T item)
        {
            Set.Remove(item);
            _db.SaveChanges();
        }
    }
}
