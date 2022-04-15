using Database.Examinations;
using Database.Patients;
using Database.Users.Simplified;

namespace Database.Appointments.Simplified
{
    public class AppointmentSimplified
    {
        public int AppointmentId { get; set; }
        public string? Description { get; set; }
        public string? Diagnosis { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public UserSimplified Receptionist { get; set; }
        public UserSimplified Doctor { get; set; }
        public Patient Patient { get; set; }
        public ICollection<PhysicalExamination> PhysicalExaminations { get; set; }
        public ICollection<LabExamination> LabExaminations { get; set; }

        public AppointmentSimplified(DateTime registrationDate, UserSimplified receptionist, UserSimplified doctor, Patient patient)
        {
            RegistrationDate = registrationDate;
            Receptionist = receptionist;
            Doctor = doctor;
            Patient = patient;

            Status = AppointmentStatus.Registered;
            PhysicalExaminations = new List<PhysicalExamination>();
            LabExaminations = new List<LabExamination>();
        }

        public AppointmentSimplified()
        {

        }
    }
}
