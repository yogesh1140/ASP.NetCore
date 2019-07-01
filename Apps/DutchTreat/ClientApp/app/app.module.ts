import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router'
import { FormsModule } from '@angular/forms'

import { AppComponent } from './app.component';
import { ProductListComponent } from './shop/product-list.component';
import { DataService } from './shared/data.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { CartComponent } from './shop/cart.component';
import { ShopComponent } from './shop/shop.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { LoginComponent } from './login/login.component';
import { TokenInterceptor } from './shared/token.interceptor';

let routes =[
    {path:'', component: ShopComponent},
    {path :'checkout', component: CheckoutComponent},
    {path:'login', component: LoginComponent}
]
@NgModule({
  declarations: [
      AppComponent,
      ProductListComponent,
      CartComponent,
      ShopComponent,
      CheckoutComponent,
      LoginComponent
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      RouterModule.forRoot(routes, {
        useHash: true,
       // enableTracing: true //for Debugging of the Routes

      }),
      FormsModule
  ],
    providers: [
        DataService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: TokenInterceptor,
            multi: true
          }
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
