import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';


import { AppComponent } from './app.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AppRoutingModule } from './app-routing.module';
import { NavegacaoModule } from './navegacao/navegacao.module';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ErrorInterceptor } from './services/error-interceptor.service';

export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
];

@NgModule({

  declarations: [
    AppComponent,   
    
    CounterComponent,
    FetchDataComponent
  ],

  imports: [
    //BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    NavegacaoModule ,    
    BrowserModule,
    CommonModule,
    BrowserAnimationsModule
    // RouterModule.forRoot([
      
    //   { path: '', component: HomeComponent, pathMatch: 'full' },
      
    // ])
  ],
  providers: [ httpInterceptorProviders ],
  bootstrap: [AppComponent]  
})
export class AppModule { }
