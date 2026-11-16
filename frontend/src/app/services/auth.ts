import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Auth {

  private apiUrl = 'http://localhost:5281/api/Auth';

  constructor(private http: HttpClient) {}

  iniciarSesion(correo: string, clave: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, {
      correo: correo,
      clave: clave
    });
  }

  guardarToken(token: string): void {
    localStorage.setItem('token', token);
  }
}