using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_BillsPaymentSystem.Data
{
    public class Configuration
    {
        public static string ConnectionString { get; set; } =
            @"Server=DESKTOP-5CTIM8C\SQLEXPRESS;Database=BillsPaymentSystem;Integrated Security=True";
    }
}
