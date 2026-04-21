namespace Project.BLL
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; }
        public int DurationInMinutes { get; set; }
        public string TokenType { get; set; } = "Bearer";

        public AuthResponseDto(string accessToken, int durationInMinutes)
        {
            AccessToken = accessToken;
            DurationInMinutes = durationInMinutes;
        }
    }
}
