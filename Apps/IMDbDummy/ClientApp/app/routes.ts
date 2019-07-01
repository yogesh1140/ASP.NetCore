import { Routes } from '@angular/router';
// import { 
//   EventsListComponent, 
//   EventDetailsComponent, 
//   CreateEventComponent, 
//   EventListResolver,
//   CreateSessionComponent,
//   EventResolver } from './events/index';
import { Error404Component } from './errors/404.component';
import { AppComponent } from './app.component';
import { MovieListComponent } from './movies/movie-list.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';
import { MovieResolver } from './movies/movie-resolver';
import { AddMovieComponent } from './movies/add-movie.component';

export const appRoutes: Routes = [
  // 
  { path:'movies/new', component: AddMovieComponent},
  { path:'movies', component: MovieListComponent},
  { path: 'movies/:id', component: MovieDetailsComponent, resolve: {movie: MovieResolver}},
  { path: '404,', component: Error404Component },
  { path:'', redirectTo:'/movies', pathMatch:'full'}
  // { path: 'user', loadChildren: 'app/user/user.module#UserModule' }
];