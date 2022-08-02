import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';

import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { finalize } from "rxjs/operators";

import { LocalStorageUtils } from 'src/app/utils/local-storage';
import { Router } from '@angular/router';
// import { LoaderService } from '../shared/loader/services/loader.service';
// import { JwtService } from './jwt/jwt.service';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(
        private router: Router,
        //  private loaderService: LoaderService,
        //  private jwtSerivce: JwtService, 
        ) { }

    localStorageUtil = new LocalStorageUtils();

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

       // this.loaderService.show();
       
        return next.handle(req).pipe(catchError(error => {

            if (error instanceof HttpErrorResponse) {

              //  finalize(() => this.loaderService.hide());

               // this.loaderService.hide();
                             
                if (error.status === 401) {
                    this.localStorageUtil.limparDadosLocaisUsuario();                    
                }
                
                if (error.status === 403) {
                    this.router.navigate(['/acesso-negado']);
                }             
            }
            return throwError(error);
        }
        ),
           // finalize(() => this.loaderService.hide())
        );
    }
}