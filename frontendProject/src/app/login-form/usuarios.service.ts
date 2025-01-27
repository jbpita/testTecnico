import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoginResponseDto, UsuarioRequestDto, Usuarios } from '../Dto/Usuarios/Usuarios';
import { ApiResponse } from '../Dto/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class UsuariosService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  login(usuario: Usuarios): Observable<ApiResponse<LoginResponseDto>> {
    return this.http.post<ApiResponse<LoginResponseDto>>(`${this.apiUrl}/Usuario/login`, usuario);
  }

  updateUser(usuario: UsuarioRequestDto): Observable<ApiResponse<Usuarios>> {
    return this.http.post<ApiResponse<Usuarios>>(`${this.apiUrl}/Usuario/updateUsuario`, usuario);
  }
}
