using System.Collections.Generic;
using System.Linq;

namespace PopVault.Domain.Entities;

public class Album
{
    public int Id { get; set; }
    public string Title { get; private set; }
    public int Year { get; private set; }
    public string Genre { get; private set; }
    public List<Review> Reviews { get; private set; } = new List<Review>();

    public decimal AverageScore 
    {
        get
        {
            if (Reviews.Count == 0) return 0;
            return Reviews.Average(r => r.Score);
        }
    }

    public int DurationInMinutes { get; private set; }

    public Album(string title, int year, string genre, int durationInMinutes)
    {
        Title = title;
        Year = year;
        Genre = genre;
        DurationInMinutes = durationInMinutes;
    }

    public void AddReview(Review review)
    {
        Reviews.Add(review);
    }
}
