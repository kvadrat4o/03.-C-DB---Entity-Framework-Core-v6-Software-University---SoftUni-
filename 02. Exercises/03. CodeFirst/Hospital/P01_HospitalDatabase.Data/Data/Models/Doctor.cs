using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data.Data.Models
{
    public class Doctor
    {
        public string Name { get; set; }

        public int DoctorId { get; set; }

        public string Specialty { get; set; }

        public ICollection<Visitation> Visitations { get; set; } = new List<Visitation>();
    }
}
