import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, pipe } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";
import { BaseService } from '../base.service';
import { Produto } from '../../models/produto.model';

export class ProdutoService extends BaseService{

constructor(private http: HttpClient) { super() }


getAll() {
    let response = this.http
    .get<Produto[]>(this.UrlServiceV1 + 'GetAll')
    .pipe(
      map(this.extractData),
      catchError(this.serviceError));    
    return response;
  }

  getById(Id: number){
    let response = this.http
    .post<Produto[]>(this.UrlServiceV1 + 'GetById', Id)
    .pipe(
      map(this.extractData),
      catchError(this.serviceError));    
    return response;
  }

}