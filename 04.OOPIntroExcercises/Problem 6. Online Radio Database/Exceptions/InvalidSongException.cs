using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class InvalidSongException : ArgumentException
    {
        private const String DeffaultMessage = "Invalid song.";

        public InvalidSongException():base(DeffaultMessage)
        {

        }

        public InvalidSongException(string message) : base(message)
        {

        }
    }

