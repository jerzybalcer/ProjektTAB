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
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<LabManager> LabManagers { get; set; }
        public DbSet<LabAssistant> LabAssistants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:tab-projekt.database.windows.net,1433;Initial Catalog=clinic;Persist Security Info=False;User ID=tab;Password=Projekt1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(x =>
            {
                x.ToTable("Patients");
                x.Property(x => x.UserId).HasColumnName("PatientId");
                x.HasKey(x => x.UserId).HasName("PatientId");
            });
            modelBuilder.Entity<Receptionist>(x =>
            {
                x.ToTable("Receptionists");
                x.Property(x => x.UserId).HasColumnName("ReceptionistId");
                x.HasKey(x => x.UserId).HasName("ReceptionistId");
            });
            modelBuilder.Entity<Doctor>(x =>
            {
                x.ToTable("Doctors");
                x.Property(x => x.UserId).HasColumnName("DoctorId");
                x.HasKey(x => x.UserId).HasName("DoctorId");
            });
            modelBuilder.Entity<LabManager>(x =>
            {
                x.ToTable("LabManagers");
                x.Property(x => x.UserId).HasColumnName("LabManagerId");
                x.HasKey(x => x.UserId).HasName("LabManagerId");
            });
            modelBuilder.Entity<LabAssistant>(x =>
            {
                x.ToTable("LabAssistants");
                x.Property(x => x.UserId).HasColumnName("LabAssistantId");
                x.HasKey(x => x.UserId).HasName("LabAssistantId");
            });
        }
    }
}
