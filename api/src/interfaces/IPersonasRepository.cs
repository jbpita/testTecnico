
using api.src.Dto;
using api.src.Dto.Personas;
using api.src.Entities;

namespace api.src.interfaces
{
    public interface IPersonasRepository
    {
        Task<PagedList<Persona>> GetPersonas(Pagination<PersonasRequestDto> pagination);
        Task<Persona?> GetPersona(PersonaRequestDto persona);
        Task<Persona> CreatePersona(PersonaRequestDto persona);
        Task<Persona> UpdatePersona(PersonaRequestDto persona);
        Task<int> DeletePersona(Persona persona);
    }
}

