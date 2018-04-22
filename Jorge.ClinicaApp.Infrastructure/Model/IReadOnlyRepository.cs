using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Jorge.ClinicaApp.Infrastructure.Model
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class 
    {
        IEnumerable<TEntity> GetAll(string[] includeProperties = null);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, string[] includeProperties = null);
        
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IList<Func<IOrderedQueryable<TEntity>, IOrderedQueryable<TEntity>>> thenOrderBy = null,
            string[] includeProperties = null,
            int? page = null,
            int? pageSize = null,
            Expression<Func<TEntity, bool>>  adicionalFilter = null);

        TEntity First(Expression<Func<TEntity, bool>> predicate, string[] includeProperties = null);

        long Count(Expression<Func<TEntity, bool>> filter = null, 
            Expression<Func<TEntity, bool>> adicionalFilter = null);
    }
}
