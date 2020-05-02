import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../../../shared/interfaces/product';

@Component({
  selector: 'app-landing-page',
  templateUrl: './products-page.component.html',
  styleUrls: ['./products-page.component.scss']
})
export class ProductsPageComponent implements OnInit {
  private _products: Product[];

  get products(): Product[] {
    return this._products;
  }

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.get();
  }

  get() {
    return this.productService.get().subscribe((products) => (this._products = products));
  }
}
