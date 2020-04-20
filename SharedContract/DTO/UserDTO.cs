namespace SharedContract.DTO
{
    public class UserDTO
    {
        public UserDTO() { }
        public UserDTO(string username) { this.UserName = username; }
        public string UserName { get; set; }
    }
}
