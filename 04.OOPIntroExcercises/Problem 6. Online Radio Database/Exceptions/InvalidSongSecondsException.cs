using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

   class InvalidSongSecondsException:InvalidSongLengthException
   {
       private const string DeffautMessage = "Song seconds should be between 0 and 59.";
        public InvalidSongSecondsException():base(DeffautMessage)
        {
        }

        public InvalidSongSecondsException(string message) : base(message)
        {
        }
    }

