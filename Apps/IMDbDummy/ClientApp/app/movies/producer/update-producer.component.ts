import { Component, OnInit, Output, EventEmitter, Input, OnChanges } from "@angular/core";
import { MovieService } from "../shared/movie.service";
import { ToastrService } from "ngx-toastr";
import { IProducer } from "../shared/producer.model";

@Component({
    selector: 'app-updateproducer',
    templateUrl: 'update-producer.component.html',
    
})
export class UpdateProducerComponent implements OnInit {
    @Output() addProducer = new EventEmitter();
    @Output() cancelAddProducer= new EventEmitter();
    producers: IProducer[]
    @Input() existingProducer: IProducer
    @Input() movieId: number
    addProduceMode: boolean
    errorMessage: any
    isError: boolean
    selectedActor: IProducer 
    constructor(private movieService: MovieService, private toastr: ToastrService) {

    }
    updateProducer(selectedActorId: any) {
        // console.log(this.movieId + ' ' + formValues)        
        this.addProducer.emit(this.producers.find(act=>act.id == selectedActorId))
    }
    ngOnInit() {
        this.isError=false
        this.addProduceMode = false;
        this.movieService.getProducers().subscribe((res) => {
            this.producers = res
            this.producers = this.producers.filter((act) =>
                this.existingProducer.id != act.id)
        })
    }

    AddnewProducer() {
        this.addProduceMode = true;
        this.isError=false
    }
    cancel() {
        this.isError=false;

        this.cancelAddProducer.emit(null)
    }
    cancelNewProducer() {
        this.addProduceMode = false;
    }
    saveNewProducer(producer: IProducer) {
        this.movieService.addNewProducer(producer).subscribe(
            data => {
                this.addProduceMode = false;
                this.ngOnInit()
                this.toastr.success('New Producer added')
            
            },

            error => {
                this.isError =true
                this.toastr.error( error.error)
                // console.log('error: ' + this.errorMessage.error)
                
            })

    }
    
}
