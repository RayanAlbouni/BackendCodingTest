using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseManager.Models
{
    public class UserScoreModel
    {
        public UserScoreModel()
        {

        }
        public UserScoreModel(int userID, int score)
        {
            this.UserID = userID;
            this.Score = score;
        }
        [Key]
        public int UserScoreID { get; set; }
        public int UserID { get; set; }
        public UserModel User { get; set; }
        [Required]
        public int Score { get; set; }
    }
}
