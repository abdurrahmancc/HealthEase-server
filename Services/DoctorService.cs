using HealthEase.Data;
using HealthEase.DTOs.Doctor;
using HealthEase.Models.Doctor;
using HealthEase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AutoMapper;
using HealthEase.Services.FilesManagement;
using HealthEase.Utilities;
using HealthEase.Helpers;
using CloudinaryDotNet.Actions;
using System.Numerics;
using Microsoft.IdentityModel.Tokens;


namespace HealthEase.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly FilesManagementHelper _filesManagementHelper;
        private readonly IDoctorHelper _dctorHelper;

        public DoctorService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor, IMapper mapper, FilesManagementHelper filesManagementHelper, IDoctorHelper dctorHelper)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _filesManagementHelper = filesManagementHelper;
            _dctorHelper = dctorHelper;
        }

        public async Task<DoctorBasicInfoCreateDto> UpdateBasicInfoService(DoctorBasicInfoCreateDto info)
        {
            try
            {
                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync();

                var existingInfo = await _appDbContext.DoctorBasicInfos.FirstOrDefaultAsync(b => b.DoctorId == doctor.DoctorId);
                var existingMemberships = await _appDbContext.DoctorMemberships.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();

                if (existingInfo != null)
                {
                    _mapper.Map(info, existingInfo);
                    _appDbContext.DoctorBasicInfos.Update(existingInfo);
                }
                else
                {
                    var newInfo = _mapper.Map<DoctorBasicInfoModel>(info);
                    newInfo.BasicInfoId = Guid.NewGuid();
                    newInfo.DoctorId = doctor.DoctorId;

                    _appDbContext.DoctorBasicInfos.Add(newInfo);
                }

                await _appDbContext.SaveChangesAsync();


                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DoctorBasicInfoCreateDto> GetBasicInfoService(string userId)
        {
            try
            {
                var isDoctor = _dctorHelper.IsDoctor();


                if (isDoctor)
                {
                    var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                    if (doctor == null) throw new KeyNotFoundException("Doctor not found");

                    var Info = await _appDbContext.DoctorBasicInfos.FirstOrDefaultAsync(b => b.DoctorId == doctor.DoctorId);

                    if (Info == null) throw new KeyNotFoundException("Doctor basic info not found");

                    return _mapper.Map<DoctorBasicInfoCreateDto>(Info);

                }
                else
                {
                    throw new UnauthorizedAccessException("You are a not authorized access to perform this action");
                }

            }
            catch (Exception) { throw; }
        }

        public async Task<string> UpdatePhotoUrlServiceeAsync(IFormFile file)
        {
            try
            {
                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync();

                var existingInfo = await _appDbContext.DoctorBasicInfos.FirstOrDefaultAsync(b => b.DoctorId == doctor.DoctorId);
                if (existingInfo == null)
                {
                    var newInfo = new DoctorBasicInfoModel
                    {
                        BasicInfoId = Guid.NewGuid(),
                        DoctorId = doctor.DoctorId
                    };
                    _appDbContext.DoctorBasicInfos.Add(newInfo);
                }
                else if (!string.IsNullOrEmpty(existingInfo.PhotoUrl))
                {
                    await _filesManagementHelper.DeleteImageHelperAsync(existingInfo.PhotoUrl);
                }

                var imageUrl = await _filesManagementHelper.UploadImageAsync(file);
                existingInfo.PhotoUrl = imageUrl;
                _appDbContext.DoctorBasicInfos.Update(existingInfo);
                await _appDbContext.SaveChangesAsync();

                await _appDbContext.SaveChangesAsync();



                return imageUrl;

            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }


        public async Task<List<DoctorMembershipReadDto>> UpdateMembershipsService(List<DoctorMembershipCreateDto> memberships, string userId)
        {
            try
            {
                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                var existingMemberships = await _appDbContext.DoctorMemberships.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();


                foreach (var membershipDto in memberships)
                {
                    var exist = existingMemberships.FirstOrDefault(e => e.MembershipId == membershipDto.MembershipId);
                    if (exist != null)
                    {
                        exist.Title = membershipDto.Title;
                        exist.About = membershipDto.About;
                    }
                    else
                    {
                        var membership = _mapper.Map<DoctorMembershipModel>(membershipDto);
                        membership.MembershipId = Guid.NewGuid();
                        membership.DoctorId = doctor.DoctorId;
                        _appDbContext.DoctorMemberships.Add(membership);
                    }

                }
                var toDelete = existingMemberships.Where(e => !memberships.Any(m => m.MembershipId == e.MembershipId)).ToList();
                _appDbContext.DoctorMemberships.RemoveRange(toDelete);

                await _appDbContext.SaveChangesAsync();

                var finalMemberships = await _appDbContext.DoctorMemberships.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();
                return _mapper.Map<List<DoctorMembershipReadDto>>(finalMemberships);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<DoctorMembershipReadDto>> GetMembershipsService(string userId)
        {
            try
            {
                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                var newMemberships = new List<DoctorMembershipModel>();

                if (doctor == null) throw new UnauthorizedAccessException("Doctor profile not found");

                newMemberships = await _appDbContext.DoctorMemberships.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();

                return _mapper.Map<List<DoctorMembershipReadDto>>(newMemberships);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<DoctorExperienceReadDto>> AddUpdateExperiencesService(List<DoctorExperienceCreateDto> experiences, string userId)
        {
            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                var existingExperiences = await _appDbContext.DoctorExperiences.Where(Ex => Ex.DoctorId == doctor.DoctorId).ToListAsync();

                var existingDict = existingExperiences.ToDictionary(e => e.ExperienceId, e => e);
                foreach (var experienceDto in experiences)
                {
                    if (existingDict.TryGetValue(experienceDto.ExperienceId, out var exist))
                    {
                        _mapper.Map(experienceDto, exist);

                        if (experienceDto.HospitalLogoFile != null)
                        {
                            if (!string.IsNullOrEmpty(exist.HospitalLogo))
                                await _filesManagementHelper.DeleteImageHelperAsync(exist.HospitalLogo);

                            var logoUrl = await _filesManagementHelper.UploadImageAsync(experienceDto.HospitalLogoFile);
                            exist.HospitalLogo = logoUrl;
                        }
                    }
                    else
                    {
                        var experience = _mapper.Map<DoctorExperienceModel>(experienceDto);
                        if (experienceDto.HospitalLogoFile != null)
                        {
                            var logoUrl = await _filesManagementHelper.UploadImageAsync(experienceDto.HospitalLogoFile);
                            experience.HospitalLogo = logoUrl;
                        }
                        experience.ExperienceId = Guid.NewGuid();
                        experience.DoctorId = doctor.DoctorId;
                        _appDbContext.DoctorExperiences.Add(experience);
                    }

                }

                var toDelete = existingExperiences.Where(exp => !experiences.Any(m => m.ExperienceId == exp.ExperienceId)).ToList();

                if (toDelete.Any())
                {
                    var deleteTasks = toDelete.Where(e => !string.IsNullOrEmpty(e.HospitalLogo)).Select(e => _filesManagementHelper.DeleteImageHelperAsync(e.HospitalLogo));
                    await Task.WhenAll(deleteTasks);

                    _appDbContext.DoctorExperiences.RemoveRange(toDelete);
                }

                await _appDbContext.SaveChangesAsync();
                await transaction.CommitAsync();


                var finalExperience = await _appDbContext.DoctorExperiences.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();
                return _mapper.Map<List<DoctorExperienceReadDto>>(finalExperience);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<List<DoctorExperienceReadDto>> GetExperiencesService(string userId)
        {
            try
            {
                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                if (doctor == null) throw new UnauthorizedAccessException("Doctor profile not found");

                var experiences = await _appDbContext.DoctorExperiences.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();

                return _mapper.Map<List<DoctorExperienceReadDto>>(experiences);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DoctorEducationReadDto>> AddUpdateEducationsService(List<DoctorEducationCreateDto> educations, string userId)
        {
            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                var existingEducations = await _appDbContext.DoctorEducations.Where(Ex => Ex.DoctorId == doctor.DoctorId).ToListAsync();

                var existingDict = existingEducations.ToDictionary(e => e.EducationId, e => e);
                foreach (var educationDto in educations)
                {

                    if ((educationDto.EndDate != null && educationDto.IsCurrentlyOngoing == true) || (educationDto.EndDate == null && educationDto.IsCurrentlyOngoing == false))
                        throw new ArgumentException("Education must either have an EndDate or be marked as currently ongoing.");



                    if (existingDict.TryGetValue(educationDto.EducationId, out var exist))
                    {
                        _mapper.Map(educationDto, exist);
                    }
                    else
                    {
                        var education = _mapper.Map<DoctorEducationModel>(educationDto);
                        education.EducationId = Guid.NewGuid();
                        education.DoctorId = doctor.DoctorId;
                        _appDbContext.DoctorEducations.Add(education);
                    }

                }

                var toDelete = existingEducations.Where(exp => !educations.Any(m => m.EducationId == exp.EducationId)).ToList();

                if (toDelete.Any())
                {
                    _appDbContext.DoctorEducations.RemoveRange(toDelete);
                }

                await _appDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                var finalEducation = await _appDbContext.DoctorEducations.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();
                return _mapper.Map<List<DoctorEducationReadDto>>(finalEducation);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<List<DoctorEducationReadDto>> GetEducationsService(string userId)
        {
            try
            {
                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                if (doctor == null) throw new UnauthorizedAccessException("Doctor profile not found");

                var educations = await _appDbContext.DoctorEducations.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();

                return _mapper.Map<List<DoctorEducationReadDto>>(educations);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<DoctorClinicReadDto>> AddUpdateClinicsService(List<DoctorClinicCreateDto> clinics, string userId)
        {
            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                var existingClinics = await _appDbContext.DoctorClinics.Where(Ex => Ex.DoctorId == doctor.DoctorId).ToListAsync();


                var existingDict = existingClinics.ToDictionary(e => e.ClinicId, e => e);
                foreach (var clinicDto in clinics)
                {
                    if (existingDict.TryGetValue(clinicDto.ClinicId, out var exist))
                    {
                        _mapper.Map(clinicDto, exist);


                        if (clinicDto.ClinicLogosFile != null && clinicDto.ClinicLogosFile.Any())
                        {
                            var urls = new List<string>();
                            foreach (var file in clinicDto.ClinicLogosFile)
                            {
                                var logoUrl = await _filesManagementHelper.UploadImageAsync(file);
                                urls.Add(logoUrl);
                            }
                            exist.ClinicLogos.AddRange(urls);
                        }

                        if (clinicDto.ClinicLogos != null)
                        {
                            var logosToDelete = exist.ClinicLogos.Where(l => !clinicDto.ClinicLogos.Contains(l)).ToList();

                            if (logosToDelete.Any())
                            {
                                var deleteTasks = logosToDelete.Select(l => _filesManagementHelper.DeleteImageHelperAsync(l));
                                await Task.WhenAll(deleteTasks);
                                exist.ClinicLogos = exist.ClinicLogos.Where(l => !logosToDelete.Contains(l)).ToList();
                            }
                        }
                    }
                    else
                    {
                        var newClinic = _mapper.Map<DoctorClinicModel>(clinicDto);
                        newClinic.DoctorId = doctor.DoctorId;

                        if (clinicDto.ClinicLogosFile != null && clinicDto.ClinicLogosFile.Any())
                        {
                            var urls = new List<string>();
                            foreach (var file in clinicDto.ClinicLogosFile)
                            {
                                var logoUrl = await _filesManagementHelper.UploadImageAsync(file);
                                urls.Add(logoUrl);
                            }
                            newClinic.ClinicLogos.AddRange(urls);
                        }

                        _appDbContext.DoctorClinics.Add(newClinic);
                    }

                }

                var toDelete = existingClinics.Where(exp => !clinics.Any(m => m.ClinicId == exp.ClinicId)).ToList();

                if (toDelete.Any())
                {
                    var deleteTasks = toDelete.Where(e => e.ClinicLogos != null && e.ClinicLogos.Any())
                        .SelectMany(e => e.ClinicLogos.Where(logo => !string.IsNullOrEmpty(logo))
                        .Select(logo => _filesManagementHelper.DeleteImageHelperAsync(logo)));

                    await Task.WhenAll(deleteTasks);

                    _appDbContext.DoctorClinics.RemoveRange(toDelete);
                }

                await _appDbContext.SaveChangesAsync();
                await transaction.CommitAsync();


                var finalClinic = await _appDbContext.DoctorClinics.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();
                return _mapper.Map<List<DoctorClinicReadDto>>(finalClinic);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<DoctorClinicReadDto>> GetClinicsService(string userId)
        {
            try
            {
                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                if (doctor == null) throw new UnauthorizedAccessException("Doctor profile not found");

                var educations = await _appDbContext.DoctorClinics.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();

                return _mapper.Map<List<DoctorClinicReadDto>>(educations);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<DoctorBusinessHourReadDto>> AddUpdateBusinessHourService(List<DoctorBusinessHourCreateDto> businessHours, string userId)
        {
            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {

                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                var existingbusinessHours = await _appDbContext.DoctorBusinessHours.Where(Ex => Ex.DoctorId == doctor.DoctorId).ToListAsync();

                var existingDict = existingbusinessHours.ToDictionary(e => e.BusinessHourId, e => e);
                foreach (var businessHourDto in businessHours)
                {
                    if (businessHourDto.BusinessHourId > 0 && existingDict.TryGetValue(businessHourDto.BusinessHourId, out var exist))
                    {
                        exist.Day = businessHourDto.Day;
                        exist.FromUTC = businessHourDto.FromUTC;
                        exist.ToUTC = businessHourDto.ToUTC;
                    }
                    else
                    {
                        var newBusinessHour = _mapper.Map<DoctorBusinessHourModel>(businessHourDto);
                        newBusinessHour.DoctorId = doctor.DoctorId;
                        _appDbContext.DoctorBusinessHours.Add(newBusinessHour);
                    }

                }

                var toDelete = existingbusinessHours.Where(exp => !businessHours.Any(m => m.BusinessHourId == exp.BusinessHourId)).ToList();
                if (toDelete.Any())
                {
                    _appDbContext.DoctorBusinessHours.RemoveRange(toDelete);
                }

                await _appDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                var finalBusinessHour = await _appDbContext.DoctorBusinessHours.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();
                return _mapper.Map<List<DoctorBusinessHourReadDto>>(finalBusinessHour);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<DoctorBusinessHourReadDto>> GetBusinessHoursService(string userId)
        {
            try
            {
                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                if (doctor == null) throw new UnauthorizedAccessException("Doctor profile not found");

                var businessHours = await _appDbContext.DoctorBusinessHours.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();

                return _mapper.Map<List<DoctorBusinessHourReadDto>>(businessHours);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<DoctorServiceReadDto>> AddUpdateServicesService(List<DoctorServiceCreateDto> services, string userId)
        {

            try
            {
                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                var existingServices = await _appDbContext.DoctorServices.Where(Ex => Ex.DoctorId == doctor.DoctorId).ToListAsync();

                var existingDict = existingServices.ToDictionary(e => e.Id, e => e);
                foreach (var serviceDto in services)
                {
                    if (serviceDto.Id.HasValue && existingDict.TryGetValue(serviceDto.Id.Value, out var exist))
                    {
                        exist.Speciality = serviceDto.Speciality;
                        exist.Service = serviceDto.Service;
                        exist.Price = serviceDto.Price;
                        exist.About = serviceDto.About;
                    }
                    else
                    {
                        var newService = new DoctorServiceModel
                        {
                            Speciality = serviceDto.Speciality,
                            Service = serviceDto.Service,
                            Price = serviceDto.Price,
                            About = serviceDto.About,
                            DoctorId = doctor.DoctorId
                        };
                        _appDbContext.DoctorServices.Add(newService);
                    }

                }

                var toDelete = existingServices.Where(exp => !services.Any(m => m.Id == exp.Id)).ToList();
                if (toDelete.Any())
                {
                    _appDbContext.DoctorServices.RemoveRange(toDelete);
                }

                await _appDbContext.SaveChangesAsync();


                var finalSerivces = await _appDbContext.DoctorServices.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();
                return _mapper.Map<List<DoctorServiceReadDto>>(finalSerivces);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DoctorServiceReadDto>> GetServicesService(string userId)
        {
            try
            {
                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                if (doctor == null) throw new UnauthorizedAccessException("Doctor profile not found");

                var services = await _appDbContext.DoctorServices.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();

                return _mapper.Map<List<DoctorServiceReadDto>>(services);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<SocialMediaReadDto>> AddUpdateSocialMideaService(List<SocialMediaCreateDto> socials, string userId)
        {
            try
            {
                var isDoctor = _dctorHelper.IsDoctor();
                var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                var existingSocial = await _appDbContext.SocialMediaLinks.Where(s => s.DoctorId == doctor.DoctorId).ToListAsync();

                var existingDict = existingSocial.ToDictionary(e => e.Id, e => e);

                foreach (var socialDto in socials)
                {
                    if (socialDto.Id != 0 && existingDict.TryGetValue(socialDto.Id, out var exist))
                    {
                        exist.SocialMedia = socialDto.SocialMedia;
                        exist.Link = socialDto.Link;
                    }
                    else
                    {
                        var newSocial = new SocialMediaModel
                        {
                            SocialMedia = socialDto.SocialMedia,
                            Link = socialDto.Link,
                            DoctorId = doctor.DoctorId
                        };
                        _appDbContext.SocialMediaLinks.Add(newSocial);
                    }
                }

                var toDelete = existingSocial.Where(e => !socials.Any(s => s.Id == e.Id)).ToList();

                if (toDelete.Any())
                {
                    _appDbContext.SocialMediaLinks.RemoveRange(toDelete);
                }

                await _appDbContext.SaveChangesAsync();


                var finalSocials = await _appDbContext.SocialMediaLinks .Where(s => s.DoctorId == doctor.DoctorId).ToListAsync();

                return _mapper.Map<List<SocialMediaReadDto>>(finalSocials);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<SocialMediaReadDto>> GetSocialsService(string userId)
        {
            try
            {
                var isDoctor = _dctorHelper.IsDoctor();

                var doctor = await _dctorHelper.GetOrCreateDoctorAsync(userId);

                if (doctor == null) throw new UnauthorizedAccessException("Doctor profile not found");

                var socials = await _appDbContext.SocialMediaLinks.Where(m => m.DoctorId == doctor.DoctorId).ToListAsync();

                return _mapper.Map<List<SocialMediaReadDto>>(socials);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
