import { Component, Input, OnInit, Output, EventEmitter } from "@angular/core";
import { IMovie } from "./shared/movie.model";

@Component({
    selector: 'app-moviethumbnail',
    templateUrl : 'movie-thumbnail.component.html',
    styleUrls: [
        'movie-thumbnail.component.css'
    ]
})
export class MovieThumbnailComponent {
    @Input() movie: IMovie
    
}