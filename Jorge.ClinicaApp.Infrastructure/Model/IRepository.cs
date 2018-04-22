using System;

namespace Jorge.ClinicaApp.Infrastructure.Model
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Remove(TEntity entity);
        void Remove(Func<TEntity, bool> predicate);
    }
}
