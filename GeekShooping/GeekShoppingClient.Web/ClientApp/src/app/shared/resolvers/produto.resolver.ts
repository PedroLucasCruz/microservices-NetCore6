import { Injectable, Type } from '@angular/core';
import { Resolve } from '@angular/router';
import { Observable } from 'rxjs';
import { Produto } from '../models/produto.model';
import { ProdutoService } from '../services/produto/produto.service';
import { BaseResolve } from './base.resolver';

@Injectable({ providedIn: 'root' })

export class ProdutoResolver extends BaseResolve {
    
 constructor(private _service: ProdutoService) { super() }
  
 
resolve(): Type<any> | Observable<Type<any>> | Promise<Type<any>> {
  return this._service.getAll();
}

getAll(): Type<any> | Observable<Type<any>> | Promise<Type<any>> {
  return this._service.getAll();
}

getById(): Type<any> | Observable<Type<any>> | Promise<Type<any>> {
  return this._service.getById(1);
}



  // resolve(): Observable<Produto> {
  //   return this._service.getAll();
  // }

  // resolveGetById(): Observable<Produto>{
  //   return this._service.getById(1);    
  // }
  
} 