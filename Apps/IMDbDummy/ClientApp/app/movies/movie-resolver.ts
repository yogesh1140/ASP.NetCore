import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { MovieService } from './shared/movie.service';

@Injectable()
export class MovieResolver implements Resolve<any> {
  constructor(private movieService:MovieService) {}
  resolve(route: ActivatedRouteSnapshot) {  
    return this.movieService.getMovieById(route.params['id']);
  }
}
