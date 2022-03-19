using Database.Examinations;
using Database.People;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class ClinicContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ExaminationTemplate> ExaminationTemplates { get; set; }
        public DbSet<PhysicalExamination> PhysicalExaminations { get; set; }
        public DbSet<LabExamination> LabExaminations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:tab-projekt.database.windows.net,1433;Initial Catalog=clinic;Persist Security Info=False;User ID=tab;Password=Projekt1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<Receptionist>().ToTable("Receptionists");
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<LabManager>().ToTable("LabManagers");
            modelBuilder.Entity<LabAssistant>().ToTable("LabAssistants");

        }
    }
}
