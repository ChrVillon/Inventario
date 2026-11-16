import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Inventario {

    private apiUrl = 'http://localhost:5281/api';

    constructor(private http: HttpClient) {}

    obtenerProductos(): Observable<any[]> {
        return this.http.get<any[]>(`${this.apiUrl}/Producto`);
    }

    obtenerProveedores(): Observable<any[]> {
        return this.http.get<any[]>(`${this.apiUrl}/Proveedor`);
    }

    obtenerExistencias(): Observable<any[]> {
        return this.http.get<any[]>(`${this.apiUrl}/ExistenciaProducto`);
    }

    crearExistencia(existencia: any): Observable<any> {
        return this.http.post(
            `${this.apiUrl}/ExistenciaProducto`,
            existencia
        );
    }

    actualizarExistencia(id: number, existencia: any): Observable<any> {
        return this.http.put(
            `${this.apiUrl}/ExistenciaProducto/${id}`,
            existencia
        );    
    }

    eliminarExistencia(id: number): Observable<any> {
        return this.http.delete(
            `${this.apiUrl}/ExistenciaProducto/${id}`
        );
    }
}