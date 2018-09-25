import { catchError, map } from 'rxjs/operators';
import { ProductService } from '../_service/product.service';
import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Product } from '../_models/Product';
import { Observable, of } from 'rxjs';
import { AlertifyService } from '../_service/alertify.service';

@Injectable()
export class ProductResolver implements Resolve<Product> {
    private product: Product = new Product();
    constructor(private productService: ProductService,
        private alertify: AlertifyService,
        private router: Router) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Product> {
        return this.productService.getProductById(route.params['id']).pipe(
            map((data) => { if (data) { return data; } }),
            catchError(error => {
                this.alertify.error('Problem getting product info');
                this.router.navigate(['/product']);
                return of(null);
            })
        );
    }
}
