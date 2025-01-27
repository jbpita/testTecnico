

using api.src.Dto;
using api.src.Dto.Usuarios;
using api.src.Entities;

namespace api.src.interfaces
{
    public interface IUsuariosRepository
    {
        Task<PagedList<Usuarios>> GetUsuarios(Pagination<string> pagination);
        Task<Usuarios?> GetUsuario(UsuarioRequestDto usuario);
        Task<Usuarios> CreateUsuario(UsuarioRequestDto usuario);
        Task<Usuarios> UpdateUsuario(UsuarioRequestDto usuario);
        Task<int> DeleteUsuario(Usuarios usuario);
        Task<Usuarios?> checkUsuarioExits(Usuarios usuario);
    }
}