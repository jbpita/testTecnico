import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PersonasFormComponent } from './personas-form/personas-form.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { RegisterUserComponent } from './register-user/register-user.component';

const routes: Routes = [
  { path: 'personas', component: PersonasFormComponent },
  { path: 'login', component: LoginFormComponent },
  { path: 'register-user/:id', component: RegisterUserComponent },
  { path: '', redirectTo: '/personas', pathMatch: 'full' }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
