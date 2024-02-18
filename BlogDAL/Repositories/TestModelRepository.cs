using BlogDAL.Models;

namespace BlogDAL.Repositories;

public class TestModelRepository : Repository<TestModel>
{
    public TestModelRepository(ApplicationDbContext db) : base(db)
    {
    }

    public void CreateTest(TestModel model)
    {
        Create(model);
    }
}