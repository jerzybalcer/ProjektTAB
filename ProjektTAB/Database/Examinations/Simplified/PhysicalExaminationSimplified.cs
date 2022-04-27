using Database.Appointments.Simplified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Examinations.Simplified
{
    public class PhysicalExaminationSimplified
    {
        public int PhysicalExaminationId { get; set; }
        public int AppointmentId { get; set; }
        public ExaminationTemplate ExaminationTemplate { get; set; }
        public string Result { get; set; }

        public PhysicalExaminationSimplified(int id, int appointment, ExaminationTemplate examinationTemplate, string result)
        {
            PhysicalExaminationId = id; 
            AppointmentId = appointment;
            ExaminationTemplate = examinationTemplate;
            Result = result;
        }
        public PhysicalExaminationSimplified()
        {

        }
    }
}
