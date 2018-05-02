using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Patient
    {
        public Patient()
        {

        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int PatientId { get; set; }

        public bool HasInsurance { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<Visitation> Visitations { get; set; } = new List<Visitation>();

        public ICollection<Diagnose> Diagnoses { get; set; } = new List<Diagnose>();

        public ICollection<PatientMedicament> Prescriptions { get; set; } = new List<PatientMedicament>();
    }
}
