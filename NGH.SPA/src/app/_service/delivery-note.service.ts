import { DeliveryNote } from '../_models/DeliveryNote';
import { catchError } from 'rxjs/operators';
import { PaginatedResult } from '../_models/Pagination';
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';


@Injectable()
export class DeliveryNoteService {
    baseUrl = 'http://localhost:5000/api/deliverynote';

    constructor(private http: HttpClient) { }

    getDeliveryNotes(page?, itemsPerPage?, userParams?): Observable<PaginatedResult<DeliveryNote[]>> {
        const paginatedResult: PaginatedResult<DeliveryNote[]> = new PaginatedResult<DeliveryNote[]>();
        let params = new HttpParams();

        if (page != null && itemsPerPage != null) {
            params = params.append('pageNumber', page);
            params = params.append('pageSize', itemsPerPage);
        }

        if (userParams != null) {
            params = params.append('searchString', userParams.searchString);
            params = params.append('customerId', userParams.customerId);
            params = params.append('status', userParams.status);
            params = params.append('salesPic', userParams.salesPic);
            params = params.append('dnDateFrom', userParams.dnDateFrom);
            params = params.append('dnDateTo', userParams.dnDateTo);
            params = params.append('dueDateFrom', userParams.dueDateFrom);
            params = params.append('dueDateTo', userParams.dueDateTo);
        }

        return this.http.get<DeliveryNote[]>(this.baseUrl, { observe: 'response', params })
            .pipe(
                map(response => {
                    paginatedResult.result = response.body;
                    if (response.headers.get('Pagination') != null) {
                        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
                    }
                    return paginatedResult;
                })
            );
    }
    getDeliveryNote(id): Observable<DeliveryNote> {
        return this.http.get<DeliveryNote>(this.baseUrl + '/' + id);
    }
    updateDeliveryNote(id: number, deliverynote: DeliveryNote) {
        return this.http.put(this.baseUrl + '/' + id, deliverynote);
    }
    createDeliveryNote(id: number, deliverynote: DeliveryNote) {
        return this.http.post(this.baseUrl + '/' + id, deliverynote);
    }
}

