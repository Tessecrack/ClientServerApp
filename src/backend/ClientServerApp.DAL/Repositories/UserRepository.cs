using ClientServerApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientServerApp.DAL.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext applicationContext)
        {
            _dbContext = applicationContext;
        }

        public async Task<IEnumerable<User>> GetAll(CancellationToken cancel = default) => await _dbContext.Users.AsNoTracking().ToListAsync();

        public async Task<User> Get(Guid id, CancellationToken cancel = default) 
            => await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancel) 
            ?? throw new Exception($"Not found user by id {id}");

        public async Task<User> Delete(Guid id, CancellationToken cancel = default)
        {
            var findedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancel) 
            ?? throw new Exception($"Not found user by id {id}");

            _dbContext.Users.Remove(findedUser);
            await _dbContext.SaveChangesAsync(cancel);
            return findedUser;
        }

        public async Task<User> Add(User user, CancellationToken cancel = default) 
        {
            user.Id = Guid.NewGuid();

            await _dbContext.AddAsync(user, cancel);
            await _dbContext.SaveChangesAsync(cancel);
            return user;
        }

        public async Task<User> Update(User user, CancellationToken cancel = default)
        {
            var findedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id, cancel) 
                ?? throw new Exception($"Not found user by id {user.Id}");

            findedUser.FirstName = user.FirstName;
            findedUser.LastName = user.LastName;
            findedUser.PhoneNumber = user.PhoneNumber;
            findedUser.Email = user.Email;

            await _dbContext.SaveChangesAsync(cancel);

            return findedUser;
        }
    }
}