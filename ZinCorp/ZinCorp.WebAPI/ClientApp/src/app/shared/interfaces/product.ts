export interface Product {
  id: number;
  name: string;
  description: string;
  rate: number;
  productImages: ProductImages[];
}

export interface ProductImages {
  id: number;
  name: string;
  base64: string;
}
