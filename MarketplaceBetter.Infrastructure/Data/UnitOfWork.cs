using MarketplaceBetter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Infrastructure.Data
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : IDbContext
    {
        private readonly IDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories;
        private bool _disposed;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;

            _repositories = new Dictionary<Type, object>();

            _disposed = false;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            var repository = new Repository<TEntity>(_dbContext);
            _repositories.Add(typeof(TEntity), repository);

            return repository;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
