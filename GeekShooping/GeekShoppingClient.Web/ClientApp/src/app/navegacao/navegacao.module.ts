import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';



@NgModule({
    declarations: [    
     NavMenuComponent,
     HomeComponent
    ],

    imports: [

        CommonModule,        
        RouterModule,    
        

        // CollapseModule.forRoot(),
        // ToastrModule.forRoot({
        // timeOut: 5000,
        // extendedTimeOut: 5000,
        // positionClass: 'toast-bottom-full-width',
        // preventDuplicates: true,
        // closeButton: true
        //})
    
      ],

      exports:[
        NavMenuComponent,
        HomeComponent
      ],

      providers: [
        
      ]
    })

    export class NavegacaoModule { }