using api.src.Data;
using api.src.Dto;
using api.src.Dto.Personas;
using api.src.Entities;
using api.src.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace api.src.Repository
{
    public class PersonaRepository: IPersonasRepository
    {
        private readonly SqlServerContext _dbContext;

        public PersonaRepository(SqlServerContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Persona> CreatePersona(PersonaRequestDto persona)
        {
            Persona _persona = new Persona();
            _persona.Nombres = persona.Nombres;
            _persona.Apellidos = persona.Apellidos;
            _persona.Email = persona.Email;
            _persona.NumeroIdentificacion = persona.NumeroIdentificacion;
            _persona.TipoIdentificacion = persona.TipoIdentificacion;

            await this._dbContext.Personas.AddAsync(_persona);
            await this._dbContext.SaveChangesAsync();
            return _persona;
        }

        public async Task<int> DeletePersona(Persona persona)
        {
            int id = persona.Identificador;
            this._dbContext.Personas.Remove(persona);
            await this._dbContext.SaveChangesAsync();
            return id;
        }

        public async Task<Persona?> GetPersona(PersonaRequestDto persona)
        {
            return await this._dbContext.Personas
                        .FirstOrDefaultAsync(P => P.NumeroIdentificacion == persona.NumeroIdentificacion);
        }

        public async Task<PagedList<Persona>> GetPersonas(Pagination<PersonasRequestDto> pagination)
        {
            int pageNumber = pagination.PageNumber;
            int pageSize = pagination.PageSize;

            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("PageNumber y PageSize deben ser mayores a 0.");
            }

            PersonasRequestDto personas = pagination.Search;
            string nombre = personas.Nombres;
            string apellidos = personas.Apellidos;
            string numeroIdentificacion = personas.NumeroIdentificacion;

            // Lista para almacenar los resultados
            List<Persona> personasResult = new List<Persona>();
            int totalCount = 0;

            using (var connection = _dbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "EXEC GetPersonasPaginated @PageNumber, @PageSize, @Nombre, @Apellido, @NumeroIdentificacion";
                    command.CommandType = CommandType.Text;

                    // Agregar parámetros
                    var pageNumberParam = command.CreateParameter();
                    pageNumberParam.ParameterName = "@PageNumber";
                    pageNumberParam.Value = pageNumber;
                    command.Parameters.Add(pageNumberParam);

                    var pageSizeParam = command.CreateParameter();
                    pageSizeParam.ParameterName = "@PageSize";
                    pageSizeParam.Value = pageSize;
                    command.Parameters.Add(pageSizeParam);

                    var nombreParam = command.CreateParameter();
                    nombreParam.ParameterName = "@Nombre";
                    nombreParam.Value = nombre ?? (object)DBNull.Value;
                    command.Parameters.Add(nombreParam);

                    var apellidoParam = command.CreateParameter();
                    apellidoParam.ParameterName = "@Apellido";
                    apellidoParam.Value = apellidos ?? (object)DBNull.Value;
                    command.Parameters.Add(apellidoParam);

                    var numeroIdentificacionParam = command.CreateParameter();
                    numeroIdentificacionParam.ParameterName = "@NumeroIdentificacion";
                    numeroIdentificacionParam.Value = numeroIdentificacion ?? (object)DBNull.Value;
                    command.Parameters.Add(numeroIdentificacionParam);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Leer el primer conjunto de resultados (Personas paginadas)
                        while (await reader.ReadAsync())
                        {
                            var persona = new Persona
                            {
                                Identificador = reader.GetInt32(0),
                                Nombres = reader.GetString(1),
                                Apellidos = reader.GetString(2),
                                NumeroIdentificacion = reader.GetString(3),
                                TipoIdentificacion = reader.GetString(4),
                                Email = reader.GetString(5),
                                FechaCreacion = reader.GetDateTime(6),
                                NumeroIdentificacionCompleto = reader.GetString(7),
                                NombreCompleto = reader.GetString(8),
                            };

                            personasResult.Add(persona);
                        }

                        // Moverse al siguiente conjunto de resultados (TotalCount)
                        if (await reader.NextResultAsync() && await reader.ReadAsync())
                        {
                            totalCount = reader.GetInt32(0);
                        }
                    }
                }
            }

            // Retornar los resultados como un PagedList
            return new PagedList<Persona>(personasResult, totalCount, pageNumber, pageSize);
        }


        public async Task<Persona> UpdatePersona(PersonaRequestDto persona)
        {
            Persona _persona = new Persona();
            _persona.Apellidos = persona.Apellidos;
            _persona.Nombres = persona.Nombres;
            _persona.Email = persona.Email;
            _persona.TipoIdentificacion = persona.TipoIdentificacion;
            _persona.NumeroIdentificacion = persona.NumeroIdentificacion;

            this._dbContext.Personas.Update(_persona);
            await _dbContext.SaveChangesAsync();
            return _persona;
        }
    }
}