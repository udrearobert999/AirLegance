using Domain.Entities;

namespace Domain.Core;

public interface IFlightsRepository : IRepository<Flight, Guid>
{
    public Task<IEnumerable<Flight>> GetFlights(Guid source, Guid destId);
}