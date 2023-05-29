﻿using Domain.Entities;

namespace Domain.Core;

public interface IUnitOfWork
{
    IUsersRepository Users { get; }
    IReadOnlyRepository<Location, Guid> Location { get; }

    public Task<int> SaveChangesAsync();

    public int SaveChanges();
}