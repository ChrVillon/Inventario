import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Auth } from '../../services/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

  correo: string = '';
  clave: string = '';
  mensaje: string = '';

  constructor(private authService: Auth) {}

  iniciarSesion(): void {
    this.authService.iniciarSesion(this.correo, this.clave)
      .subscribe({
        next: (respuesta) => {
          this.authService.guardarToken(respuesta.token);
          this.mensaje = 'Inicio de sesión exitoso.';
        },
        error: (error) => {
          this.mensaje = error.error?.mensaje || 'Error al iniciar sesión.';
        }
      });
  }
}