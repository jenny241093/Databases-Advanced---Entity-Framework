using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InvalidSongLengthException : InvalidSongException
{
    private const String DeffaultMessage = "Invalid song length.";
    public InvalidSongLengthException() : base(DeffaultMessage)
    {
    }

    public InvalidSongLengthException(string message) : base(message)
    {
    }
}

