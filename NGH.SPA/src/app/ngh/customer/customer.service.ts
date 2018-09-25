import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Helpers } from '../Heplers/helpers';
import { Injectable } from '@angular/core';
import { Customer } from '../../_models/customer';
import { PaginatedResult } from '../../_models/Pagination';
import { map } from 'rxjs/operators';

@Injectable()
export class CustomerService {
    private baseUrl = 'http://localhost:5000/api/customer';
    constructor(private http: HttpClient) { }

    getAllCustomers() {
        return this.http.get(this.baseUrl + '/all');
    }

    getCustomerList(page?, itemsPerPage?, param?): Observable<PaginatedResult<Customer[]>> {
        const paginatedResult: PaginatedResult<Customer[]> = new PaginatedResult<Customer[]>();
        let params = new HttpParams();

        if (page != null && itemsPerPage != null) {
            params = params.append('pageNumber', page);
            params = params.append('pageSize', itemsPerPage);
        }

        if (param != null) {
            params = params.append('ageFrom', param.ageFrom);
            params = params.append('ageFrom', param.ageTo);
            params = params.append('ageFrom', param.gender);
        }
        return this.http.get<Customer[]>(this.baseUrl, { observe: 'response', params })
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

    getCustomerById(id: number) {
        return this.http.get<Customer>(this.baseUrl + '/' + id);
    }

    addCustomer(customer: Customer): Observable<Customer> {
        return this.http.post<Customer>(this.baseUrl, customer);
    }
    updateCustomer(customer: Customer): Observable<Customer> {
        return this.http.put<Customer>(this.baseUrl, customer);
    }
    deleteCustomer(id: number) {
        this.http.delete(this.baseUrl + '/' + id);
    }

}
