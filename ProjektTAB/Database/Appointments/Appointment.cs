using Database.Appointments;
using Database.Examinations;
using Database.Patients;
using Database.Users;

namespace Database
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string? Description { get; set; }
        public string? Diagnosis { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public Receptionist Receptionist { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public ICollection<PhysicalExamination> PhysicalExaminations { get; set; }
        public ICollection<LabExamination> LabExaminations { get; set; }

        public Appointment(DateTime registrationDate, Receptionist receptionist, Doctor doctor, Patient patient)
        {
            RegistrationDate = registrationDate;
            Receptionist = receptionist;
            Doctor = doctor;
            Patient = patient;

            Status = AppointmentStatus.Registered;
            PhysicalExaminations = new List<PhysicalExamination>();
            LabExaminations = new List<LabExamination>();
        }

        protected Appointment()
        {

        }
    }
}