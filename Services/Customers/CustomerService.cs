//Static class for easy prototyping, DI can be added later
public static class CustomerService
{
    private static readonly List<Customer> customers = new List<Customer>(); //Will be DB Table

    public static Customer? GetCustomerById(Guid customerId)
    {
        return customers.FirstOrDefault(c => c.Id == customerId);
    }

    public static Customer CreateCustomer(string name)
    {
        var customer = new Customer(name);
        customers.Add(customer);
        return customer;
    }

    public static List<Customer> GetAllCustomers()
    {
        return customers;
    }

    public static string GetCustomerStatement(Guid customerId)
    {
        var customer = GetCustomerById(customerId);

        if (customer == null)
            return "Customer not found.";

        String result = "Rental Record for " + customer.Name + "\n";

        var rentals = RentalService.GetRentalsByCustomer(customer);

        for (int i = 0; i < rentals.Count; i++)
        {
            result += "\t" + rentals[i].Movie.Title + "\t" + rentals[i].TotalPrice + "\n";
        }

        result += "You owed " + RentalService.GetTotalPriceForCustomer(customer) + "\n";
        result += "You earned " + customer.frequentRenterPoints + " frequent renter points\n";

        return result;
    }
}
