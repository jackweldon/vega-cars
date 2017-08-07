import * as Raven from 'raven-js';
import { AppErrorHandler } from './app.error-handler';
import { ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { ToastyModule } from 'ng2-toasty';
import { RouterModule } from '@angular/router';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { VehicleService } from './Services/vehicle.service';
import { VehicleViewComponent } from './components/vehicle-view/vehicle-view.component'
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component'
import { PaginationComponent } from './components/shared/pagination-component';
import { PhotoService } from "./Services/photo.service";

Raven
    .config('https://3713a77cd3424259a34ee6336bd48d90@sentry.io/194241')
    .install();

export const sharedConfig: NgModule = {
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        VehicleFormComponent,
        VehicleListComponent,
        VehicleViewComponent,
        PaginationComponent,
    ],
    imports: [
        RouterModule.forRoot([
            { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'vehicle/new', component: VehicleFormComponent },
            { path: 'vehicles/edit/:id', component: VehicleFormComponent },
            { path: 'vehicles/:id', component: VehicleViewComponent },
            { path: 'vehicles', component: VehicleListComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        FormsModule,
        ToastyModule.forRoot(),
    ],
    providers: [
        { provide: ErrorHandler, useClass: AppErrorHandler },
        VehicleService, PhotoService
    ]
};
