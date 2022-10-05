using Core.Models;
using Core.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TModel> where TModel : BaseModel
    {
        public static IQueryable<TModel> GetQuery(IQueryable<TModel> inputQuery,
            ISpecification<TModel> spec)
        {
            var query = inputQuery;
            
            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
