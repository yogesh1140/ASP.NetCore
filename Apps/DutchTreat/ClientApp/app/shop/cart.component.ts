import { Component } from "@angular/core";
import { DataService } from "../shared/data.service";
import { Router } from "@angular/router";

@Component({
    selector: 'app-cart',
    templateUrl: 'cart.component.html',
    styleUrls:[]
})
export class CartComponent {
    /**
     *
     */
    constructor(private data: DataService, private router: Router) {
       
        
    }
    OnCheckOut(){
        if(this.data.loginRequired){
            this.router.navigate(['login'])
        }else{
            this.router.navigate(['checkout'])
            
        }
    }
}
