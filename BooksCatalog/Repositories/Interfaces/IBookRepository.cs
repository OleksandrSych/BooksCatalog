using System.Collections.Generic;

namespace BooksCatalog.Repositories.Interfaces
{
    public interface IBookRepository<TEntity, U> where TEntity : class
    {
        public IEnumerable<TEntity> GetAll();
        public TEntity Get(U id);
        public int Add(TEntity b);
        public int Add(TEntity[] b);
        public int Update(U id, TEntity b);
        public int Delete(U id);
    }
}
