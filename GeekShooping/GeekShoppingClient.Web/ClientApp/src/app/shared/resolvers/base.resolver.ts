import { Injectable, Type } from '@angular/core';
import { Resolve } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })

export abstract class BaseResolve implements Resolve<Type<any>> {

    resolve(): Type<any> | Observable<Type<any>> | Promise<Type<any>> {      
       return Type;
    }

    getAll(): Type<any> | Observable<Type<any>> | Promise<Type<any>> {      
        return Type;
    }

    getById(): Type<any> | Observable<Type<any>> | Promise<Type<any>> {      
        return Type;
    }
  

  // resolveGetById(): Observable<Produto>{
  //   return this._service.getById(1);    
  // }
  
} 