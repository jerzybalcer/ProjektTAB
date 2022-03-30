using System.ComponentModel.DataAnnotations;

namespace Database.Examinations
{
    public class PhysicalExamination
    {
        public int PhysicalExaminationId { get; set; }
        public Appointment Appointment { get; set; }
        public ExaminationTemplate ExaminationTemplate { get; set; }
        public string Result { get; set; }

        public PhysicalExamination(Appointment appointment, ExaminationTemplate examinationTemplate, string result)
        {
            Appointment = appointment;
            ExaminationTemplate = examinationTemplate;
            Result = result;
        }

        protected PhysicalExamination()
        {

        }
    }
}
