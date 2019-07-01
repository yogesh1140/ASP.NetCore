import { Component, OnInit } from "@angular/core";
import { MovieService } from "./shared/movie.service";
import { IMovie } from "./shared/movie.model";
import { ToastrService } from "ngx-toastr";

@Component({
    selector : 'app-movielist',
    templateUrl:'movie-list.component.html'
    

})
export class MovieListComponent implements OnInit {
    movieList: IMovie[]
    constructor(private movieService: MovieService, private toastr: ToastrService){}
    ngOnInit(){
        this.movieService.getAllMovies().subscribe(
            res=> this.movieList =res, 
            error=>console.log(error) 
        )
    }
    deleteMovie(movieId: number){
        this.movieService.deleteMovie(movieId).subscribe(
            success=> this.toastr.success('Movie Deleted: '+movieId),
            error=>this.toastr.error(error.error))
    }
}
