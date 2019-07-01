"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
// import { AuthService } from '../user/auth.service';
// import { ISession } from '../events/shared/event.model';
// import { EventService } from '../events/index';
var NavBarComponent = /** @class */ (function () {
    function NavBarComponent() {
    }
    NavBarComponent = __decorate([
        core_1.Component({
            selector: 'app-navbar',
            templateUrl: 'navbar.component.html',
            styles: ["\n    .nav.navbar-nav {font-size:15px} \n    #searchForm {margin-right:100px; } \n    @media (max-width: 1200px) {#searchForm {display:none}}\n    li > a.active { color: #F97924; }\n    .nav li :hover { background-color: #ddd; }\n  "],
        })
    ], NavBarComponent);
    return NavBarComponent;
}());
exports.NavBarComponent = NavBarComponent;
//# sourceMappingURL=navbar.component.js.map