using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Examinations
{
    public class ExaminationTemplate
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), MaxLength(3)]
        public string ExaminationCode { get; set; }
        public ExaminationType ExaminationType { get; set; }
        public string Name { get; set; }

        public ExaminationTemplate(string examinationCode, ExaminationType examinationType, string name)
        {
            ExaminationCode = examinationCode;
            ExaminationType = examinationType;
            Name = name;
        }
    }
}
