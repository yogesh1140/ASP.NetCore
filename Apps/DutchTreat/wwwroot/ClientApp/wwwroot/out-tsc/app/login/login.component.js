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
var data_service_1 = require("../shared/data.service");
var router_1 = require("@angular/router");
var LoginComponent = /** @class */ (function () {
    function LoginComponent(data, router) {
        this.data = data;
        this.router = router;
        this.errMessage = '';
        this.creds = {
            username: '',
            password: ''
        };
    }
    LoginComponent.prototype.onLogin = function () {
        var _this = this;
        this.data.login(this.creds);
        this.data.login(this.creds).subscribe(function (success) {
            if (_this.data.order.items.length == 0) {
                _this.router.navigate(['']);
            }
            else {
                _this.router.navigate(['/checkout']);
            }
        }, function (err) { return _this.errMessage = 'Failed to LogIn'; });
    };
    LoginComponent = __decorate([
        core_1.Component({
            templateUrl: 'login.component.html'
        }),
        __metadata("design:paramtypes", [data_service_1.DataService, router_1.Router])
    ], LoginComponent);
    return LoginComponent;
}());
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=login.component.js.map