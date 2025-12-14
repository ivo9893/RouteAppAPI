namespace RouteAppAPI.Models.DTO
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public UserProfileDto User { get; set; }
    }

    public class ApiResponseDto<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
    }
}
