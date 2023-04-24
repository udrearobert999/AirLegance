namespace Domain.Entities;

public class TestEntity
{
    public TestEntity(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public string Name { get; set; }
    public string Address { get; set; }
}