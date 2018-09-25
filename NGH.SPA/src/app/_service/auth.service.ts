import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ServiceNameService {
    baseUrl = environment.baseApiUrl + 'auth';
    constructor(private http: HttpClient) { }

    register (model) {
        this.http.post(this.baseUrl, model);
    }

    login(model) {
        this.http.post(this.baseUrl, model);
    }
}
