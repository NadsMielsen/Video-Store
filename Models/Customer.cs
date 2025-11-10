public class Customer
{
    public Guid Id;
    private string name;
    public int frequentRenterPoints;

    public Customer(string name)
    {
        Id = Guid.NewGuid();
        this.name = name;
    }

    public string Name
    {
        get { return name; }
    }
}
