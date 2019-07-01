"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var httpOptions = {
    headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json' })
};
var MovieService = /** @class */ (function () {
    function MovieService(httpClient) {
        this.httpClient = httpClient;
        this.baseUrl = 'http://localhost:5000';
    }
    MovieService.prototype.deleteMovie = function (movieId) {
        return this.httpClient.delete("/api/movies/" + movieId, httpOptions);
    };
    MovieService.prototype.updateMovie = function (movie) {
        return this.httpClient.put("/api/movies?actorsOnly=false", movie, httpOptions);
    };
    MovieService.prototype.addNewMovie = function (movie) {
        return this.httpClient.post(this.baseUrl + '/api/movies', movie, httpOptions);
    };
    MovieService.prototype.addNewProducer = function (producer) {
        return this.httpClient.post(this.baseUrl + '/api/producers', producer, httpOptions);
    };
    MovieService.prototype.getProducers = function () {
        return this.httpClient.get("/api/producers", httpOptions);
    };
    MovieService.prototype.addNewActor = function (actor) {
        return this.httpClient.post(this.baseUrl + '/api/actors', actor, httpOptions);
        //.pipe(catchError(error=>of(null)))
    };
    MovieService.prototype.getAllMovies = function () {
        return this.httpClient.get(this.baseUrl + '/api/movies', httpOptions);
    };
    MovieService.prototype.getMovieById = function (id) {
        return this.httpClient.get("/api/movies/" + id, httpOptions);
    };
    MovieService.prototype.getActors = function () {
        return this.httpClient.get("/api/actors", httpOptions);
    };
    MovieService.prototype.addActorToMovie = function (movie, actor) {
        var mv = { id: movie.id, name: movie.name, posterUrl: movie.posterUrl, yearOfRelease: movie.yearOfRelease, producer: movie.producer, movieActors: [] };
        movie.movieActors.forEach(function (a) {
            mv.movieActors.push({ actorId: a.actorId, movieId: movie.id });
        });
        mv.movieActors.push({ actorId: actor.id, movieId: movie.id });
        return this.httpClient.put("/api/movies", mv, httpOptions);
    };
    MovieService.prototype.deleteActorFromMovie = function (movie, actorId) {
        var mv = { id: movie.id, name: movie.name, posterUrl: movie.posterUrl, yearOfRelease: movie.yearOfRelease, producer: movie.producer, movieActors: [] };
        movie.movieActors.forEach(function (a) {
            if (a.actorId != actorId)
                mv.movieActors.push({ actorId: a.actorId, movieId: movie.id });
        });
        return this.httpClient.put("/api/movies", mv, httpOptions);
    };
    MovieService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], MovieService);
    return MovieService;
}());
exports.MovieService = MovieService;
//# sourceMappingURL=movie.service.js.map