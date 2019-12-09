import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material';

@Injectable()
export class SnackbarInterceptor implements HttpInterceptor {

    constructor(private snackBar: MatSnackBar) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if ((this.isDataManipulatingRequest(req))) {
            return next.handle(req).pipe(tap(event => {
                if (event instanceof HttpResponse) {
                    if (event.status === 201 || event.status === 204) {
                        this.snackBar.open('Operacja powiodła się');
                    }
                }
            }), catchError((error: HttpErrorResponse) => {
                if (error.status === 400 || error.status === 404 || error.status === 500) {
                    this.snackBar.open('Coś poszło nie tak');
                }
                return throwError(error);
            }));
        } else {
            return next.handle(req);
        }
    }

    private isDataManipulatingRequest(req: HttpRequest<any>): boolean {
        return req.method === 'POST' || req.method === 'DELETE' || req.method === 'PUT';
    }
}
