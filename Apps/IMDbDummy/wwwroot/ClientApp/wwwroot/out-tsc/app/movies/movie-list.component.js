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
var ngx_toastr_1 = require("ngx-toastr");
var MovieListComponent = /** @class */ (function () {
    function MovieListComponent(movieService, toastr) {
        this.movieService = movieService;
        this.toastr = toastr;
    }
    MovieListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.movieService.getAllMovies().subscribe(function (res) { return _this.movieList = res; }, function (error) { return console.log(error); });
    };
    MovieListComponent.prototype.deleteMovie = function (movieId) {
        var _this = this;
        this.movieService.deleteMovie(movieId).subscribe(function (success) { return _this.toastr.success('Movie Deleted: ' + movieId); }, function (error) { return _this.toastr.error(error.error); });
    };
    MovieListComponent = __decorate([
        core_1.Component({
            selector: 'app-movielist',
            templateUrl: 'movie-list.component.html'
        }),
        __metadata("design:paramtypes", [movie_service_1.MovieService, ngx_toastr_1.ToastrService])
    ], MovieListComponent);
    return MovieListComponent;
}());
exports.MovieListComponent = MovieListComponent;
//# sourceMappingURL=movie-list.component.js.map