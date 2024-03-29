﻿using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace JornadaMIlhasTestes.Helper
{
    internal class TestDbAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestDbAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }

        TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default(CancellationToken))
        {
            var expectedResultType = typeof(TResult).GetGenericArguments()[0];
            object executionResult = ExecuteWithTaskType(expression, expectedResultType);
            return CreateResultTask<TResult>(expectedResultType, executionResult);
        }
        private object ExecuteWithTaskType(Expression expression, Type expectedResultType)
        {
            return typeof(IQueryProvider)
              .GetMethods()
              .First(method => method.Name == nameof(IQueryProvider.Execute) && method.IsGenericMethod)
              .MakeGenericMethod(expectedResultType)
              .Invoke(this, new object[] { expression });
        }
        private static TResult CreateResultTask<TResult>(Type expectedResultType, object executionResult)
        {
            return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
                          .MakeGenericMethod(expectedResultType)
                          .Invoke(null, new[] { executionResult });
        }
    }
}
