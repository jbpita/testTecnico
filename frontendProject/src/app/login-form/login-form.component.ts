import { Component, ViewChild } from '@angular/core';
import { UsuariosService } from './usuarios.service';
import { Usuarios } from '../Dto/Usuarios/Usuarios';
import { Router } from '@angular/router';
import { NotificationComponent } from '../notification/notification.component';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent {
  @ViewChild('notification')
  notification: NotificationComponent = new NotificationComponent;

  usuario: Usuarios = {
    usuario: '',
    pass: '',
    persona: 0,
    fechaCreacion: new Date(),
    identificador: 0,
  }

  constructor(
    private usuariosService: UsuariosService,
    private router: Router
  ) {}

  onSubmit() {
    this.usuariosService.login(this.usuario).subscribe(response => {
      if (response.succeeded) {
        localStorage.setItem('token', response.data.token);
        this.notification.showNotification(response.message);
        setTimeout(() => this.router.navigate(['/personas']), 3000)
      } else {
        setTimeout(() => this.router.navigate(['/personas']), 3000)
      }
    });

  }
}
