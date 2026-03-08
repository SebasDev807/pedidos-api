namespace DeliveryApi.Services.Interfaces;

using DeliveryApi.DTOs.Auth;

public interface IAuthService
{
    Task<LoginResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
}