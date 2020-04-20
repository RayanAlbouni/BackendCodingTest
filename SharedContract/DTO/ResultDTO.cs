namespace SharedContract.DTO
{
    public class ResultDTO
    {
        public ResultDTO() : this("Success", null) { }
        public ResultDTO(string status, string data) { this.Status = status; this.Data = data; }
        public string Status { get; set; }
        public object Data { get; set; }
    }
}
