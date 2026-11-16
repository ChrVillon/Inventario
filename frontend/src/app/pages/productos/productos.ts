import { Component, OnInit, ChangeDetectorRef } from '@angular/core';  import { CommonModule } from '@angular/common';
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

    editando: boolean = false;

    idExistenciaEditando: number = 0;

  constructor(
    private inventarioService: Inventario,
    private cdr: ChangeDetectorRef
  ) {}
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

          this.cdr.detectChanges();

          console.log('Tabla construida:', this.inventarioTabla);
        },
        error: (error) => {
          console.error('Error al cargar inventario:', error);
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

      const operacion = this.editando
        ? this.inventarioService.actualizarExistencia(
            this.idExistenciaEditando,
            this.nuevaExistencia
          )
        : this.inventarioService.crearExistencia(
            this.nuevaExistencia
          );

      operacion.subscribe({
        next: () => {
          this.mensaje = this.editando
            ? 'Existencia actualizada correctamente.'
            : 'Existencia registrada correctamente.';

          this.mostrarFormulario = false;
          this.editando = false;
          this.idExistenciaEditando = 0;

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
          console.error('Error al actualizar/guardar:', error);
          console.log('Respuesta del backend:', error.error);
          this.mensaje = error.error?.mensaje || 'No se pudo guardar la existencia.';
        }
      });
    }

    editarExistencia(existencia: any): void {
      this.editando = true;
      this.idExistenciaEditando = existencia.idExistencia;

      this.nuevaExistencia = {
        idExistencia: existencia.idExistencia,
        idProducto: existencia.idProducto,
        idProveedor: existencia.idProveedor,
        numeroLote: existencia.numeroLote,
        precio: existencia.precio,
        cantidad: existencia.cantidad
      };

      this.mostrarFormulario = true;
    }

    eliminarExistencia(id: number): void {
      if (!confirm('¿Estás seguro de eliminar esta existencia?')) {
        return;
      }

      this.inventarioService.eliminarExistencia(id).subscribe({
        next: () => {
          this.mensaje = 'Existencia eliminada correctamente.';
          this.cargarInventario();
        },
        error: (error) => {
          console.error(error);
          this.mensaje = 'No se pudo eliminar la existencia.';
        }
      });
    }
    
  }