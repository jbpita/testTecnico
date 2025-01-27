
namespace api.src.Dto.Personas
{
    public class PersonaRequestDto
    {
        public string NumeroIdentificacion { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TipoIdentificacion { get; set; } = string.Empty;

    }
}
