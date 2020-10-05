import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { NavigationExtras, Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';

import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toatr: ToastrService ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(err => {
        if (err) {
          switch (err.status) {
            case 400:
              if (err.error.errors) {
                const modalStateErrors = [];
                for (const key in err.error.errors){
                  if (err.error.errors[key]) {
                    modalStateErrors.push(err.error.errors[key])
                  }
                }
                throw modalStateErrors.flat();
              } else {
                this.toatr.error(err.statusText, err.status);
              }
              break;
            case 401:
              console.log(err);
              this.toatr.error(err.statusText, err.status);
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              const navigationExtras: NavigationExtras = { state: {error: err.error }}
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              this.toatr.error('Something unexpected went wrong');
              console.log(err);
              break;
          }
        }
        return throwError(err);
      })
    );
  }
}
