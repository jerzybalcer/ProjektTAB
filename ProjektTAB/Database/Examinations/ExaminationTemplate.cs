namespace Database.Examinations
{
    public class ExaminationTemplate
    {
        public int ExaminationCode { get; set; }
        public ExaminationType ExaminationType { get; set; }
        public string Name { get; set; }

        public ExaminationTemplate(int examinationCode, ExaminationType examinationType, string name)
        {
            ExaminationCode = examinationCode;
            ExaminationType = examinationType;
            Name = name;
        }
    }
}
