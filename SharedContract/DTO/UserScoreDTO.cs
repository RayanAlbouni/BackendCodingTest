namespace SharedContract.DTO
{
    public class UserScoreDTO
    {
        public UserScoreDTO() { }
        public UserScoreDTO (string username, int score) { this.UserName = username;this.Score = score; }
        public string UserName { get; set; }
        public int Score { get; set; }
    }
}
