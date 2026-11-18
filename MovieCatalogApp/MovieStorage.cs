using System.Configuration;
using Microsoft.Data.SqlClient;
using MovieCatalogApp;

namespace MovieCatalogApp;

public class MovieStorage
{
    private static SqlConnection GetConnection() =>
        new SqlConnection(ConfigurationManager.ConnectionStrings["MovieCatalog"].ConnectionString);

    private static Movie MapRowToMovie(SqlDataReader r) => new()
    {
        Id = r.GetInt32(0),
        Title = r.GetString(1),
        ReleaseYear = r.GetInt32(2),
        DurationMinutes = r.GetInt32(3),
        Rating = r.GetDouble(4),
        Comment = r.IsDBNull(5) ? null : r.GetString(5)
    };

    public List<Movie> GetAll()
    {
        var list = new List<Movie>();
        using var conn = GetConnection();
        conn.Open();
        using var cmd = new SqlCommand("SELECT Id, Title, ReleaseYear, DurationMinutes, Rating, Comment FROM Movies ORDER BY Id", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) list.Add(MapRowToMovie(reader));
        return list;
    }

    public Movie? GetById(int id)
    {
        using var conn = GetConnection();
        conn.Open();
        using var cmd = new SqlCommand("SELECT Id, Title, ReleaseYear, DurationMinutes, Rating, Comment FROM Movies WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapRowToMovie(reader) : null;
    }

    public int Add(Movie m)
    {
        using var conn = GetConnection();
        conn.Open();
        using var cmd = new SqlCommand(
            "INSERT INTO Movies (Title, ReleaseYear, DurationMinutes, Rating, Comment) " +
            "VALUES (@t, @y, @d, @r, @c)", conn);
        cmd.Parameters.AddWithValue("@t", m.Title);
        cmd.Parameters.AddWithValue("@y", m.ReleaseYear);
        cmd.Parameters.AddWithValue("@d", m.DurationMinutes);
        cmd.Parameters.AddWithValue("@r", m.Rating);
        cmd.Parameters.AddWithValue("@c", (object?)m.Comment ?? DBNull.Value);
        return cmd.ExecuteNonQuery();
    }

    public int UpdateRating(int id, double rating)
    {
        if (rating < 0 || rating > 10) throw new ArgumentOutOfRangeException(nameof(rating), "Рейтинг должен быть от 0 до 10");
        using var conn = GetConnection();
        conn.Open();
        using var cmd = new SqlCommand("UPDATE Movies SET Rating = @r WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@r", rating);
        return cmd.ExecuteNonQuery();
    }
}