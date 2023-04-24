using System.Linq.Expressions;
using Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }

    public void MarkEntryAsModified<TEntity>(TEntity entity,
        Expression<Func<TEntity, object>> propertyExpression,
        bool isModified) where TEntity : class
    {
        _dbContext.Entry(entity).Property(propertyExpression).IsModified = isModified;
    }

    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}