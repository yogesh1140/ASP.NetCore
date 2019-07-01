import { Component, OnInit, Output, EventEmitter, Input, OnChanges } from "@angular/core";
import { MovieService } from "../shared/movie.service";
import { IActor } from "../shared/actor.model";
import { ToastrService } from "ngx-toastr";

@Component({
    selector: 'app-addactor',
    templateUrl: 'add-actor.component.html',
    
})
export class AddActorComponent implements OnInit {
    @Output() addActor = new EventEmitter();
    @Output() cancelAddActor = new EventEmitter();
    actors: IActor[]
    @Input() existingActors: IActor[]
    @Input() movieId: number
    addActorMode: boolean
    errorMessage: any
    isError: boolean
    selectedActor: IActor 
    constructor(private movieService: MovieService, private toastr: ToastrService) {

    }
    addActorToMovie(selectedActorId: any) {
        // console.log(this.movieId + ' ' + formValues)        
        this.addActor.emit(this.actors.find(act=>act.id == selectedActorId))
    }
    ngOnInit() {
        this.isError=false
        this.addActorMode = false;
        this.movieService.getActors().subscribe((res) => {
            this.actors = res
            this.actors = this.actors.filter((act) =>
                this.existingActors.find((ac:any) => ac.id == act.id) == null)
        })
    }

    AddNewActor() {
        this.addActorMode = true;
        this.isError=false
    }
    cancel() {
        this.isError=false;

        this.cancelAddActor.emit(null)
    }
    cancelNewActor() {
        this.addActorMode = false;
    }
    saveNewActor(actor: IActor) {
        this.movieService.addNewActor(actor).subscribe(
            data => {
                this.addActorMode = false;
                this.ngOnInit()
                this.toastr.success('New Actor added')
                this.selectedActor= actor
            },

            error => {
                this.isError =true
                this.toastr.error( error.error)
                // console.log('error: ' + this.errorMessage.error)
                
            })

    }
    
}
