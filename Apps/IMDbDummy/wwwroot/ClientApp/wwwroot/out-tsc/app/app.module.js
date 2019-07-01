"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var index_1 = require("./index");
var _404_component_1 = require("./errors/404.component");
var index_2 = require("./movies/index");
var app_component_1 = require("./app.component");
var navbar_component_1 = require("./nav/navbar.component");
var footer_component_1 = require("./footer/footer-component");
var collapsible_well_component_1 = require("./common/collapsible-well.component");
var routes_1 = require("./routes");
var http_1 = require("@angular/common/http");
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        index_1.NgModule({
            declarations: [
                app_component_1.AppComponent,
                navbar_component_1.NavBarComponent,
                _404_component_1.Error404Component,
                index_2.MovieListComponent,
                index_2.MovieThumbnailComponent,
                footer_component_1.FooterComponent,
                index_2.MovieDetailsComponent,
                collapsible_well_component_1.CollapsibleWellComponent,
                index_2.AddNewActorComponent,
                index_2.ActorsListComponent,
                index_2.AddActorComponent,
                index_2.AddNewProducerComponent,
                index_2.UpdateProducerComponent,
                index_2.AddMovieComponent,
                index_2.UpdateMovieComponent
            ],
            imports: [
                index_1.BrowserModule,
                index_1.HttpClientModule,
                index_1.ReactiveFormsModule,
                index_1.FormsModule,
                index_1.BrowserAnimationsModule,
                index_1.ToastrModule.forRoot(),
                index_1.RouterModule.forRoot(routes_1.appRoutes, { useHash: true }),
                index_1.AngularMultiSelectModule
            ],
            providers: [
                index_2.MovieService,
                index_2.MovieResolver,
                index_2.UploadService,
                { provide: http_1.HTTP_INTERCEPTORS, useClass: index_2.RedirectorInterceptor, multi: true }
            ],
            bootstrap: [app_component_1.AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map