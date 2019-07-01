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
var ngx_toastr_1 = require("ngx-toastr");
var AddActorComponent = /** @class */ (function () {
    function AddActorComponent(movieService, toastr) {
        this.movieService = movieService;
        this.toastr = toastr;
        this.addActor = new core_1.EventEmitter();
        this.cancelAddActor = new core_1.EventEmitter();
    }
    AddActorComponent.prototype.addActorToMovie = function (selectedActorId) {
        // console.log(this.movieId + ' ' + formValues)        
        this.addActor.emit(this.actors.find(function (act) { return act.id == selectedActorId; }));
    };
    AddActorComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.isError = false;
        this.addActorMode = false;
        this.movieService.getActors().subscribe(function (res) {
            _this.actors = res;
            _this.actors = _this.actors.filter(function (act) {
                return _this.existingActors.find(function (ac) { return ac.id == act.id; }) == null;
            });
        });
    };
    AddActorComponent.prototype.AddNewActor = function () {
        this.addActorMode = true;
        this.isError = false;
    };
    AddActorComponent.prototype.cancel = function () {
        this.isError = false;
        this.cancelAddActor.emit(null);
    };
    AddActorComponent.prototype.cancelNewActor = function () {
        this.addActorMode = false;
    };
    AddActorComponent.prototype.saveNewActor = function (actor) {
        var _this = this;
        this.movieService.addNewActor(actor).subscribe(function (data) {
            _this.addActorMode = false;
            _this.ngOnInit();
            _this.toastr.success('New Actor added');
            _this.selectedActor = actor;
        }, function (error) {
            _this.isError = true;
            _this.toastr.error(error.error);
            // console.log('error: ' + this.errorMessage.error)
        });
    };
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], AddActorComponent.prototype, "addActor", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], AddActorComponent.prototype, "cancelAddActor", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Array)
    ], AddActorComponent.prototype, "existingActors", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Number)
    ], AddActorComponent.prototype, "movieId", void 0);
    AddActorComponent = __decorate([
        core_1.Component({
            selector: 'app-addactor',
            templateUrl: 'add-actor.component.html',
        }),
        __metadata("design:paramtypes", [movie_service_1.MovieService, ngx_toastr_1.ToastrService])
    ], AddActorComponent);
    return AddActorComponent;
}());
exports.AddActorComponent = AddActorComponent;
//# sourceMappingURL=add-actor.component.js.map