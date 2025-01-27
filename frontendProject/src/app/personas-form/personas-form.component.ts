import { Component, OnInit, ViewChild } from '@angular/core';
import { PersonasService } from './personas.service';
import { Personas, PersonasRequestDto, PersonasRequestSearchDto } from '../Dto/Personas/Personas';
import { Pagination } from '../Dto/ApiResponse';
import { NotificationComponent } from '../notification/notification.component';
import { Router } from '@angular/router';
import { UsuariosService } from '../login-form/usuarios.service';
import { Usuarios } from '../Dto/Usuarios/Usuarios';

@Component({
  selector: 'app-personas-form',
  templateUrl: './personas-form.component.html',
  styleUrls: ['./personas-form.component.css']
})
export class PersonasFormComponent implements OnInit{
  @ViewChild('notification')
  notification: NotificationComponent = new NotificationComponent;

  persona: Personas | PersonasRequestDto = {
    identificador: 0,
    apellidos: '',
    numeroIdentificacion: '',
    tipoIdentificacion: '',
    email: '',
    nombres: '',
  };


  usuario: Usuarios = {
    usuario: '',
    pass: '',
    persona: 0,
    fechaCreacion: new Date(),
    identificador: 0,
  };



  personas: Personas[] = []
  pagination: Pagination<PersonasRequestSearchDto> = {
    PageSize: 10,
    PageNumber: 1,
    Search: {
      Nombres: '',
      Apellidos: '',
      NumeroIdentificacion: '',
    },
  }

  isLoggedIn: boolean = false;

  constructor(
    private personasService: PersonasService,
    private usuariosService: UsuariosService,
    private router: Router
  ) {}

  ngOnInit() {
    this.isLoggedIn = !localStorage.getItem('token');
    if (this.isLoggedIn) {
      console.log('entre')
      this.getPersonas();
      return;
    }
  }

  onSubmit() {
    if (this.isLoggedIn) {
      this.personasService.updatePersona(this.persona as PersonasRequestDto).subscribe(response => {
        this.notification.showNotification('Persona actualizada exitosamente');
      });
    } else {
      this.personasService.createPersona(this.persona as Personas).subscribe(response => {
        this.getPersonas();
        this.notification.showNotification('Persona creada exitosamente');
      });
    }
  }

  onSubmitUser() {
    this.usuariosService.updateUser(this.usuario).subscribe(response => {
      this.notification.showNotification('Usuario actualizado exitosamente');
    });
  }

  getPersonas() {
    this.personasService.getPersonas(this.pagination).subscribe(response => {
      this.personas = response.data.items;
    });
  }

  onSearchChange() {
    this.pagination.PageNumber = 1;
    this.getPersonas();
  }

  previousPage() {
    if (this.pagination.PageNumber > 1) {
      this.pagination.PageNumber--;
      this.getPersonas();
    }
  }

  nextPage() {
    this.pagination.PageNumber++;
    this.getPersonas();
  }

  registerUser(id: number) {
    this.router.navigate(['/register-user', id]);
  }

  editPersona(persona: Personas) {
    this.personasService.getPersonaById(persona).subscribe(response => {
      this.persona = response.data;
      this.usuario.persona = response.data.identificador;
    });
  }


  login() {
    this.router.navigate(['/login'])
  }

  logout() {
    localStorage.removeItem('token');
    this.isLoggedIn = false;
    this.router.navigate(['/login']);
  }

}
