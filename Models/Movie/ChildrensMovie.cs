public class ChildrensMovie : Movie
{
    public int AgeLimit { get; }

    public ChildrensMovie(string title, int ageLimit)
        : base(title)
    {
        AgeLimit = ageLimit;
    }
}
