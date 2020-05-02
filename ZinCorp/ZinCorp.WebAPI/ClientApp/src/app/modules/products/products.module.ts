import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing.module';
import { ProductsPageComponent } from './pages/products-page/products-page.component';
import { SharedModule } from '../../shared/shared.module';
import { ProductComponent } from './components/product/product.component';

@NgModule({
  declarations: [ProductsPageComponent, ProductComponent],
  imports: [CommonModule, ProductsRoutingModule, SharedModule]
})
export class ProductsModule {}
