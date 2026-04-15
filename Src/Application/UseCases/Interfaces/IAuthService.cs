namespace DeliveryApi.Application.UseCases.Interfaces;

using DeliveryApi.Application.DTOs.Auth;

public interface IAuthService
{
    Task<LoginResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
}