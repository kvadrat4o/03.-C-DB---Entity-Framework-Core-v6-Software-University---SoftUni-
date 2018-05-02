using System;
using System.Linq;
using System.Collections.Generic;

class Book
{
    private string title;

    private string author;

    private decimal price;

    public Book()
    {

    }

    public Book(string author, string title, decimal price)
    {
        this.Title = title;
        this.Author = author;
        this.Price = price;
    }

    public string Title
    {
        get
        {
            return this.title;
        }

        set
        {
            if (value.Length < 3)
            {
                throw new ArgumentException("Title not valid!");
            }
            else
            {
                this.title = value;
            }
        }
    }

    public string Author 
    {
        get
        {
            return this.author;
        }

        set
        {
            if (value.Substring(value.IndexOf(" ") + 1).ToCharArray().Any(c => c >= '0' && c <= '9'))
            {
                throw new ArgumentException("Author not valid!");
            }
            else
            {
                this.author = value;
            }
        }
    }

    public decimal Price
    {
        get
        {
            return this.price;
        }

        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Price not valid!");
            }
            else
            {
                this.price = value;
            }
        }
    }

    public override string ToString()
    {
        return $"Type: {this.GetType()}\nTitle: {this.Title}\nAuthor: {this.Author}\nPrice: {this.Price:f2}";
    }
}
