using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            //Creat A varialbe To Store Input Our Query In So We Save Our Query Equales inputQuery
            /*
             var typeId = 1;  =>  var query = inputQuery;
            var products = _context.Products
            .Where(x => x.ProductTypeId == typeId).Include(p => p.ProductType).ToListAsync();

            */

            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); // Where(p => p.ProductId == id)     For Example
            }
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;

        }
    }
}