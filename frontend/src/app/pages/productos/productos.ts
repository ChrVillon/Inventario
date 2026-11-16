import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { forkJoin } from 'rxjs';
import { Inventario } from '../../services/inventario';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-productos',
  standalone: true,
  imports: [CommonModule, FormsModule],  
  templateUrl: './productos.html',
  styleUrl: './productos.css'
})
export class ProductosComponent implements OnInit {

  productos: any[] = [];
  proveedores: any[] = [];
  existencias: any[] = [];

  inventarioTabla: any[] = [];
  mensaje: string = '';

  mostrarFormulario: boolean = false;

  nuevaExistencia: any = {
    idProducto: 0,
    idProveedor: 0,
    numeroLote: '',
    precio: 0,
    cantidad: 0
  };

  constructor(private inventarioService: Inventario) {}

  ngOnInit(): void {
    this.cargarInventario();
  }

  cargarInventario(): void {
    forkJoin({
      productos: this.inventarioService.obtenerProductos(),
      proveedores: this.inventarioService.obtenerProveedores(),
      existencias: this.inventarioService.obtenerExistencias()
    }).subscribe({
      next: (respuesta) => {
        this.productos = respuesta.productos;
        this.proveedores = respuesta.proveedores;
        this.existencias = respuesta.existencias;

        this.construirTabla();
      },
      error: (error) => {
        console.error(error);
        this.mensaje = 'No se pudo cargar el inventario.';
      }
    });
  }

  construirTabla(): void {
    this.inventarioTabla = this.productos.map(producto => ({
      ...producto,

      existenciasPorProveedor: this.proveedores.map(proveedor => ({
        ...proveedor,

        registros: this.existencias.filter(existencia =>
          existencia.idProducto === producto.idProducto &&
          existencia.idProveedor === proveedor.idProveedor
        )
      }))
    }));
  }

  registrarExistencia(): void {
    this.inventarioService.crearExistencia(this.nuevaExistencia)
      .subscribe({
        next: () => {
          this.mensaje = 'Existencia registrada correctamente.';

          this.mostrarFormulario = false;

          this.nuevaExistencia = {
            idProducto: 0,
            idProveedor: 0,
            numeroLote: '',
            precio: 0,
            cantidad: 0
          };

          this.cargarInventario();
        },
        error: (error) => {
          console.error(error);
          this.mensaje = 'No se pudo registrar la existencia.';
        }
      });
  }
  
}