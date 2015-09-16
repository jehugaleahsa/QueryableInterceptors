using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace QueryableInterceptors.ExceptionWrapping.Test
{
    [TestFixture]
    public class OrderedTest
    {
        IQueryable<object> _items;
        [SetUp]
        public void Setup()
        {
            _items = Enumerable.Empty<object>()
                        .AsQueryable()
                        .WrapExceptions()
                        .OfType<Exception>()
                        .With<Exception>();
        }

        [Test]
        public void OrderByOnInterceptedQueryable()
        {
            Assert.DoesNotThrow(() =>
            {
                var result = _items.OrderBy(s => s.GetHashCode());
            });
        }
    }
}
