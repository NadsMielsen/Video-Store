//Static class for easy prototyping, DI can be added later
public static class PriceCalculationService
{
    //Could be extended to use Strategy Pattern for more complex pricing rules
    public static double CalculateRentalAmount(Movie movie, int daysRented)
    {
        double thisAmount = 0;

        //New release
        if (RentalService.IsNewRelease(movie))
        {
            thisAmount += daysRented * 3;
        }
        //Childrens movie
        else if (movie is ChildrensMovie)
        {
            thisAmount += 1.5;
            if (daysRented > 3)
                thisAmount += (daysRented - 3) * 1.5;
        }
        //Regular movie
        else
        {
            thisAmount += 2;
            if (daysRented > 2)
                thisAmount += (daysRented - 2) * 1.5;
        }

        return thisAmount;
    }
}
