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
        public void RegisterUser_Returns_Success()
        {
            //Arrange
            var username = string.Format("TestUser_{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
            var user = new UserDTO(username);

            // Act
            var okResult = _controller.register(user).Result;
            var okObjectResult = okResult as OkObjectResult;
            var val = okObjectResult.Value as ResultDTO;
            
            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<ResultDTO>(okObjectResult.Value);
            Assert.Equal("Success", val.Status);

        }

        [Fact]
        public void RegisterUser_Returns_Error()
        {
            //Arrange
            var username = string.Format("TestUser");
            var user = new UserDTO(username);

            // Act
            var okResult = _controller.register(user).Result;
            var okObjectResult = okResult as OkObjectResult;
            var val = okObjectResult.Value as ResultDTO;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<ResultDTO>(okObjectResult.Value);
            Assert.Equal("Error", val.Status);
            Assert.Equal("User Already Exist", val.Data);
        }

        [Fact]
        public void ReportUserScore_Returns_Success()
        {
            //Arrange
            var username = string.Format("TestUser_20200420033913");
            var userscore = new UserScoreDTO(username, 10);

            // Act
            var okResult = _controller.report(userscore).Result;
            var okObjectResult = okResult as OkObjectResult;
            var val = okObjectResult.Value as ResultDTO;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<ResultDTO>(okObjectResult.Value);
            Assert.Equal("Success", val.Status);
        }

        [Fact]
        public void ReportUserScore_Returns_Error()
        {
            //Arrange
            var username = string.Format("Not_Exist_User_#$&");
            var userscore = new UserScoreDTO(username, 10);

            // Act
            var okResult = _controller.report(userscore).Result;
            var okObjectResult = okResult as OkObjectResult;
            var val = okObjectResult.Value as ResultDTO;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<ResultDTO>(okObjectResult.Value);
            Assert.Equal("Error", val.Status);
            Assert.Equal("Username Not Exist", val.Data);
        }

        [Fact]
        public void GetLeaderboard_Returns_Success()
        {
            // Act
            var okResult = _controller.GetLeaderboard(null).Result;
            var okObjectResult = okResult as OkObjectResult;
            var val = okObjectResult.Value as ResultDTO;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<ResultDTO>(okObjectResult.Value);
            Assert.IsType<List<UserScoreDTO>>(val.Data);
            Assert.Equal("Success", val.Status);
        }

        [Fact]
        public void GetLeaderboard_Top10_Returns_Success()
        {
            // Act 
            var okResult = _controller.GetLeaderboard(10).Result;
            var okObjectResult = okResult as OkObjectResult;
            var val = okObjectResult.Value as ResultDTO;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<ResultDTO>(okObjectResult.Value);
            Assert.IsType<List<UserScoreDTO>>(val.Data);
            Assert.Equal("Success", val.Status);
        }
    }
}
