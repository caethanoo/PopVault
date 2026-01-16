namespace PopVault.Domain.Entities;

public class Review
{
    public string Author { get; private set; }
    public decimal Score { get; private set; }
    public string Comment { get; private set; }

    public Review(string author, decimal score, string comment)
    {
        Author = author;
        Score = score;
        Comment = comment;
    }
}
