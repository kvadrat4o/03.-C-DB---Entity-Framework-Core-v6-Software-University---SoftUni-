using System.Collections.Generic;

namespace P01_HospitalDatabase.Data.Models
{
    public class Doctor
    {
        public string Name { get; set; }

        public int DoctorId { get; set; }

        public string Specialty { get; set; }

        public ICollection<Visitation> Visitations { get; set; } = new List<Visitation>();
    }
}
