import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [ 

    {path: '', redirectTo: '/home', pathMatch: 'full'},
    {path: '', redirectTo: '/home', pathMatch: 'full'},
    
    {
        path: ''
    },

   // {path: '**', component: componenteDeRotaNãoEncontrada}

];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
    // bootstrap: [RouterModule]
})
export class AppRoutingModule  { }
