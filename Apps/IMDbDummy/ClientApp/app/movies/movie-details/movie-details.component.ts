import { Component, Inject, OnInit } from "@angular/core";
import { IMovie } from "../shared/movie.model";
import { MovieService } from "../shared/movie.service";
import {  ActivatedRoute, Router } from "@angular/router";
import { IActor } from "../shared/actor.model";
import { ToastrService } from "ngx-toastr";

@Component({
    templateUrl:'movie-details.component.html',
    styles: [`
    div span{
        color: darkslategrey;
    }
    `]
    
})
export class MovieDetailsComponent implements OnInit {
    movie: IMovie
    addMode :boolean
    updateMovieMode: boolean
    actorList :IActor[] =[]
    filterBy: string ='All'
    constructor (private movieService: MovieService, private route: ActivatedRoute , private toastr: ToastrService, private router: Router){

    }
    deleteMovie(){
        this.movieService.deleteMovie(this.movie.id).subscribe(
            error=>{
                this.toastr.error(error.error)
            },
            success=> {
                this.toastr.success('Movie Deleted: '+this.movie.name)
                this.router.navigate(["/movies"])    
        })
    }

    getactors(movie: any){
        return movie.movieActors
       // return movie.movieActors.map(ma=> ma.actor !=null ? ma.actor : ma)
    }
    addActor(){
        this.addMode =true
    }
    ngOnInit(){
        this.addMode = false
        this.route.data.forEach((data) => {
            //console.log(data['movie'])
            this.movie = data['movie'];
            this.addMode = false;
           this. movie.movieActors.forEach((element: any) => {
              // console.log(element)
            this.actorList.push(element.actor)  
            });
           // console.log(this.actorList)

          });
         

    }
    cancelUpdateMovie(){
        this.updateMovieMode =false
    }
    updateMovie(movie: IMovie){
        this.movieService.updateMovie(movie).subscribe(
            data=>{
                
                this.updateMovieMode=false
                this.toastr.success("Movie details updated")
                this.movie =data
                //this.router.navigate([`/movies/${movie.id}`])
            },
            error=>{
                this.toastr.error(error.error)
                
            }
        ) 
        
    }
    cancelAddActor(){
        this.addMode=false;
    }
    addActorToMovie(actor: IActor){
      //  console.log('actor: ', actor)
        this.movieService.addActorToMovie(this.movie, actor).subscribe(
            data=>{
                this.actorList.push(actor)
                this.addMode =false
                this.toastr.success("Actor Added from movie")

            },
            error=>{
                this.toastr.error(error.error)
                
            }
        )
        
    }

    deleteActorFromMovie(actor: IActor){
      //  console.log('actor: ', actor)
        this.movieService.deleteActorFromMovie(this.movie, actor.id).subscribe(
            data=>{
                this.actorList = this.actorList.filter(al=> al.id!=actor.id)
                this.addMode =false
                this.toastr.success("Actor deleted from movie")
            },
            error=>{
                this.toastr.error(error.error)
               // console.log(error.error)
            }
        )
        
    }

}

