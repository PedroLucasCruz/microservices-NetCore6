import { HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { throwError } from "rxjs";
import { environment } from 'src/environments/environment';
import { LocalStorageUtils } from 'src/app/utils/local-storage';
import { SessionStorageUtils } from 'src/app/utils/session-storage';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})

export abstract class BaseService {

    protected UrlServiceV1: string = environment.baseApiUrl;
   
    
    public LocalStorage = new LocalStorageUtils();

    protected ObterHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
    }

    protected ObterAuthHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.LocalStorage.obterTokenUsuario()}`
            })
        };
    }

    protected returnData(response: any){
        return response;
    }

    protected extractData(response: any) {
        return response.data || {};
    }

    protected serviceError(response: Response | any) {
        let customError: string[] = [];
        let customResponse =  { error: { errors: [''] }} 

        if (response instanceof HttpErrorResponse) {

          if (response.statusText === "Unknown Error") {
            console.log(response)
                customError.push("Ocorreu um erro desconhecido");
                response.error.errors = customError;
            }
        }
        if (response.status === 500) {
            customError.push("Ocorreu um erro no processamento, tente novamente mais tarde ou contate o nosso suporte.");
            
            // Erros do tipo 500 não possuem uma lista de erros
            // A lista de erros do HttpErrorResponse é readonly                
            customResponse.error.errors = customError; // = customError;
            return throwError(customResponse);
        }

        console.error(response);
        return throwError(response);
    }
}