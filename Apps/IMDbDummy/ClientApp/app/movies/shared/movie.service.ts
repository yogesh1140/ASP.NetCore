import { Injectable, OnInit } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http"
import { Observable, observable } from "rxjs";
import { map, catchError } from 'rxjs/operators';
import { IMovie } from "./movie.model";
import { IActor } from "./actor.model";
import { IProducer } from "./producer.model";

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable()
export class MovieService {
    

    baseUrl = 'http://localhost:5000'
    constructor(private httpClient: HttpClient) { }
    
    deleteMovie(movieId: number): any {
        return this.httpClient.delete(`/api/movies/${movieId}`, httpOptions)
        
    }

    updateMovie(movie: IMovie): Observable<IMovie> {
        return this.httpClient.put<IMovie>(`/api/movies?actorsOnly=false`, movie, httpOptions)
    }

   

    addNewMovie(movie: any): any {
        return this.httpClient.post<IProducer>(this.baseUrl + '/api/movies', movie, httpOptions)
    }

    addNewProducer(producer): Observable<IProducer> {
        return this.httpClient.post<IProducer>(this.baseUrl + '/api/producers', producer, httpOptions)
    }
    getProducers(): Observable<IProducer[]> {
        return this.httpClient.get<IProducer[]>(`/api/producers`, httpOptions)

    }

    addNewActor(actor: IActor): Observable<IActor> {
        return this.httpClient.post<IActor>(this.baseUrl + '/api/actors', actor, httpOptions)
        //.pipe(catchError(error=>of(null)))
    }


    getAllMovies(): Observable<IMovie[]> {       
        
        return this.httpClient.get<IMovie[]>(this.baseUrl + '/api/movies', httpOptions)
    }
    getMovieById(id: number): Observable<IMovie> {
        return this.httpClient.get<IMovie>(`/api/movies/${id}`, httpOptions)
    }
    getActors(): Observable<IActor[]> {
        return this.httpClient.get<IActor[]>(`/api/actors`, httpOptions)
    }
    addActorToMovie(movie: IMovie, actor: IActor) {
        let mv: any = { id: movie.id, name: movie.name, posterUrl: movie.posterUrl, yearOfRelease: movie.yearOfRelease, producer: movie.producer, movieActors: [] }
        movie.movieActors.forEach((a: any) => {
            mv.movieActors.push({ actorId: a.actorId, movieId: movie.id })
        })
        mv.movieActors.push({ actorId: actor.id, movieId: movie.id })
        return this.httpClient.put(`/api/movies`, mv, httpOptions)
    }
    deleteActorFromMovie(movie: IMovie, actorId: number) {
       
        let mv: any = { id: movie.id, name: movie.name, posterUrl: movie.posterUrl, yearOfRelease: movie.yearOfRelease, producer: movie.producer, movieActors: [] }
        movie.movieActors.forEach((a: any) => {
            if (a.actorId != actorId)
                mv.movieActors.push({ actorId: a.actorId, movieId: movie.id })
        })
        return this.httpClient.put(`/api/movies`, mv, httpOptions)
    }
}
