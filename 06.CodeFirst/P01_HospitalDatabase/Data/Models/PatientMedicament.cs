﻿

namespace P01_HospitalDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PatientMedicament
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

       
        public Medicament Medicament { get; set; }
        public int MedicamentId { get; set; }
    }
}
