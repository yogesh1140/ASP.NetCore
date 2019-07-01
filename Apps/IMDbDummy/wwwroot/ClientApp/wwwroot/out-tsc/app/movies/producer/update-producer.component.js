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
var UpdateProducerComponent = /** @class */ (function () {
    function UpdateProducerComponent(movieService, toastr) {
        this.movieService = movieService;
        this.toastr = toastr;
        this.addProducer = new core_1.EventEmitter();
        this.cancelAddProducer = new core_1.EventEmitter();
    }
    UpdateProducerComponent.prototype.updateProducer = function (selectedActorId) {
        // console.log(this.movieId + ' ' + formValues)        
        this.addProducer.emit(this.producers.find(function (act) { return act.id == selectedActorId; }));
    };
    UpdateProducerComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.isError = false;
        this.addProduceMode = false;
        this.movieService.getProducers().subscribe(function (res) {
            _this.producers = res;
            _this.producers = _this.producers.filter(function (act) {
                return _this.existingProducer.id != act.id;
            });
        });
    };
    UpdateProducerComponent.prototype.AddnewProducer = function () {
        this.addProduceMode = true;
        this.isError = false;
    };
    UpdateProducerComponent.prototype.cancel = function () {
        this.isError = false;
        this.cancelAddProducer.emit(null);
    };
    UpdateProducerComponent.prototype.cancelNewProducer = function () {
        this.addProduceMode = false;
    };
    UpdateProducerComponent.prototype.saveNewProducer = function (producer) {
        var _this = this;
        this.movieService.addNewProducer(producer).subscribe(function (data) {
            _this.addProduceMode = false;
            _this.ngOnInit();
            _this.toastr.success('New Producer added');
        }, function (error) {
            _this.isError = true;
            _this.toastr.error(error.error);
            // console.log('error: ' + this.errorMessage.error)
        });
    };
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], UpdateProducerComponent.prototype, "addProducer", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], UpdateProducerComponent.prototype, "cancelAddProducer", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], UpdateProducerComponent.prototype, "existingProducer", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Number)
    ], UpdateProducerComponent.prototype, "movieId", void 0);
    UpdateProducerComponent = __decorate([
        core_1.Component({
            selector: 'app-updateproducer',
            templateUrl: 'update-producer.component.html',
        }),
        __metadata("design:paramtypes", [movie_service_1.MovieService, ngx_toastr_1.ToastrService])
    ], UpdateProducerComponent);
    return UpdateProducerComponent;
}());
exports.UpdateProducerComponent = UpdateProducerComponent;
//# sourceMappingURL=update-producer.component.js.map