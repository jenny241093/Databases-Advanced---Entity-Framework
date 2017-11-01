using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public class InvalidArtistNameException:InvalidSongException
   {
       private const String DeffaultMessage = "Artist name should be between 3 and 20 symbols.";
        public InvalidArtistNameException():base(DeffaultMessage)
        {

        }

        public InvalidArtistNameException(string message) : base(message)
        {

        }
    }

