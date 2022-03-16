using Database.Enums;

namespace Database.Examinations
{
    public class LabExamination : Examination
    {
        public DateTime OrderDate { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public string? DoctorComment { get; set; }
        public string? LabManagerComment { get; set; }
        public DateTime? ClosingDate { get; set; }
        public LabExaminationStatus Status { get; set; }

        public LabExamination(DateTime orderDate, string? result = null, DateTime? executionDate = null, string? doctorComment = null, string? labManagerComment = null, DateTime? closingDate = null, LabExaminationStatus status = LabExaminationStatus.Ordered)
            : base(result)
        {
            OrderDate = orderDate;
            ExecutionDate = executionDate;
            DoctorComment = doctorComment;
            LabManagerComment = labManagerComment;
            ClosingDate = closingDate;
            Status = status;
        }
    }
}
