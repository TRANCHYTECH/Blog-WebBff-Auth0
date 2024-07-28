import { HttpInterceptorFn } from '@angular/common/http';

export const AskApiHttpInterceptor: HttpInterceptorFn = (req, next) => {
  const headers = req.headers
    .set('Authorization', 'Bearer <replace by predefined token>')
    .set('Access-Control-Allow-Origin', '*');

  req = req.clone({
    url: req.url,
    headers,
    withCredentials: true,
  });
  return next(req);
};
