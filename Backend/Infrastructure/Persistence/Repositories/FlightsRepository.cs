using Domain.Core;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class FlightsRepository : Repository<Flight, Guid>, IFlightsRepository
{
    public FlightsRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Flight>> GetFlights(Guid source, Guid destId)
    {
        var flights = await _dbContext.Flights
            .Include(f => f.DestinationLocation)
            .Include(f => f.SourceLocation)
            .Where(f => f.DestinationLocationId == destId && f.SourceLocationId == source)
            .ToListAsync();

        return flights;
    }
}