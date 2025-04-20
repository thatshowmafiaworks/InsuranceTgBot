using InsuranceTgBot.Data;
using InsuranceTgBot.Models;
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
    }
}
