"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// import { 
//   EventsListComponent, 
//   EventDetailsComponent, 
//   CreateEventComponent, 
//   EventListResolver,
//   CreateSessionComponent,
//   EventResolver } from './events/index';
var _404_component_1 = require("./errors/404.component");
var movie_list_component_1 = require("./movies/movie-list.component");
var movie_details_component_1 = require("./movies/movie-details/movie-details.component");
var movie_resolver_1 = require("./movies/movie-resolver");
var add_movie_component_1 = require("./movies/add-movie.component");
exports.appRoutes = [
    // 
    { path: 'movies/new', component: add_movie_component_1.AddMovieComponent },
    { path: 'movies', component: movie_list_component_1.MovieListComponent },
    { path: 'movies/:id', component: movie_details_component_1.MovieDetailsComponent, resolve: { movie: movie_resolver_1.MovieResolver } },
    { path: '404,', component: _404_component_1.Error404Component },
    { path: '', redirectTo: '/movies', pathMatch: 'full' }
    // { path: 'user', loadChildren: 'app/user/user.module#UserModule' }
];
//# sourceMappingURL=routes.js.map