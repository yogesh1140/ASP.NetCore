import { Component } from "@angular/core";
import { DataService } from "../shared/data.service";
import { Router } from "@angular/router";

@Component({
  selector: "checkout",
  templateUrl: "checkout.component.html",
  styleUrls: ['checkout.component.css']
})
export class CheckoutComponent {

  constructor(public data: DataService, private router: Router) {
  }
  public errMessage:string =''
  onCheckout() {
    // TODO
    this.data.checkOut().subscribe(success=>{
      if(success){
        this.router.navigate([''])
      }
      
    }, err=> this.errMessage='Failed to checkout')
  }
}