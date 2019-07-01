import { Component, Input, OnInit, Output, EventEmitter, OnChanges } from "@angular/core";
import { IActor } from "../shared/actor.model";

@Component({
    selector: 'app-actorslist',
    templateUrl: 'actors-list.component.html'
})
export class ActorsListComponent implements OnInit, OnChanges {
    @Input() actors: IActor[]
    @Input() filterBy: string
    filteredActors: IActor[] = []
    @Output() deleteActorFromMovie = new EventEmitter()
    ngOnInit() {
        if (this.filterBy == 'Actor')
            this.filteredActors = this.actors.filter(act => act.sex == 'Male')
        else if (this.filterBy == 'Actress')
            this.filteredActors = this.actors.filter(act => act.sex == 'Female')
        else this.filteredActors = this.actors
        // console.log(this.filterBy , this.filteredActors)
    }
ngOnChanges(){
    this.ngOnInit()
}
    deleteActor(actor: IActor) {
        this.deleteActorFromMovie.emit(actor)
    }
}