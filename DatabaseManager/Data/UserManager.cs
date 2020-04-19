using DatabaseManager.Models;
using Microsoft.EntityFrameworkCore;
using SharedContract.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseManager.Data
{
    public class UserManager
    {
        GameContext _context;
        public UserManager(GameContext context)
        {
            _context = context;
        }
        public async Task<bool> IsUserExistAsync(string username)
        {
            return await _context.Users.AnyAsync(user => user.Username.Equals(username));
        }

        public async Task<UserModel> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Username.Equals(username));
        }

        public async Task<int> RegisterUserAsync(string username)
        {
            await _context.Users.AddAsync(new UserModel(username));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddUserScoreAsync(string username, int score)
        {
            var user = await GetUserByUsernameAsync(username);
            await _context.UserScores.AddAsync(new UserScoreModel(user.UserID, score));
            return await _context.SaveChangesAsync();
        }

        public async Task<List<UserScoreDTO>> GetLeaderboardAsync(int? top_users)
        {
            var result = await _context.UserScores.GroupBy(groupby => groupby.User.Username).Select(select=>new UserScoreDTO(select.Key, select.Sum(sum=>sum.Score))).ToListAsync();

            if (top_users.HasValue && top_users.Value > 0)
                return result.Take(top_users.Value).OrderByDescending(order => order.Score).ToList();
            return  result.OrderByDescending(order => order.Score).ToList();
        }

    }
}
