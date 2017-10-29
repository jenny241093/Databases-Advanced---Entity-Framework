﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Book
{
    private string title;
    private string author;
    private decimal price;

    public Book(string author,string title, decimal price)
    {
        this.Author = author;
        this.Title = title;      
        this.Price = price;
    }

    public string Title {
        get { return this.title; }
        set
        {
            if (value.Length < 3)
            {
                throw new ArgumentException("Title not valid!");
            }
            this.title = value;
        }
    }
    public string Author
    {
      
        get { return this.author; }
        set
        {

            string[] names = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (names.Length > 1)
            {
                if (char.IsDigit(names[1][0]))
                {
                    throw new ArgumentException("Author not valid!");
                }
            }

            this.author = value;
        }
    }
    public virtual decimal Price
    {
        get { return this.price; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Price not valid!");
            }
            this.price = value;
        }
    }
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("Type: ").AppendLine(this.GetType().Name)
            .Append("Title: ").AppendLine(this.Title)
            .Append("Author: ").AppendLine(this.Author)
            .Append("Price: ").Append($"{this.Price:f2}")
            .AppendLine();

        return sb.ToString();
    }

}

