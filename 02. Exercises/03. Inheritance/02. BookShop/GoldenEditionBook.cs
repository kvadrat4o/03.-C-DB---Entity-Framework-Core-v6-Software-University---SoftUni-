using System;


class GoldenEditionBook : Book
{
    public GoldenEditionBook(string title, string author, decimal price) :base(title, author, price)
    {
        this.Price *= 1.3m;
    }

    public override string ToString()
    {
        return $"Type: {base.GetType()}\nTitle: {base.Title}\nAuthor: {base.Author}\nPrice: {base.Price:f2}";
    }
}

