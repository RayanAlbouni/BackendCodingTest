using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseManager.Models
{
    public class UserModel
    {
        public UserModel() { }
        public UserModel(string userName) { this.Username = userName; }
        [Key]
        public int UserID { get; set; }
        [Required]
        public string Username { get; set; }
        public ICollection<UserScoreModel> UserScores { get; set; }

    }
}
