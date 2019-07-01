import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Product } from "./product";
import { Order, OrderItem } from "./order";
import {Observable} from 'rxjs'
import {catchError, map} from 'rxjs/operators';

@Injectable()
export class DataService {
    public order: Order = new Order()
    public token: string = ''
    private tokenExpiration: Date;
    constructor(private http: HttpClient) { }
    loadProducts() {
        return this.http.get<Product[]>("/api/products")
    }
    public get loginRequired():boolean{
        return this.token.length ==0 || this.tokenExpiration> new Date()
    }

    login(creds): Observable<boolean>{
        return this.http.post('/account/createtoken', creds).pipe(map((res:any)=>{
            this.token= res.token,
            this.tokenExpiration= res.expiration
            return true
        }))
    }
    checkOut(){
        if(!this.order.orderNumber){
            this.order.orderNumber= this.order.orderDate.getFullYear().toString()+ this.order.orderDate.getTime().toString()
        }
        else{
            
        }
        // console.log('token: '+this.token)
        return this.http.post('/api/orders', this.order).pipe(map(res=>{
            this.order=new Order()
            return true
        }))
    }
    public addToOrder(newProduct: Product){
    
        let item :OrderItem= this.order.items.find(i=>i.productId==newProduct.id)
        if(item){
            item.quantity+=1
        }else{
            item= new OrderItem()
            item.productId =newProduct.id
            item.productArtist=newProduct.artist
            item.productArtId=newProduct.artId
            item.productSize=newProduct.size
            item.productTitle=newProduct.title
            item.unitPrice=newProduct.price
            item.quantity=1
            item.productCategory=newProduct.category

            this.order.items.push(item)
        }
        
        
    }
}
