import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UsuarioRequestDto } from '../Dto/Usuarios/Usuarios';
import { RegisterUserService } from './register-user.service';
import { NotificationComponent } from '../notification/notification.component';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {
  @ViewChild('notification')
  notification: NotificationComponent = new NotificationComponent;

  rptPassword: string = '';
  usuario: UsuarioRequestDto = {
    usuario: '',
    pass:    '',
    persona: 0,
  };

  constructor(private route: ActivatedRoute, private usuariosService: RegisterUserService) {}

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.usuario.persona = params['id'];
    });
  }

  checkPassword(): boolean {
    return this.rptPassword === this.usuario.pass;
  }

  onSubmit() {
    if ( !this.checkPassword() ) {
      this.notification.showNotification('Las contraseñas no coinciden');
      return;
    }
    this.usuariosService.createUser(this.usuario).subscribe(response => {
      this.notification.showNotification(response.message);
    });
  }
}
