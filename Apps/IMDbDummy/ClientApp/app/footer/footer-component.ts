import { Component } from "@angular/core";

@Component({
    selector: 'app-footer',
    template: `
    <footer class="container">
    <div class="text-center well" style="margin:0">IMDb Dummy &copy;2018</div>
  </footer>
    `,
    styles:[`
    footer {
        position:fixed;
        min-height: 50px;
        max-height: 50px;

        bottom:0;
        padding:0;
    }
    `]
})
export class FooterComponent {

}
