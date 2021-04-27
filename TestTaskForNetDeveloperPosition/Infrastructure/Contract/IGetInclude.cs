using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskForNetDeveloperPosition.Infrastructure.Contract
{
    public interface IGetInclude<T> where T : class
    {
        IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter);
        IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter, string children);
        IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter, string childrenOne, string childrenTwo);
        IEnumerable<T> GetInclude(string children);
    }
}
