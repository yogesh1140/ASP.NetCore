import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http"
import { Observable } from "rxjs";

const httpOptions = {
    reportProgress: true
};
@Injectable()

export class UploadService {
   // private baseUrl = 'http://localhost:5000'
    constructor(private httpClient: HttpClient) { 
       
    }
    uploadFile(formData: FormData):Observable<any> {
        return this.httpClient.post<any>('/api/upload', formData,httpOptions)
    }
}
