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
var movie_service_1 = require("../shared/movie.service");
var router_1 = require("@angular/router");
var ngx_toastr_1 = require("ngx-toastr");
var MovieDetailsComponent = /** @class */ (function () {
    function MovieDetailsComponent(movieService, route, toastr, router) {
        this.movieService = movieService;
        this.route = route;
        this.toastr = toastr;
        this.router = router;
        this.actorList = [];
        this.filterBy = 'All';
    }
    MovieDetailsComponent.prototype.deleteMovie = function () {
        var _this = this;
        this.movieService.deleteMovie(this.movie.id).subscribe(function (error) {
            _this.toastr.error(error.error);
        }, function (success) {
            _this.toastr.success('Movie Deleted: ' + _this.movie.name);
            _this.router.navigate(["/movies"]);
        });
    };
    MovieDetailsComponent.prototype.getactors = function (movie) {
        return movie.movieActors;
        // return movie.movieActors.map(ma=> ma.actor !=null ? ma.actor : ma)
    };
    MovieDetailsComponent.prototype.addActor = function () {
        this.addMode = true;
    };
    MovieDetailsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.addMode = false;
        this.route.data.forEach(function (data) {
            //console.log(data['movie'])
            _this.movie = data['movie'];
            _this.addMode = false;
            _this.movie.movieActors.forEach(function (element) {
                // console.log(element)
                _this.actorList.push(element.actor);
            });
            // console.log(this.actorList)
        });
    };
    MovieDetailsComponent.prototype.cancelUpdateMovie = function () {
        this.updateMovieMode = false;
    };
    MovieDetailsComponent.prototype.updateMovie = function (movie) {
        var _this = this;
        this.movieService.updateMovie(movie).subscribe(function (data) {
            _this.updateMovieMode = false;
            _this.toastr.success("Movie details updated");
            _this.movie = data;
            //this.router.navigate([`/movies/${movie.id}`])
        }, function (error) {
            _this.toastr.error(error.error);
        });
    };
    MovieDetailsComponent.prototype.cancelAddActor = function () {
        this.addMode = false;
    };
    MovieDetailsComponent.prototype.addActorToMovie = function (actor) {
        var _this = this;
        //  console.log('actor: ', actor)
        this.movieService.addActorToMovie(this.movie, actor).subscribe(function (data) {
            _this.actorList.push(actor);
            _this.addMode = false;
            _this.toastr.success("Actor Added from movie");
        }, function (error) {
            _this.toastr.error(error.error);
        });
    };
    MovieDetailsComponent.prototype.deleteActorFromMovie = function (actor) {
        var _this = this;
        //  console.log('actor: ', actor)
        this.movieService.deleteActorFromMovie(this.movie, actor.id).subscribe(function (data) {
            _this.actorList = _this.actorList.filter(function (al) { return al.id != actor.id; });
            _this.addMode = false;
            _this.toastr.success("Actor deleted from movie");
        }, function (error) {
            _this.toastr.error(error.error);
            // console.log(error.error)
        });
    };
    MovieDetailsComponent = __decorate([
        core_1.Component({
            templateUrl: 'movie-details.component.html',
            styles: ["\n    div span{\n        color: darkslategrey;\n    }\n    "]
        }),
        __metadata("design:paramtypes", [movie_service_1.MovieService, router_1.ActivatedRoute, ngx_toastr_1.ToastrService, router_1.Router])
    ], MovieDetailsComponent);
    return MovieDetailsComponent;
}());
exports.MovieDetailsComponent = MovieDetailsComponent;
//# sourceMappingURL=movie-details.component.js.map