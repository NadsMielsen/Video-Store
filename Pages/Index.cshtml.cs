using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VideoStore.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public string? CustomerStatement { get; set; }

    public void OnGet()
    {
        // Create a test customer (or get existing)
        var customer = CustomerService.CreateCustomer("Test User");

        //New release
        RentalService.RentMovie(
            new Movie("The Cell", id: new Guid("e0ec7e36-41fd-4230-bd24-c37654e9299c")),
            3,
            customer.Id
        );

        //Childrens Movie
        RentalService.RentMovie(new ChildrensMovie("Toy Story", 12), 3, customer.Id);

        //Regular Movies
        RentalService.RentMovie(new Movie("Plan 9 from Outer Space"), 1, customer.Id);
        RentalService.RentMovie(new Movie("8 1/2"), 2, customer.Id);
        RentalService.RentMovie(new Movie("Eraserhead"), 3, customer.Id);

        // Get the statement
        CustomerStatement = CustomerService.GetCustomerStatement(customer.Id);
    }
}
