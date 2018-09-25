import { PaginatedResult } from './../_models/Pagination';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Product } from '../_models/product';
import { Observable, throwError } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class ProductService {
    baseUrl = 'http://localhost:5000/api/product';

    constructor(private http: HttpClient) { }

    getAllProducts(): Observable<Product[]> {
        return this.http.get<Product[]>(this.baseUrl);
    }

    getProductList(page?, itemsPerPage?, searchParams?): Observable<PaginatedResult<Product[]>> {
        const paginatedResult = new PaginatedResult<Product[]>();
        let params = new HttpParams();

        if (page != null && itemsPerPage != null) {
            params = params.append('pageNumber', page);
            params = params.append('pageSize', itemsPerPage);
        }

        if (searchParams != null) {
            params = params.append('searchString', searchParams.searchString);
        }
        return this.http.get<Product[]>(this.baseUrl, { observe: 'response', params })
             .pipe(
                 map(res => {
                     paginatedResult.result = res.body;
                     if (res.headers.get('Pagination') != null) {
                         paginatedResult.pagination = JSON.parse(res.headers.get('Pagination'));
                     }
                     return paginatedResult;
                 })
             );
    }

    getProductById(id: number): Observable<Product> {
        return this.http.get<Product>(this.baseUrl + '/' + id);
    }

    addProduct(product: Product) {
        console.log('Add product');
        return this.http.post(this.baseUrl, product);
    }

    updateProduct(id: number, product: Product) {
        console.log('Update product');
        return this.http.put(this.baseUrl + '/' + id, product);
    }

    private handleError(error: any) {
        const applicationError = error.headers.get('Application-Error');
        if (applicationError) {
            return throwError(applicationError);
        }
        const serverError = error.json();
        let modelStateError = '';
        if (serverError) {
            for (const key in serverError) {
                if (serverError[key]) {
                    modelStateError += serverError[key] + '\n';
                }
            }
        }
        return throwError(modelStateError || 'Server error');
    }
}
