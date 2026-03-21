namespace DeliveryApi.Services.Implementations;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeliveryApi.Config;
using DeliveryApi.DTOs.Auth;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interfaces;
using DeliveryApi.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DeliveryApi.Exceptions;

public class AuthService : IAuthService
{

    private readonly IUsuarioRepository _usuarioRepo;
    private readonly IClienteRepository _clienteRepo;
    private readonly JwtSettings _jwtSettings;
    public AuthService(
        IUsuarioRepository usuarioRepo,
        IClienteRepository clienteRepo,
        IOptions<JwtSettings> jwtSettings)
    {
        _usuarioRepo = usuarioRepo;
        _clienteRepo = clienteRepo;
        _jwtSettings = jwtSettings.Value;

    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var usuario = await _usuarioRepo.GetByEmailAsync(loginRequestDto.Email)
            ?? throw new UnauthorizedAccessException("Email o contraseña incorrectos");

        if (!BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, usuario.Password))
            throw new UnauthorizedAccessException("Email o contraseña incorrectos");

        return GenerarToken(usuario);
    }

    public async Task<LoginResponseDto> RegisterAsync(RegisterDto dto)
    {
        if (await _usuarioRepo.EmailExistsAsync(dto.Email))
            throw new ConflictExcption("El email ya está registrado");

        var usuario = new Usuario
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Rol = "cliente",
            Telefono = dto.Telefono
        };

        await _usuarioRepo.AddAsync(usuario);
        await _usuarioRepo.SaveAsync();

        // Crear el perfil de cliente automáticamente
        var cliente = new Cliente
        {
            Nombre = dto.Nombre,
            Telefono = dto.Telefono,
            UsuarioId = usuario.Id
        };

        await _clienteRepo.AddAsync(cliente);
        await _clienteRepo.SaveAsync();

        return GenerarToken(usuario);
    }

    private LoginResponseDto GenerarToken(Usuario usuario)
    {
        var expira = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes);
        // Claims = datos que van dentro del token
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Role, usuario.Rol)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expira,
            signingCredentials: creds
        );

        return new LoginResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Nombre = usuario.Nombre,
            Email = usuario.Email,
            Rol = usuario.Rol,
            Expira = expira
        };
    }
}