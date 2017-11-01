using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InvalidSongNameException : InvalidSongException
{
    private const String DeffaultMessage = "Song name should be between 3 and 30 symbols.";

    public InvalidSongNameException() : base(DeffaultMessage)
    {

    }

    public InvalidSongNameException(string message) : base(message)
    {

    }
}


