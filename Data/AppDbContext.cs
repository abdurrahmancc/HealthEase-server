using HealthEase.DTOs;
using HealthEase.Models;
using HealthEase.Models.Auth;
using HealthEase.Models.Doctor;
using Microsoft.EntityFrameworkCore;

namespace HealthEase.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationUserRole> UserRoles { get; set; }
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<AppListCountry> AppListCountries { get; set; }
        public DbSet<AppLanguage> AppLanguages { get; set; }
        public DbSet<DoctorBasicInfoModel> DoctorBasicInfos { get; set; }
        public DbSet<DoctorExperienceModel> DoctorExperiences { get; set; }
        public DbSet<DoctorEducationModel> DoctorEducations { get; set; }
        public DbSet<DoctorMembershipModel> DoctorMemberships { get; set; }
        public DbSet<DoctorClinicModel> DoctorClinics { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
        }

    }
}
