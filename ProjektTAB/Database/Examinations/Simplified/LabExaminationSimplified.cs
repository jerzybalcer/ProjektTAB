using Database.Users.Simplified;

namespace Database.Examinations.Simplified
{
    public  class LabExaminationSimplified
    {
        public int LabExaminationId { get; set; }
        public string? Result { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public string? LabManagerComment { get; set; }
        public string? DoctorComment { get; set; }
        public DateTime? ClosingDate { get; set; }
        public LabExaminationStatus Status { get; set; }
        public ExaminationTemplate? ExaminationTemplate { get; set; }
        public UserSimplified? LabAssistant { get; set; }
        public UserSimplified? LabManager { get; set; }
        public LabExaminationSimplified(UserSimplified? labAssistant,UserSimplified? labManager, LabExamination exam,DateTime executionTime)
        {
            Status = exam.Status;
            LabExaminationId = exam.LabExaminationId;
            if (labAssistant != null)
            {
                ExecutionDate = executionTime;
                Result = exam.Result;
                ExaminationTemplate = exam.ExaminationTemplate;
                ExecutionDate = executionTime;
                LabAssistant = labAssistant;
            }
            else
            {
                LabManager = labManager;
                LabManagerComment = exam.LabManagerComment;
            }

            DoctorComment = exam.DoctorComment;
        }
        public LabExaminationSimplified()
        {
        }
    }
}
