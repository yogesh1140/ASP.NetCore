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
var forms_1 = require("@angular/forms");
var ngx_toastr_1 = require("ngx-toastr");
var upload_service_1 = require("./shared/upload.service");
var router_1 = require("@angular/router");
var AddMovieComponent = /** @class */ (function () {
    function AddMovieComponent(movieService, uploadService, toastr, router) {
        this.movieService = movieService;
        this.uploadService = uploadService;
        this.toastr = toastr;
        this.router = router;
        this.dropdownList = [];
        this.selectedItems = [];
        this.dropdownSettings = {};
        this.addActorMode = false;
        this.addProducerMode = false;
    }
    AddMovieComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.addActorMode = false;
        this.addProducerMode = false;
        this.name = new forms_1.FormControl('', [forms_1.Validators.required]);
        this.yearOfRelease = new forms_1.FormControl('', [forms_1.Validators.required, forms_1.Validators.pattern('[0-9]{4}')]);
        this.plot = new forms_1.FormControl('', [forms_1.Validators.required, , forms_1.Validators.minLength(20), forms_1.Validators.maxLength(500)]);
        this.producer = new forms_1.FormControl('', [forms_1.Validators.required]);
        this.actor = new forms_1.FormControl('', [forms_1.Validators.required]);
        this.posterUrl = new forms_1.FormControl('', forms_1.Validators.required);
        this.addMovieForm = new forms_1.FormGroup({
            name: this.name,
            yearOfRelease: this.yearOfRelease,
            plot: this.plot,
            producer: this.producer,
            posterUrl: this.posterUrl,
            actor: this.actor
        });
        this.movieService.getProducers().subscribe(function (data) {
            _this.producersList = data;
        }, function (err) { return _this.toastr.error(err.error); });
        this.movieService.getActors().subscribe(function (data) {
            _this.actorsList = data;
            _this.updateActorsList();
        }, function (err) { return _this.toastr.error(err.error); });
        this.selectedItems = [];
        this.dropdownSettings = {
            singleSelection: false,
            text: "Select Actor",
            selectAllText: 'Select All',
            unSelectAllText: 'UnSelect All',
            enableSearchFilter: true,
            classes: "myclass custom-class"
        };
    };
    AddMovieComponent.prototype.addMovie = function (formvalue) {
        var _this = this;
        var movie = {
            id: 0,
            name: formvalue.name,
            plot: formvalue.plot,
            yearOfRelease: formvalue.yearOfRelease,
            movieActors: [],
            producer: this.producersList.find(function (pr) { return pr.id == formvalue.producer; }),
            posterUrl: this.imgSrc
        };
        //console.log(formvalue)
        this.selectedItems.forEach(function (ele) { return movie.movieActors.push({ actorId: ele.id }); }),
            this.movieService.addNewMovie(movie).subscribe(function (data) {
                _this.toastr.success('New Movie Added');
                _this.router.navigate(['/movies']);
            }, function (err) { return _this.toastr.error(err.error); });
    };
    AddMovieComponent.prototype.saveNewActor = function (actor) {
        var _this = this;
        this.movieService.addNewActor(actor).subscribe(function (data) {
            _this.actorsList.push(actor);
            _this.toastr.success('New Actor Added');
            _this.selectedItems.push({ id: data.id, itemName: data.firstName + ' ' + data.lastName });
            _this.addActorMode = false;
        }, function (err) {
            return _this.toastr.error(err.error);
        });
    };
    AddMovieComponent.prototype.saveNewProducer = function (producer) {
        var _this = this;
        this.movieService.addNewProducer(producer).subscribe(function (data) {
            _this.producersList.push(data);
            _this.toastr.success("New Producer Added");
            _this.addProducerMode = false;
        }, function (err) { return _this.toastr.error(err.error); });
    };
    AddMovieComponent.prototype.updateActorsList = function () {
        var _this = this;
        this.actorsList.forEach(function (ele) { return _this.dropdownList.push({ id: ele.id, itemName: ele.firstName + ' ' + ele.lastName }); });
    };
    AddMovieComponent.prototype.cancelNewProducer = function () {
        this.addProducerMode = false;
    };
    AddMovieComponent.prototype.cancelNewActor = function () {
        this.addActorMode = false;
    };
    AddMovieComponent.prototype.uploadFiles = function (files) {
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
    AddMovieComponent.prototype.cancel = function () {
        this.router.navigate(['/movies']);
    };
    AddMovieComponent = __decorate([
        core_1.Component({
            templateUrl: 'add-movie.component.html',
            styles: ["\n    em {float:right; color:#E05C65; padding-left:10px;}\n    .error input, .error select, .error textarea {background-color:#E3C3C5;}\n    .error ::-webkit-input-placeholder { color: #999; } \n    .error :-moz-placeholder { color: #999; }\n    .error ::-moz-placeholder { color: #999; }\n    .error :ms-input-placeholder  { color: #999; }\n  "]
        }),
        __metadata("design:paramtypes", [movie_service_1.MovieService,
            upload_service_1.UploadService,
            ngx_toastr_1.ToastrService,
            router_1.Router])
    ], AddMovieComponent);
    return AddMovieComponent;
}());
exports.AddMovieComponent = AddMovieComponent;
//# sourceMappingURL=add-movie.component.js.map