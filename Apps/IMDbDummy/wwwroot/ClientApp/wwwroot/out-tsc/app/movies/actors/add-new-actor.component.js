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
// import { ISession, restrictedWords } from '../shared/index';
var AddNewActorComponent = /** @class */ (function () {
    function AddNewActorComponent() {
        this.newActorAdded = new core_1.EventEmitter();
        this.cancelNewActor = new core_1.EventEmitter();
    }
    AddNewActorComponent.prototype.ngOnInit = function () {
        this.firstName = new forms_1.FormControl('', [forms_1.Validators.required, forms_1.Validators.pattern('[A-Za-z]*')]);
        this.lastName = new forms_1.FormControl('', [forms_1.Validators.required, forms_1.Validators.pattern('[A-Za-z]*')]);
        this.dob = new forms_1.FormControl('', forms_1.Validators.required);
        this.bio = new forms_1.FormControl('', [forms_1.Validators.required, forms_1.Validators.minLength(20), forms_1.Validators.maxLength(500)]);
        this.sex = new forms_1.FormControl('', forms_1.Validators.required);
        this.newActorForm = new forms_1.FormGroup({
            firstName: this.firstName,
            lastName: this.lastName,
            dob: this.dob,
            bio: this.bio,
            sex: this.sex
        });
    };
    AddNewActorComponent.prototype.addActor = function (formValues) {
        var actor = {
            id: 0,
            firstName: formValues.firstName,
            lastName: formValues.lastName,
            dob: formValues.dob,
            sex: formValues.sex,
            bio: formValues.bio
        };
        // console.log(actor)
        this.newActorAdded.emit(actor);
    };
    AddNewActorComponent.prototype.cancel = function () {
        this.cancelNewActor.emit(null);
    };
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], AddNewActorComponent.prototype, "newActorAdded", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], AddNewActorComponent.prototype, "cancelNewActor", void 0);
    AddNewActorComponent = __decorate([
        core_1.Component({
            selector: 'app-addnewactor',
            templateUrl: 'add-new-actor.component.html',
            styles: ["\n    em {float:right; color:#E05C65; padding-left:10px;}\n    .error input, .error select, .error textarea {background-color:#E3C3C5;}\n    .error ::-webkit-input-placeholder { color: #999; } \n    .error :-moz-placeholder { color: #999; }\n    .error ::-moz-placeholder { color: #999; }\n    .error :ms-input-placeholder  { color: #999; }\n  "]
        })
    ], AddNewActorComponent);
    return AddNewActorComponent;
}());
exports.AddNewActorComponent = AddNewActorComponent;
//# sourceMappingURL=add-new-actor.component.js.map