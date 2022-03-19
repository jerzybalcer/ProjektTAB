using Database.Examinations;
using Database.People;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class ClinicContext : DbContext
    {
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
            modelBuilder.Entity<Patient>().ToTable("Patients").HasKey(x=>x.UserId);
            modelBuilder.Entity<Receptionist>().ToTable("Receptionists").HasKey(x => x.UserId);
            modelBuilder.Entity<Doctor>().ToTable("Doctors").HasKey(x => x.UserId);
            modelBuilder.Entity<LabManager>().ToTable("LabManagers").HasKey(x => x.UserId);
            modelBuilder.Entity<LabAssistant>().ToTable("LabAssistants").HasKey(x => x.UserId);
        }
    }
}
