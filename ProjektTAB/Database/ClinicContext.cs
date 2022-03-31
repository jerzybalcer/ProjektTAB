using Database.Examinations;
using Database.Patients;
using Database.Users;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class ClinicContext : DbContext
    {
        public DbSet<ExaminationTemplate> ExaminationTemplates { get; set; }
        public DbSet<PhysicalExamination> PhysicalExaminations { get; set; }
        public DbSet<LabExamination> LabExaminations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<LabAssistant> LabAssistants { get; set; }
        public DbSet<LabManager> LabManagers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:tab-projekt.database.windows.net,1433;Initial Catalog=clinic;Persist Security Info=False;User ID=tab;Password=Projekt1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<Receptionist>().ToTable("Receptionists");
            modelBuilder.Entity<LabAssistant>().ToTable("LabAssistants");
            modelBuilder.Entity<LabManager>().ToTable("LabManagers");
        }
    }
}
