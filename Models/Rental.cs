public class Rental
{
    private Movie movie;
    private int daysRented;
    public Customer? customer;
    private double totalPrice;

    public Rental(Movie movie, int daysRented, double totalPrice, Customer? customer = null)
    {
        this.movie = movie;
        this.daysRented = daysRented;
        this.customer = customer;
        this.totalPrice = totalPrice;
    }

    public int DaysRented
    {
        get { return daysRented; }
    }

    public Movie Movie
    {
        get { return movie; }
    }

    public double TotalPrice
    {
        get { return totalPrice; }
    }
}
