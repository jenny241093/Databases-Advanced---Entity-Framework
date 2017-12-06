using System;
using System.Collections.Generic;
using System.Text;

namespace Instagraph.DataProcessor.Dtos
{
    public class UsersWithoutCommentsDto
    {
        public int Id { get; set; }
        public string Picture { get; set; }
        public string User { get; set; }
    }
}
