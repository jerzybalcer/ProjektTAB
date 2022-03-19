using Database.People;

namespace Database.Examinations
{
    public class LabExamination
    {
        public int LabExaminationId { get; set; }
        public string? Result { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public string? DoctorComment { get; set; }
        public string? LabManagerComment { get; set; }
        public DateTime? ClosingDate { get; set; }
        public LabExaminationStatus Status { get; set; }
        public Appointment Appointment { get; set; }
        public int ExaminationCode { get; set; }
        public ExaminationTemplate ExaminationTemplate { get; set; }
        public LabAssistant? LabAssistant { get; set; }
        public LabManager? LabManager { get; set; }

        public LabExamination(DateTime orderDate, Appointment appointment, ExaminationTemplate examinationTemplate, string? doctorComment = null)
        {
            OrderDate = orderDate;
            Appointment = appointment;
            ExaminationTemplate = examinationTemplate;
            ExaminationCode = ExaminationTemplate.ExaminationCode;
            DoctorComment = doctorComment;

            Status = LabExaminationStatus.Ordered;
        }
    }
}
