using InsuranceTgBot.Data;
using InsuranceTgBot.Models;
using InsuranceTgBot.Models.DTOs;
using InsuranceTgBot.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsuranceTgBot.Services
{
    public class UserRepository(
            AppDbContext context,
            ILogger<UserRepository> logger
        ) : IUserRepository
    {
        private async Task<User> Create(User user)
        {
            try
            {
                await context.Users.AddAsync(user);
                var userProgress = new UserProgress()
                {
                    UserId = user.Id,
                    UserTgId = user.TgId
                };
                await context.UserProgresses.AddAsync(userProgress);
                await context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return null;
            }
        }

        public async Task<bool> IsActual(User user)
        {
            try
            {
                var dbUser = await GetById(user.Id);
                if (user.UserName == null && dbUser.UserName == null) return true;
                if (!user.UserName.Equals(dbUser.UserName)) return false;
                if (user.FirstName == null && dbUser.FirstName == null) return true;
                if (!user.FirstName.Equals(dbUser.FirstName)) return false;
                if (user.LastName == null && dbUser.LastName == null) return true;
                if (!user.LastName.Equals(dbUser.LastName)) return false;
                if (user.ChatId != dbUser.ChatId) return false;
                return true;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return false;
            }
        }

        public async Task<User> Create(Telegram.Bot.Types.User user, long chatId)
        {
            try
            {
                var newUser = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    TgId = user.Id,
                    ChatId = chatId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.Username
                };
                return await Create(newUser);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return null;
            }
        }

        public async Task DeleteById(string id)
        {
            try
            {
                var user = await GetById(id);
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
            }
        }

        public async Task DeleteByTgId(long id)
        {
            try
            {
                var user = await GetByTgId(id);
                await DeleteById(user.Id);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
            }
        }

        public async Task<User> GetByChatId(long chatId)
        {
            try
            {
                var user = await context.Users.Where(x => x.ChatId == chatId).FirstOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return null;
            }
        }

        public async Task<User> GetById(string id)
        {
            try
            {
                var user = await context.Users.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return null;
            }
        }

        public async Task<User> GetByTgId(long tgId)
        {
            try
            {
                var user = await context.Users.Where(x => x.TgId == tgId).FirstOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return null;
            }
        }

        public async Task<User> GetByUsername(string username)
        {
            try
            {
                var user = await context.Users.Where(x => x.UserName.Equals(username)).FirstOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return null;
            }
        }

        public async Task Update(User user)
        {
            try
            {

                if (await IsActual(user)) return;
                var dbUser = await GetById(user.Id);
                dbUser.ChatId = user.ChatId;
                dbUser.UserName = user.UserName;
                dbUser.FirstName = user.FirstName;
                dbUser.LastName = user.LastName;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
            }
        }

        public async Task<UserProgress> GetProgress(User user)
        {
            try
            {
                var progress = await GetProgress(user.Id);
                return progress;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return null;
            }
        }

        public async Task<UserProgress> GetProgress(long tgId)
        {
            try
            {
                var progress = await context.UserProgresses.FirstOrDefaultAsync(x => x.UserTgId == tgId);
                if (progress is null) throw new NullReferenceException($"The progress with UserId : '{tgId}' was not found!");
                return progress;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return null;
            }
        }

        public async Task<UserProgress> GetProgress(string userId)
        {
            try
            {
                var progress = await context.UserProgresses.FirstOrDefaultAsync(x => x.UserId == userId);
                if (progress is null) throw new NullReferenceException($"The progress with UserId : '{userId}' was not found!");
                return progress;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return null;
            }
        }

        public async Task UpdateProgress(UserProgress updated)
        {
            try
            {
                context.UserProgresses.Update(updated);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
            }
        }

        public async Task AddLicense(DriverLicenseDto dto, string userId)
        {
            try
            {
                var license = await context.DriverLicenses.FirstOrDefaultAsync(x => x.UserId.Equals(userId));
                if (license is null)
                {
                    license = new DriverLicense()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = userId,
                        CountryCode = dto.CountryCode,
                        State = dto.State,
                        IdentificationNumber = dto.IdentificationNumber,
                        Category = dto.Category,
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        DateOfBirth = dto.DateOfBirth,
                        Issued = dto.Issued,
                        Expires = dto.Expires,
                        DDNumber = dto.DDNumber
                    };
                    await context.DriverLicenses.AddAsync(license);
                    await context.SaveChangesAsync();
                }
                else
                {
                    license.CountryCode = dto.CountryCode;
                    license.State = dto.State;
                    license.IdentificationNumber = dto.IdentificationNumber;
                    license.Category = dto.Category;
                    license.FirstName = dto.FirstName;
                    license.LastName = dto.LastName;
                    license.DateOfBirth = dto.DateOfBirth;
                    license.Issued = dto.Issued;
                    license.Expires = dto.Expires;
                    license.DDNumber = dto.DDNumber;
                    context.DriverLicenses.Update(license);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
            }
        }

        public async Task AddVehicleId(VehicleDocumentDto dto, string userId)
        {
            try
            {
                var vehicleDocument = new VehicleDocument()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    VehicleIdNumber = dto.VehicleIdNumber,
                    Manufacturer = dto.Manufacturer,
                    Model = dto.Model,
                    Issued = dto.Issued,
                    Manufactured = dto.Manufactured
                };
                await context.VehicleDocuments.AddAsync(vehicleDocument);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
            }
        }

        public async Task<DriverLicense> GetLicense(long tgId)
        {
            try
            {
                var user = await GetByTgId(tgId);
                if (user is null) throw new NullReferenceException("User not found");
                var license = await context.DriverLicenses.FirstOrDefaultAsync(x => x.UserId.Equals(user.Id));
                return license;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return null;
            }
        }

        public async Task<VehicleDocument> GetVehicle(long tgId)
        {
            try
            {
                var user = await GetByTgId(tgId);
                if (user is null) throw new NullReferenceException("User not found");
                var vehicleId = await context.VehicleDocuments.FirstOrDefaultAsync(x => x.UserId.Equals(user.Id));
                return vehicleId;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return null;
            }
        }
    }
}
