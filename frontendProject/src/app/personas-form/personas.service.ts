import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ApiResponse, PagedList, Pagination } from '../Dto/ApiResponse';
import { Personas, PersonasRequestDto, PersonasRequestSearchDto } from '../Dto/Personas/Personas';

@Injectable({
  providedIn: 'root'
})
export class PersonasService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  createPersona(persona: Personas): Observable<ApiResponse<Personas>> {
    return this.http.post<ApiResponse<Personas>>(`${this.apiUrl}/Persona/createPersona`, persona);
  }

  getPersonas(pagination: Pagination<PersonasRequestSearchDto>): Observable<ApiResponse<PagedList<Personas>>> {
    return this.http.post<ApiResponse<PagedList<Personas>>>(`${this.apiUrl}/Persona/getPersonas`,  pagination );
  }

  updatePersona(persona: PersonasRequestDto): Observable<ApiResponse<Personas>> {
    return this.http.put<ApiResponse<Personas>>(`${this.apiUrl}/Persona/updatePersona`,  persona);
  }

  getPersonaById(persona: Personas): Observable<ApiResponse<Personas>> {
    return this.http.post<ApiResponse<Personas>>(`${this.apiUrl}/Persona/getPersona`, persona);
  }
}
