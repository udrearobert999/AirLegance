namespace Domain.Entities;

public class TestEntity : IEntity<Guid>
{
    public TestEntity(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}