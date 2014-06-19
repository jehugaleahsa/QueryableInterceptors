using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryableInterceptors
{
    internal class InterceptingQuery<TElement> : IQueryable<TElement>
    {
        private readonly static MethodInfo includeMethodInfo = getIncludeMethodInfo();

        private readonly IQueryable queryable;
        private readonly InterceptingQueryProvider provider;

        public InterceptingQuery(IQueryable queryable, InterceptingQueryProvider provider)
        {
            this.queryable = queryable;
            this.provider = provider;
        }

        private static MethodInfo getIncludeMethodInfo()
        {
            Type type = Type.GetType("System.Data.Entity.DbExtensions, EntityFramework", false);
            if (type == null)
            {
                return null;
            }
            MethodInfo include = type.GetMethod("Include", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(String) }, null);
            return include;
        }

        public IQueryable<TElement> Include(string path)
        {
            if (includeMethodInfo == null)
            {
                return this;
            }
            IQueryable result = (IQueryable)includeMethodInfo.Invoke(null, new object[] { path });
            return new InterceptingQuery<TElement>(result, provider);
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            Expression expression = queryable.Expression;
            return provider.ExecuteQuery<TElement>(expression);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType
        {
            get { return typeof(TElement); }
        }

        public Expression Expression
        {
            get { return queryable.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return provider; }
        }
    }
}
