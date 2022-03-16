using Database.Enums;

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


        public Appointment(DateTime registrationDate, string? description = null, string? diagnosis = null, AppointmentStatus status = AppointmentStatus.YetToCome, DateTime? closingDate = null)
        {
            RegistrationDate = registrationDate;
            Description = description;
            Diagnosis = diagnosis;
            Status = status;
            ClosingDate = closingDate;
        }
    }
}