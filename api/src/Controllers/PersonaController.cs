using api.src.Dto;
using api.src.Dto.Personas;
using api.src.Entities;
using api.src.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.src.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonaController: ControllerBase
    {
        private readonly IPersonasRepository _personasRepository;

        public PersonaController(IPersonasRepository personasRepository)
        {
            this._personasRepository = personasRepository;
        }

        [HttpPost("getPersonas")]
        public async Task<ActionResult<PagedList<Persona>>> GetUsuaGetPersonas(Pagination<PersonasRequestDto> pagination)
        {
            ApiResponse<PagedList<Persona>> response;
            
            try
            {
                PagedList<Persona> pageList = await this._personasRepository.GetPersonas(pagination);
                response = new ApiResponse<PagedList<Persona>>(pageList);
                response.Message = "Consulta realizada correctamente";
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                
                response = new ApiResponse<PagedList<Persona>>(null, ex.Message, false);
                response.Message = ApiResponse<string>.Error;
                response.Succeeded = false;
            }
            
            return Ok(response);
        }


        [HttpPost("getPersona")]
        public async Task<ActionResult<Persona?>> GetPersona(PersonaRequestDto persona) 
        {
            ApiResponse<Persona?> response;

            try
            {
                Persona? _persona = await this._personasRepository.GetPersona(persona);
                response = new ApiResponse<Persona?>(_persona);
                response.Message = "Consulta realizada correctamente";
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                response = new ApiResponse<Persona?>(null, ex.Message, false);
                response.Message = ApiResponse<string>.Error;
                response.Succeeded = false;
            }

            return Ok(response);
        }

        [HttpPost("createPersona")]
        public async Task<ActionResult<Persona>> CreatePersona(PersonaRequestDto persona)
        {
            ApiResponse<Persona> response;

            try
            {
                Persona _persona = await this._personasRepository.CreatePersona(persona);
                response = new ApiResponse<Persona>(_persona);
                response.Message = "Persona creado con exito";
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response = new ApiResponse<Persona>(null, ex.Message, false);
                response.Message = ApiResponse<string>.Error;
                response.Succeeded = false;
            }
            return Ok(response);

        }

        [Authorize]
        [HttpPut("updatePersona")]
        public async Task<ActionResult<Persona>> UpdatePersona(PersonaRequestDto persona)
        {
            ApiResponse<Persona> response;

            try
            {
                Persona _persona = await this._personasRepository.UpdatePersona(persona);
                response = new ApiResponse<Persona>(_persona);
                response.Message = "Persona actualizado con exito";
                response.Succeeded = true;
            }
            catch (Exception ex)
            {
                response = new ApiResponse<Persona>(null, ex.Message, false);
                response.Message = ApiResponse<string>.Error;
                response.Succeeded = false;
            }
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("deletePersona")]
        public async Task<ActionResult<int>> DeletePersona(Persona persona)
        {
             ApiResponse<int> response;

            try
            {
                int id = await this._personasRepository.DeletePersona(persona);
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