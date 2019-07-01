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
var movie_service_1 = require("./shared/movie.service");
var MovieResolver = /** @class */ (function () {
    function MovieResolver(movieService) {
        this.movieService = movieService;
    }
    MovieResolver.prototype.resolve = function (route) {
        return this.movieService.getMovieById(route.params['id']);
    };
    MovieResolver = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [movie_service_1.MovieService])
    ], MovieResolver);
    return MovieResolver;
}());
exports.MovieResolver = MovieResolver;
//# sourceMappingURL=movie-resolver.js.map