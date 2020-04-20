using System;
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
        private async Task<bool> IsUserExistAsync(string username)
        {
            return await _context.Users.AnyAsync(user => user.Username.Equals(username));
        }

        private async Task<UserModel> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Username.Equals(username));
        }

        public async Task<ResultDTO> RegisterUserAsync(string username)
        {
            var result = new ResultDTO();
            try
            {
                if (await IsUserExistAsync(username))
                {
                    result.Status = "Error";
                    result.Data = "User Already Exist";
                }
                else
                {
                    await _context.Users.AddAsync(new UserModel(username));
                    await _context.SaveChangesAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Status = "Error";
                result.Data = ex.Message;
                return result;
            }
        }

        public async Task<ResultDTO> AddUserScoreAsync(string username, int score)
        {
            var result = new ResultDTO();
            try
            {
                if (!await IsUserExistAsync(username))
                {
                    result.Status = "Error";
                    result.Data = "Username Not Exist";
                }
                else
                {
                    var user = await GetUserByUsernameAsync(username);
                    await _context.UserScores.AddAsync(new UserScoreModel(user.UserID, score));
                    await _context.SaveChangesAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Status = "Error";
                result.Data = ex.Message;
                return result;
            }
        }

        public async Task<ResultDTO> GetLeaderboardAsync(int? top_users)
        {
            var result = new ResultDTO();
            try
            {
                var data = await _context.UserScores.GroupBy(groupby => groupby.User.Username).Select(select => new UserScoreDTO(select.Key, select.Sum(sum => sum.Score))).ToListAsync();

                if (top_users.HasValue && top_users.Value > 0)
                    result.Data = data.Take(top_users.Value).OrderByDescending(order => order.Score).ToList();
                else
                    result.Data = data.OrderByDescending(order => order.Score).ToList();
                return result;
            }
            catch (Exception ex)
            {
                result.Status = "Error";
                result.Data = ex.Message;
                return result;
            }

        }

    }
}
