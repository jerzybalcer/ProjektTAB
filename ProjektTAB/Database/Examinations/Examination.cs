namespace Database.Examinations
{
    public class Examination
    {
        public int ExaminationId { get; set; }
        public string? Result { get; set; }
        public Examination(string? result = null)
        {
            Result = result;
        }
    }
}
