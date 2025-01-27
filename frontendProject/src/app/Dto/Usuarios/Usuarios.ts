export interface Usuarios {
  identificador: number;
  persona:       number;
  usuario:       string;
  pass:          string;
  fechaCreacion: Date;
}


export interface UsuarioRequestDto {
  persona:  number;
  usuario:  string;
  pass:     string;
}

export interface LoginResponseDto {
  token:    string;
  userId:   number;
  username: string;
}
