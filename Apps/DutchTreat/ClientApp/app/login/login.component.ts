import { Component } from "@angular/core";
import { DataService } from "../shared/data.service";
import { Router } from "@angular/router";

@Component({
    templateUrl:'login.component.html'
})
export class LoginComponent {
    public errMessage:string =''
    constructor(private data: DataService, private router: Router){}
    public creds ={
        username: '',
        password:''
    }
    onLogin(){
        this.data.login(this.creds)
        this.data.login(this.creds).subscribe(success=>{
            if(this.data.order.items.length==0){
                this.router.navigate([''])
                
            }else{
                this.router.navigate(['/checkout'])
            }
        },err=> this.errMessage='Failed to LogIn') 
    
    }
}
