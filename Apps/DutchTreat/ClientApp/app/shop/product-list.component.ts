import { Component, OnInit } from '@angular/core'
import { DataService } from '../shared/data.service';
import { Product } from '../shared/product';
@Component({
    selector: 'app-product-list',
    templateUrl: 'product-list.component.html',
    styleUrls: ['product-list.component.css']
})
export class ProductListComponent implements OnInit {

    public products: Product[] = []
    constructor(private data: DataService) { }
    ngOnInit() {
        this.data.loadProducts().subscribe(data => this.products=data)
    }
    addProduct(product: Product){
        this.data.addToOrder(product)
    }
}

