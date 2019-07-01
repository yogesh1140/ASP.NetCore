import { Component ,Inject, OnInit} from '@angular/core';
import * as $ from 'jquery'
@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit{
    data =[
1,2,3,4,5
    ]
     
    constructor(){

    }
    ngOnInit(){
        // $('.autoplay').slick({
        //    slidesToShow: 4,
        //    slidesToScroll: 1,
        //    autoplay: true,
        //    autoplaySpeed: 2000,
        //});
    }

    
}
