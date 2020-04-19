using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseManager.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedContract.DTO;

namespace BackendCodingTest.Controllers
{
    //[Route("api/[action]")]
    [ApiController]
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
        /// <param name="username"></param>
        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> register(UserDTO request)
        {
            if (await userManager.IsUserExistAsync(request.UserName))
                return BadRequest("Username Already Exist");
            await userManager.RegisterUserAsync(request.UserName);
            return Ok();
        }


        /// <summary>
        /// Store game result for specified user
        /// POST: api/report
        /// </summary>
        /// <param name="username"></param>
        /// <param name="score"></param>
        [HttpPost]
        public async Task<IActionResult> report(UserScoreDTO request)
        {
            if (!await userManager.IsUserExistAsync(request.UserName))
                return BadRequest("Username Not Exist");
            await userManager.AddUserScoreAsync(request.UserName, request.Score);
            return Ok();
        }


        // GET: api/API
        /// <summary>
        /// 
        /// </summary>
        /// <param name="top_users">Optional</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/Getleaderboard")]
        [Route("/Getleaderboard/{top_users}")]
        public async Task<IActionResult> GetLeaderboard(int? top_users)
        {
            var result = await userManager.GetLeaderboardAsync(top_users);
            return Ok(result);
        }

    }
}
