export interface Personas {
  identificador:                number;
  nombres:                      string;
  apellidos:                    string;
  numeroIdentificacion:         string;
  email:                        string;
  tipoIdentificacion:           string;
}


export interface PersonasRequestDto {
  identificador:                number;
  nombres:                      string;
  apellidos:                    string;
  numeroIdentificacion:         string;
  email:                        string;
  tipoIdentificacion:           string;
}


export interface PersonasRequestSearchDto {
  Nombres:                      string;
  Apellidos:                    string;
  NumeroIdentificacion:         string;
}



