//Factory patter could be utilized here for more complex Movie creation logic
public class Movie
{
    private Guid Id;
    public string Title { get; }

    public Movie(string title, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Title = title;
    }

    public Guid id
    {
        get { return Id; }
    }
}
