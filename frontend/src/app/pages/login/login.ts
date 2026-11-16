import { Component, ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Auth } from '../../services/auth';
import { Router } from '@angular/router';

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

  constructor(
    private authService: Auth,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  iniciarSesion(): void {
    this.authService.iniciarSesion(this.correo, this.clave)
      .subscribe({
      next: (respuesta) => {
        this.authService.guardarToken(respuesta.token);
        this.mensaje = 'Inicio de sesión exitoso.';
        this.router.navigate(['/productos']);
      },
      error: (error) => {
        console.log('Error del login:', error);
        this.mensaje = error.error?.mensaje || 'Credenciales inválidas.';
        this.cdr.detectChanges();
      }
    });
  }
}