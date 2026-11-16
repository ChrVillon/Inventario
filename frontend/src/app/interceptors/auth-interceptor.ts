import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const token = localStorage.getItem('token');

  if (token) {
    const nuevaSolicitud = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });

    return next(nuevaSolicitud);
  }

  return next(req);
};