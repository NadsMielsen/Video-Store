//Static class for easy prototyping, DI can be added later
public static class RentalService
{
    private static readonly List<Rental> rentals = new List<Rental>(); //Will be DB Table

    public static List<Movie> NewReleaseMovies()
    {
        // This would normally pull from a database
        return new List<Movie>
        {
            new Movie("The Cell", id: new Guid("e0ec7e36-41fd-4230-bd24-c37654e9299c")), //Set GUIDs for test consistency
            new Movie("The Tigger Movie", id: new Guid("a1b2c3d4-e5f6-4789-abcd-ef0123456789")),
            new Movie("The Shawshank Redemption"),
            new Movie("The Godfather"),
            new Movie("The Dark Knight"),
            new Movie("Pulp Fiction"),
            new ChildrensMovie("Toy Story", 12),
        };
    }

    public static bool IsNewRelease(Movie movie)
    {
        return NewReleaseMovies().Select(x => x.id).Contains(movie.id);
    }

    public static Rental RentMovie(Movie movie, int daysRented, Guid customerId)
    {
        if (movie == null)
            throw new ArgumentNullException(nameof(movie), "Movie cannot be null.");
        if (daysRented <= 0)
            throw new ArgumentException(
                "Days rented must be greater than zero.",
                nameof(daysRented)
            );

        var customer = CustomerService.GetCustomerById(customerId);
        if (customer == null)
            throw new InvalidOperationException("Customer not found.");

        var price = PriceCalculationService.CalculateRentalAmount(movie, daysRented);
        var rental = new Rental(movie, daysRented, price, customer);
        rentals.Add(rental);
        customer.frequentRenterPoints++;
        if (IsNewRelease(movie) && daysRented > 1)
            customer.frequentRenterPoints++;
        return rental;
    }

    public static List<Rental> GetRentals()
    {
        return rentals;
    }

    public static List<Rental> GetRentalsByCustomer(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
        return GetRentalsByCustomer(customer.Id);
    }

    public static List<Rental> GetRentalsByCustomer(Guid customerId)
    {
        var customerRentals = rentals
            .Where(r => r.customer != null && r.customer.Id == customerId)
            .ToList();
        return customerRentals;
    }

    public static double GetTotalPriceForCustomer(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
        return GetTotalPriceForCustomer(customer.Id);
    }

    public static double GetTotalPriceForCustomer(Guid customerId)
    {
        var customerRentals = GetRentalsByCustomer(customerId);
        return customerRentals.Sum(r => r.TotalPrice);
    }
}
