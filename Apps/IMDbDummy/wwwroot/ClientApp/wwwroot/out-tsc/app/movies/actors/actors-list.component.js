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
var ActorsListComponent = /** @class */ (function () {
    function ActorsListComponent() {
        this.filteredActors = [];
        this.deleteActorFromMovie = new core_1.EventEmitter();
    }
    ActorsListComponent.prototype.ngOnInit = function () {
        if (this.filterBy == 'Actor')
            this.filteredActors = this.actors.filter(function (act) { return act.sex == 'Male'; });
        else if (this.filterBy == 'Actress')
            this.filteredActors = this.actors.filter(function (act) { return act.sex == 'Female'; });
        else
            this.filteredActors = this.actors;
        // console.log(this.filterBy , this.filteredActors)
    };
    ActorsListComponent.prototype.ngOnChanges = function () {
        this.ngOnInit();
    };
    ActorsListComponent.prototype.deleteActor = function (actor) {
        this.deleteActorFromMovie.emit(actor);
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Array)
    ], ActorsListComponent.prototype, "actors", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", String)
    ], ActorsListComponent.prototype, "filterBy", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], ActorsListComponent.prototype, "deleteActorFromMovie", void 0);
    ActorsListComponent = __decorate([
        core_1.Component({
            selector: 'app-actorslist',
            templateUrl: 'actors-list.component.html'
        })
    ], ActorsListComponent);
    return ActorsListComponent;
}());
exports.ActorsListComponent = ActorsListComponent;
//# sourceMappingURL=actors-list.component.js.map