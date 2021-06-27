using MarketplaceBetter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Data
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        TEntity Get(long id);

        IList<TEntity> GetAll();

        IQueryable<TEntity> GetQuery();

        bool Any(Expression<Func<TEntity, bool>> predicate);

        int Count(Expression<Func<TEntity, bool>> predicate);

        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    }
}
