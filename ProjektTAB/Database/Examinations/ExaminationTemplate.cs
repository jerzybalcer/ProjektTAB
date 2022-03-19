namespace Database.Examinations
{
    public class ExaminationTemplate
    {
        public int ExaminationCode { get; set; }
        public ExaminationType ExaminationType { get; set; }
        public string Name { get; set; }

        public ExaminationTemplate(ExaminationType examinationType, string name)
        {
            ExaminationType = examinationType;
            Name = name;
        }
    }
}
