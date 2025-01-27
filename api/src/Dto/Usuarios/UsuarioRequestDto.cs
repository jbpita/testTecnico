namespace api.src.Dto.Usuarios
{
    public class UsuarioRequestDto
    {
            public int Persona { get; set; }

            public string Usuario { get; set; } = null!;

            public string Pass { get; set; } = null!;

    }
}