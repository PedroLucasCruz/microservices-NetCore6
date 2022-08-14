import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { Observable } from 'rxjs';
import { Produto } from '../models/produto.model';
import { ProdutoService } from '../services/produto/produto.service';

@Injectable({ providedIn: 'root' })

export class ProdutoResolver implements Resolve<Produto> {
    
  constructor(private _service: ProdutoService) {}
  
  resolve(): Observable<Produto> {
    return this._service.getAll();
  }
  
  // resolveGetById(): Observable<Produto>{
  //   return this._service.getById(1);    
  // }
  
} 