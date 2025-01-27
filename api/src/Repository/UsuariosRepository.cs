
using api.src.Data;
using api.src.Dto;
using api.src.Dto.Usuarios;
using api.src.Entities;
using api.src.interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.src.Repository
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly SqlServerContext _dbContext;

        public UsuariosRepository(SqlServerContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Usuarios> CreateUsuario(UsuarioRequestDto usuario)
        {
            Usuarios _usuarios = new Usuarios();
            _usuarios.Usuario = usuario.Usuario;
            _usuarios.Persona = usuario.Persona;
            _usuarios.Pass = usuario.Pass;

            await this._dbContext.AddAsync(_usuarios);
            await this._dbContext.SaveChangesAsync();
            return _usuarios;
        }

        public async Task<int> DeleteUsuario(Usuarios usuario)
        {
            int identificador = usuario.Identificador;
            this._dbContext.Usuarios.Remove(usuario);
            await this._dbContext.SaveChangesAsync();
            return identificador;
        }

        public async Task<Usuarios?> GetUsuario(UsuarioRequestDto usuario)
        {
            return await this._dbContext.Usuarios
                        .FirstOrDefaultAsync(U => U.Usuario == usuario.Usuario);
        }

        public async Task<Usuarios?> checkUsuarioExits(Usuarios usuario)
        {
            return await this._dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Usuario == usuario.Usuario && u.Pass == usuario.Pass );
        }

        public async Task<PagedList<Usuarios>> GetUsuarios(Pagination<string> pagination)
        {
            IQueryable<Usuarios> query = this._dbContext.Usuarios;
            if ( !string.IsNullOrEmpty(pagination.Search) ) {
                query = query.Where( 
                    usuario => usuario.Usuario.Contains(pagination.Search)
                );
            }
            int totalCount = await query.CountAsync();
            var pagedQuery = query.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);
            List<Usuarios> result = await pagedQuery.ToListAsync(); 

            return new PagedList<Usuarios>(result,totalCount,pagination.PageNumber,pagination.PageSize);
        }

        public async Task<Usuarios> UpdateUsuario(UsuarioRequestDto usuario)
        {
            Usuarios _usuarios = new Usuarios();
            _usuarios.Usuario = usuario.Usuario;
            _usuarios.Persona = usuario.Persona;
            _usuarios.Pass = usuario.Pass;
            
            this._dbContext.Usuarios.Update(_usuarios);
            await this._dbContext.SaveChangesAsync();
            return _usuarios;
        }
    }
}