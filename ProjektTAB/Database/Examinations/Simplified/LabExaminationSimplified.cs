using Database.Users.Simplified;

namespace Database.Examinations.Simplified
{
    public  class LabExaminationSimplified
    {
        public int LabExaminationId { get; set; }
        public string? Result { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public string? LabManagerComment { get; set; }
        public DateTime? ClosingDate { get; set; }
        public LabExaminationStatus Status { get; set; }
        public ExaminationTemplate ExaminationTemplate { get; set; }
        public UserSimplified? LabAssistant { get; set; }
        public UserSimplified? LabManager { get; set; }
        public LabExaminationSimplified(UserSimplified labWorker, LabExamination exam,DateTime executionTime)
        {
            LabExaminationId = exam.LabExaminationId;
            ExecutionDate = executionTime;
            Result = exam.Result;
            ExaminationTemplate = exam.ExaminationTemplate;
            Status = exam.Status;
            ExecutionDate = executionTime;
            ClosingDate = DateTime.Now;
            LabAssistant = labWorker;
        }

        public LabExaminationSimplified()
        {
        }
    }
}
