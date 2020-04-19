using BackendCodingTest.Controllers;
using DatabaseManager.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedContract.DTO;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestProject
{
    public class APIControllerUnitTest
    {
        APIController _controller;
        GameContext _context;
        public APIControllerUnitTest()
        {
            var options = new DbContextOptionsBuilder<GameContext>().UseSqlServer("Server=tcp:database-1.coqaybrmjcqc.us-east-2.rds.amazonaws.com,1433;Initial Catalog=test-database;Persist Security Info=False;User ID=admin;Password=admintestdatabase;MultipleActiveResultSets=False;Encrypt=True;Connection Timeout=30;TrustServerCertificate=True;").Options;
            _context = new GameContext(options);
            _controller = new APIController(_context);
        }

        [Fact]
        public void RegisterUser_Returns_OK()
        {
            //Arrange
            var username = string.Format("TestUser_{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
            var user = new UserDTO(username);

            // Act
            var okResult = _controller.register(user).Result;

            // Assert
            Assert.IsType<OkResult>(okResult);
        }

        [Fact]
        public void RegisterUser_Returns_Badrequest()
        {
            //Arrange
            var username = string.Format("TestUser");
            var user = new UserDTO(username);

            // Act
            var badrequestResult = _controller.register(user).Result as BadRequestObjectResult;
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(badrequestResult);
            Assert.Equal("Username Already Exist", badrequestResult.Value.ToString());
        }

        [Fact]
        public void ReportUserScore_Returns_OK()
        {
            //Arrange
            var username = string.Format("TestUser_20200420033913");
            var userscore = new UserScoreDTO(username, 10);

            // Act
            var okResult = _controller.report(userscore).Result;

            // Assert
            Assert.IsType<OkResult>(okResult);
        }

        [Fact]
        public void ReportUserScore_Returns_Badrequest()
        {
            //Arrange
            var username = string.Format("Not_Exist_User_#$&");
            var userscore = new UserScoreDTO(username, 10);
            
            // Act
            var badrequestResult = _controller.report(userscore).Result as BadRequestObjectResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(badrequestResult);
            Assert.Equal("Username Not Exist", badrequestResult.Value.ToString());

        }

        [Fact]
        public void GetLeaderboard_Returns_OK()
        {
            // Act
            var okObjectResult = _controller.GetLeaderboard(null).Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okObjectResult);
            Assert.NotNull(okObjectResult);
            var result = okObjectResult.Value as List<UserScoreDTO>;
            Assert.NotNull(result);
        }

        [Fact]
        public void GetLeaderboard_Top10_Returns_OK()
        {
            // Act 
            var okObjectResult = _controller.GetLeaderboard(10).Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okObjectResult);
            Assert.NotNull(okObjectResult);
            var result = okObjectResult.Value as List<UserScoreDTO>;
            Assert.NotNull(result);
        }
    }
}
