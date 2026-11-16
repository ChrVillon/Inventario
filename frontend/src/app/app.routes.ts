import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { ProductosComponent } from './pages/productos/productos';

export const routes: Routes = [
  {
    path: '',
    component: Login
  },
  {
    path: 'productos',
    component: ProductosComponent
  }
];