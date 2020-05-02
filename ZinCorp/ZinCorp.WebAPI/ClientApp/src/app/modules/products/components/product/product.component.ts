import { Component, Input, OnInit } from '@angular/core';
import { Product } from '../../../../shared/interfaces/product';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {
  @Input() product: Product;
  array = [1, 2, 3, 4];
  effect = 'scrollx';

  constructor() {}

  ngOnInit(): void {}
}
