import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PersonasFormComponent } from './personas-form/personas-form.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { PersonasService } from './personas-form/personas.service';
import { UsuariosService } from './login-form/usuarios.service';
import { NotificationComponent } from './notification/notification.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { RegisterUserService } from './register-user/register-user.service';


@NgModule({
  declarations: [
    AppComponent,
    PersonasFormComponent,
    LoginFormComponent,
    NotificationComponent,
    RegisterUserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule
  ],
  providers: [PersonasService, UsuariosService, RegisterUserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
