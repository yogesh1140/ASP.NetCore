import { Injectable } from "@angular/core";
import {  HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable()
export class RedirectorInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next :HttpHandler):Observable<HttpEvent<any>>{
        // req = req.clone({
        //     url:  req.url
        // })
        return next.handle(req)

    }
}