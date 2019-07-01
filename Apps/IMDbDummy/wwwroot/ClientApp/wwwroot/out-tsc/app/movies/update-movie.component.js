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
var forms_1 = require("@angular/forms");
var upload_service_1 = require("./shared/upload.service");
var ngx_toastr_1 = require("ngx-toastr");
var router_1 = require("@angular/router");
var movie_service_1 = require("./shared/movie.service");
var UpdateMovieComponent = /** @class */ (function () {
    function UpdateMovieComponent(movieService, uploadService, toastr, router) {
        this.movieService = movieService;
        this.uploadService = uploadService;
        this.toastr = toastr;
        this.router = router;
        this.cancelUpdateMovie = new core_1.EventEmitter();
        this.updateMovie = new core_1.EventEmitter();
        this.addProducerMode = false;
    }
    UpdateMovieComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.updatedMovie = {
            name: this.movie.name,
            yearOfRelease: this.movie.yearOfRelease,
            plot: this.movie.plot,
            id: this.movie.id,
            posterUrl: this.movie.posterUrl,
            producer: {
                id: this.movie.producer.id,
                firstName: this.movie.producer.firstName,
                lastName: this.movie.producer.lastName,
                sex: this.movie.producer.sex,
                bio: this.movie.producer.bio,
                dob: this.movie.producer.dob
            },
            movieActors: []
        };
        this.imgSrc = this.movie.posterUrl;
        this.name = new forms_1.FormControl(this.updatedMovie.name, [forms_1.Validators.required]);
        this.yearOfRelease = new forms_1.FormControl(this.updatedMovie.yearOfRelease, [forms_1.Validators.required, forms_1.Validators.pattern('[0-9]{4}')]);
        this.plot = new forms_1.FormControl(this.updatedMovie.plot, [forms_1.Validators.required, , forms_1.Validators.minLength(20), forms_1.Validators.maxLength(500)]);
        this.producer = new forms_1.FormControl(this.updatedMovie.producer.id, [forms_1.Validators.required]);
        this.posterUrl = new forms_1.FormControl(this.updatedMovie.posterUrl);
        this.updateMovieForm = new forms_1.FormGroup({
            name: this.name,
            yearOfRelease: this.yearOfRelease,
            plot: this.plot,
            producer: this.producer,
            posterUrl: this.posterUrl
        });
        this.movieService.getProducers().subscribe(function (data) {
            _this.producersList = data;
        }, function (err) { return _this.toastr.error(err.error); });
    };
    UpdateMovieComponent.prototype.saveNewProducer = function (producer) {
        var _this = this;
        this.movieService.addNewProducer(producer).subscribe(function (data) {
            _this.producersList.push(data);
            _this.toastr.success("New Producer Added");
            _this.addProducerMode = false;
        }, function (err) { return _this.toastr.error(err.error); });
    };
    UpdateMovieComponent.prototype.cancelNewProducer = function () {
        this.addProducerMode = false;
    };
    UpdateMovieComponent.prototype.uploadFiles = function (files) {
        var _this = this;
        console.log('length:', files);
        if (files.length === 0)
            return;
        var formData = new FormData();
        for (var _i = 0, files_1 = files; _i < files_1.length; _i++) {
            var file = files_1[_i];
            var ext = file.name.substring(file.name.lastIndexOf('.')).toLowerCase();
            if (ext !== '.jpg' && ext != '.png' && ext != '.bmp' && ext != '.tiff') {
                this.toastr.error('Invalid file selected for poster');
                return;
            }
            formData.append(file.name, file);
        }
        this.uploadService.uploadFile(formData).subscribe(function (data) {
            console.log(data.filePath);
            _this.imgSrc = data.filePath;
        }, function (err) { return _this.toastr.error(err.error); });
    };
    UpdateMovieComponent.prototype.cancel = function () {
        this.cancelUpdateMovie.emit(null);
    };
    UpdateMovieComponent.prototype.updateMovieDetails = function (formvalues) {
        this.updatedMovie.name = formvalues.name;
        this.updatedMovie.yearOfRelease = formvalues.yearOfRelease;
        this.updatedMovie.plot = formvalues.plot;
        this.updatedMovie.producer.id = formvalues.producer;
        this.updatedMovie.posterUrl = this.imgSrc;
        // console.log(formvalues,this.movie, this.updatedMovie)
        if (this.updatedMovie.name != this.movie.name || this.updatedMovie.plot != this.movie.plot || this.updatedMovie.posterUrl != this.movie.posterUrl || this.updatedMovie.producer.id != this.movie.producer.id)
            this.updateMovie.emit(this.updatedMovie);
        else {
            this.toastr.info("No updations made");
            this.cancelUpdateMovie.emit(null);
        }
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], UpdateMovieComponent.prototype, "movie", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], UpdateMovieComponent.prototype, "cancelUpdateMovie", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], UpdateMovieComponent.prototype, "updateMovie", void 0);
    UpdateMovieComponent = __decorate([
        core_1.Component({
            selector: 'app-updatemovie',
            templateUrl: 'update-movie.component.html',
            styles: ["\n    em {float:right; color:#E05C65; padding-left:10px;}\n    .error input, .error select, .error textarea {background-color:#E3C3C5;}\n    .error ::-webkit-input-placeholder { color: #999; } \n    .error :-moz-placeholder { color: #999; }\n    .error ::-moz-placeholder { color: #999; }\n    .error :ms-input-placeholder  { color: #999; }\n  "]
        }),
        __metadata("design:paramtypes", [movie_service_1.MovieService, upload_service_1.UploadService,
            ngx_toastr_1.ToastrService,
            router_1.Router])
    ], UpdateMovieComponent);
    return UpdateMovieComponent;
}());
exports.UpdateMovieComponent = UpdateMovieComponent;
//# sourceMappingURL=update-movie.component.js.map