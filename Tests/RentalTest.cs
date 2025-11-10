using NUnit.Framework;

[TestFixture]
public class RentalTest
{
    Customer customer = new Customer("Fred");

    [SetUp]
    public void Setup()
    {
        //Note: should use Mock for CustomerService in real unit tests - keeping it simple for this example
        //Also easier to mock if CustomerService had DI implemented
        customer = CustomerService.CreateCustomer("Fred");
    }

    [Test]
    public void SingleNewReleaseStatement()
    {
        RentalService.RentMovie(
            new Movie("The Cell", id: new Guid("e0ec7e36-41fd-4230-bd24-c37654e9299c")),
            3,
            customer.Id
        );
        Assert.That(
            "Rental Record for Fred\n\tThe Cell\t9\nYou owed 9\nYou earned 2 frequent renter points\n"
                == CustomerService.GetCustomerStatement(customer.Id)
        );
    }

    [Test]
    public void DualNewReleaseStatement()
    {
        RentalService.RentMovie(
            new Movie("The Cell", id: new Guid("e0ec7e36-41fd-4230-bd24-c37654e9299c")),
            3,
            customer.Id
        );
        RentalService.RentMovie(
            new Movie("The Tigger Movie", id: new Guid("a1b2c3d4-e5f6-4789-abcd-ef0123456789")),
            3,
            customer.Id
        );
        Assert.That(
            "Rental Record for Fred\n\tThe Cell\t9\n\tThe Tigger Movie\t9\nYou owed 18\nYou earned 4 frequent renter points\n"
                == CustomerService.GetCustomerStatement(customer.Id)
        );
    }

    [Test]
    public void SingleChildrensStatement()
    {
        RentalService.RentMovie(new ChildrensMovie("Toy Story", 12), 3, customer.Id);
        Assert.That(
            "Rental Record for Fred\n\tToy Story\t1.5\nYou owed 1.5\nYou earned 1 frequent renter points\n"
                == CustomerService.GetCustomerStatement(customer.Id)
        );
    }

    [Test]
    public void MultipleRegularStatement()
    {
        RentalService.RentMovie(new Movie("Plan 9 from Outer Space"), 1, customer.Id);
        RentalService.RentMovie(new Movie("8 1/2"), 2, customer.Id);
        RentalService.RentMovie(new Movie("Eraserhead"), 3, customer.Id);
        Assert.That(
            "Rental Record for Fred\n\tPlan 9 from Outer Space\t2\n\t8 1/2\t2\n\tEraserhead\t3.5\nYou owed 7.5\nYou earned 3 frequent renter points\n"
                == CustomerService.GetCustomerStatement(customer.Id)
        );
    }
}
