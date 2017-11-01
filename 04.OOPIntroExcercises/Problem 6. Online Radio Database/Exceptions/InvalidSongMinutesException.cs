using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class InvalidSongMinutesException : InvalidSongLengthException
    {
        private const String DeffaultMessage = "Song minutes should be between 0 and 14.";
        public InvalidSongMinutesException() : base(DeffaultMessage)
        {
        }

        public InvalidSongMinutesException(String message) : base(message)
        {

        }


    }

