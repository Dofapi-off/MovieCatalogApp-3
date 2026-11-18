namespace MovieCatalogApp;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int ReleaseYear { get; set; }
    public int DurationMinutes { get; set; }
    public double Rating { get; set; }
    public string? Comment { get; set; }

    public Movie() { }

    public Movie(string title, int releaseYear, int durationMinutes, double rating, string? comment)
    {
        Title = title;
        ReleaseYear = releaseYear;
        DurationMinutes = durationMinutes;
        Rating = rating;
        Comment = comment;
    }

    public override string ToString() =>
        $"[{Id}] {Title} ({ReleaseYear}), {DurationMinutes} мин, рейтинг {Rating:0.0}, комментарий: {(Comment ?? "—")}";
}