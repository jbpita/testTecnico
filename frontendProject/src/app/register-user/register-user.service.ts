import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { UsuarioRequestDto, Usuarios } from '../Dto/Usuarios/Usuarios';
import { ApiResponse } from '../Dto/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class RegisterUserService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  createUser(user: UsuarioRequestDto): Observable<ApiResponse<Usuarios>> {
    return this.http.post<ApiResponse<Usuarios>>(`${this.apiUrl}/Usuario/createUsuario`, user);
  }

}
