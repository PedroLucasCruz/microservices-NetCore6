import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './navegacao/home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';


const routes: Routes = [ 

    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'counter', component: CounterComponent },
    { path: 'fetch-data', component: FetchDataComponent}
    // {
    //    // path: '', redirectTo: /
    // },

   // {path: '**', component: componenteDeRotaNãoEncontrada}

];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
    // bootstrap: [RouterModule]
})
export class AppRoutingModule  { }
