import { 
    BrowserModule, 
    NgModule,
    RouterModule,
    HttpClientModule,
    ToastrModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule, 
    AngularMultiSelectModule,
    
   } from './index'


import { Error404Component } from './errors/404.component';
import { MovieService, 
    UploadService,
    ActorsListComponent,
    MovieThumbnailComponent, 
    UpdateMovieComponent,
    UpdateProducerComponent, 
    MovieListComponent, 
    MovieDetailsComponent, 
    MovieResolver,
    AddActorComponent,
    AddMovieComponent,
    AddNewActorComponent,
    AddNewProducerComponent,
  RedirectorInterceptor } from './movies/index';

    
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav/navbar.component';
import { FooterComponent } from './footer/footer-component';
import { CollapsibleWellComponent } from './common/collapsible-well.component';
import { appRoutes } from './routes';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    Error404Component,
    MovieListComponent,
    MovieThumbnailComponent,
    FooterComponent,
    MovieDetailsComponent,    
    CollapsibleWellComponent,
    AddNewActorComponent,
    ActorsListComponent,
    AddActorComponent,
    AddNewProducerComponent,
    UpdateProducerComponent,
    AddMovieComponent,
    UpdateMovieComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot(appRoutes, {useHash: true}),
    AngularMultiSelectModule
    
  ],
  providers: [
    MovieService,
    MovieResolver,
    UploadService,
    {provide: HTTP_INTERCEPTORS, useClass: RedirectorInterceptor, multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
