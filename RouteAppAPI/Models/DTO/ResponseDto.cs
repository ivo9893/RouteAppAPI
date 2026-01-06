namespace RouteAppAPI.Models.DTO
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiry { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Errors = null
            };
        }
        public static ApiResponse<T> Fail(string errorMessage, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = errorMessage,
                Errors = errors ?? new List<string> { errorMessage }
            };
        }
    }
}
