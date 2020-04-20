using System;
using System.Threading.Tasks;
using DatabaseManager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedContract.DTO;

namespace BackendCodingTest.Controllers
{
    [ApiController]
    [Authorize]
    public class APIController : ControllerBase
    {
        UserManager userManager;

        public APIController(GameContext context)
        {
            userManager = new UserManager(context);
        }

        /// <summary>
        /// Register a new user that can report a score
        /// POST: api/register
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> register(UserDTO request)
        {
            var result = await userManager.RegisterUserAsync(request.UserName);
            return Ok(result);
        }


        /// <summary>
        /// Store game result for specified user
        /// POST: api/report
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [Route("api/report")]
        public async Task<IActionResult> report(UserScoreDTO request)
        {
            var result = await userManager.AddUserScoreAsync(request.UserName, request.Score);
            return Ok(result);
        }


        /// <summary>
        /// Returns a list of users sorted by their highest score value
        /// Get: api/getleaderboard
        /// </summary>
        /// <param name="top_users"></param>
        [HttpGet]
        [Route("api/getleaderboard")]
        [Route("api/getleaderboard/{top_users}")]
        public async Task<IActionResult> GetLeaderboard(int? top_users)
        {
            var result = await userManager.GetLeaderboardAsync(top_users);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/IsAlive")]
        public IActionResult IsAlive()
        {
            return Ok(string.Format("You have reached the server successfully, the local time:{0}", DateTime.Now));
        }

    }
}
