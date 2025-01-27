using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.src.Dto;
using api.src.Dto.Usuarios;
using api.src.Entities;
using api.src.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api.src.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuarioController: ControllerBase
    {
        private readonly IUsuariosRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public UsuarioController(IUsuariosRepository usuariosRepository, IConfiguration configuration)
        {
            this._usuarioRepository = usuariosRepository;
            this._configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuarios login)
        {
            ApiResponse<object> response;

            try
            {
                var user = await this._usuarioRepository.checkUsuarioExits(login);

                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Usuario),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("Identificador", user.Identificador.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"])),
                        signingCredentials: creds
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    response = new ApiResponse<object>(new
                    {
                        token = tokenString,
                        userId = user.Identificador,
                        username = user.Usuario
                    })
                    {
                        Message = "Login exitoso",
                        Succeeded = true
                    };
                }
                else
                {
                    response = new ApiResponse<object>("Credenciales inválidas.", false)
                    {
                        Message = ApiResponse<string>.Error,
                    };
                }
            }
            catch (Exception ex)
            {
                response = new ApiResponse<object>(null, ex.Message, false)
                {
                    Message = ApiResponse<string>.Error,
                };
            }

            return Ok(response);
        }


        [HttpPost("getUsuarios")]
        public async Task<ActionResult<PagedList<Usuarios>>> GetUsuarios(Pagination<string> pagination)
        {
            ApiResponse<PagedList<Usuarios>> response;
            
            try
            {
                PagedList<Usuarios> pageList = await this._usuarioRepository.GetUsuarios(pagination);
                response = new ApiResponse<PagedList<Usuarios>>(pageList);
                response.Message = "Consulta realizada correctamente";
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                
                response = new ApiResponse<PagedList<Usuarios>>(null, ex.Message, false);
                response.Message = ApiResponse<string>.Error;
                response.Succeeded = false;
            }
            
            return Ok(response);
        }


        [HttpPost("getUsuario")]
        public async Task<ActionResult<Usuarios?>> GetUsuario(UsuarioRequestDto usuario) 
        {
            ApiResponse<Usuarios?> response;

            try
            {
                Usuarios? _usuario = await this._usuarioRepository.GetUsuario(usuario);
                response = new ApiResponse<Usuarios?>(_usuario );
                response.Message = "Consulta realizada correctamente";
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                response = new ApiResponse<Usuarios?>(null, ex.Message, false);
                response.Message = ApiResponse<string>.Error;
                response.Succeeded = false;
            }

            return Ok(response);
        }

        [HttpPost("createUsuario")]
        public async Task<ActionResult<Usuarios>> CreateUsuario(UsuarioRequestDto usuario)
        {
            ApiResponse<Usuarios> response;

            try
            {
                Usuarios _usuarios = await this._usuarioRepository.CreateUsuario(usuario);
                response = new ApiResponse<Usuarios>(_usuarios);
                response.Message = "Usuario creado con exito";
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                response = new ApiResponse<Usuarios>(null, ex.Message, false);
                response.Message = ApiResponse<string>.Error;
                response.Succeeded = false;
            }
            return Ok(response);

        }

        [Authorize]
        [HttpPut("updateUsuario")]

        public async Task<ActionResult<Usuarios>> UpdateUsuario(UsuarioRequestDto usuario)
        {
            ApiResponse<Usuarios> response;

            try
            {
                Usuarios _usuarios = await this._usuarioRepository.UpdateUsuario(usuario);
                response = new ApiResponse<Usuarios>(_usuarios);
                response.Message = "Usuario actualizado con exito";
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                response = new ApiResponse<Usuarios>(null, ex.Message, false);
                response.Message = ApiResponse<string>.Error;
                response.Succeeded = false;
            }
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("deleteUsuario")]
        public async Task<ActionResult<int>> DeleteUsuario(Usuarios usuario)
        {
             ApiResponse<int> response;

            try
            {
                int id = await this._usuarioRepository.DeleteUsuario(usuario);
                response = new ApiResponse<int>(id);
                response.Message = "Usuario actualizado con exito";
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                response = new ApiResponse<int>(0, ex.Message, false);
                response.Message = ApiResponse<string>.Error;
                response.Succeeded = false;
            }
            return Ok(response);
        }

    }
}