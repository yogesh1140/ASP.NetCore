import { Component, OnInit } from "@angular/core";
import { MovieService } from "./shared/movie.service";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { IProducer } from "./shared/producer.model";
import { ToastrService } from "ngx-toastr";
import { UploadService } from "./shared/upload.service";
import { HttpRequest, HttpEventType, HttpResponse, HttpClient } from "@angular/common/http";
import { IActor } from "./shared/actor.model";
import { IMovie } from './shared/movie.model'
import { Router } from "@angular/router";


@Component({
    templateUrl: 'add-movie.component.html',
    styles: [`
    em {float:right; color:#E05C65; padding-left:10px;}
    .error input, .error select, .error textarea {background-color:#E3C3C5;}
    .error ::-webkit-input-placeholder { color: #999; } 
    .error :-moz-placeholder { color: #999; }
    .error ::-moz-placeholder { color: #999; }
    .error :ms-input-placeholder  { color: #999; }
  `]
})
export class AddMovieComponent implements OnInit {

    addMovieForm: FormGroup
    name: FormControl;
    yearOfRelease: FormControl;
    plot: FormControl;
    producer: FormControl;
    actor: FormControl
    posterUrl: FormControl

    dropdownList = [];
    selectedItems = [];
    dropdownSettings = {};

    addActorMode: boolean = false
    addProducerMode: boolean = false

    imgSrc: string
    producersList: IProducer[]
    actorsList: IActor[]
    constructor(private movieService: MovieService,
        private uploadService: UploadService,         
        private toastr: ToastrService,
        private router: Router
    ) { }


    ngOnInit() {
        this.addActorMode = false
        this.addProducerMode = false
        this.name = new FormControl('', [Validators.required]);
        this.yearOfRelease = new FormControl('', [Validators.required, Validators.pattern('[0-9]{4}')]);
        this.plot = new FormControl('', [Validators.required, , Validators.minLength(20), Validators.maxLength(500)]);
        this.producer = new FormControl('', [Validators.required]);
        this.actor = new FormControl('', [Validators.required]);
        this.posterUrl = new FormControl('', Validators.required)

        this.addMovieForm = new FormGroup({
            name: this.name,
            yearOfRelease: this.yearOfRelease,
            plot: this.plot,
            producer: this.producer,
            posterUrl: this.posterUrl,
            actor: this.actor
        });
        this.movieService.getProducers().subscribe(
            data => //console.log(data) ,
            {
                this.producersList = data

            },
            err => this.toastr.error(err.error)
        )
        this.movieService.getActors().subscribe(
            data => //console.log(data) ,
            {
                this.actorsList = data
                this.updateActorsList()

            },
            err => this.toastr.error(err.error)
        )



        this.selectedItems = [];
        this.dropdownSettings = {
            singleSelection: false,
            text: "Select Actor",
            selectAllText: 'Select All',
            unSelectAllText: 'UnSelect All',
            enableSearchFilter: true,
            classes: "myclass custom-class"
        };

    }
    addMovie(formvalue: any) {
        let movie: any = {
            id: 0,
            name: formvalue.name,
            plot: formvalue.plot,
            yearOfRelease: formvalue.yearOfRelease,
            movieActors: [],
            producer: this.producersList.find(pr => pr.id == formvalue.producer),
            posterUrl: this.imgSrc
        }
        //console.log(formvalue)
        this.selectedItems.forEach(ele => movie.movieActors.push({ actorId: ele.id })),
            this.movieService.addNewMovie(movie).subscribe(
                data =>{

                 this.toastr.success('New Movie Added')
                 this.router.navigate(['/movies'])                
                },
                err => this.toastr.error(err.error)
            )

    }
    saveNewActor(actor: IActor) {

        this.movieService.addNewActor(actor).subscribe(
            data => {
                this.actorsList.push(actor)
                this.toastr.success('New Actor Added')
                this.selectedItems.push({id: data.id, itemName : data.firstName+ ' '+ data.lastName})
                this.addActorMode = false
            },
            err =>
                this.toastr.error(err.error)
        )

    }
    saveNewProducer(producer: IProducer) {
        this.movieService.addNewProducer(producer).subscribe(
            data => {
                this.producersList.push(data)
                this.toastr.success("New Producer Added")
                this.addProducerMode = false
            },
            err => this.toastr.error(err.error)
        )

    }
    updateActorsList() {
        this.actorsList.forEach(ele => this.dropdownList.push({ id: ele.id, itemName: ele.firstName + ' ' + ele.lastName }))

    }
    cancelNewProducer() {
        this.addProducerMode = false
    }
    cancelNewActor() {
        this.addActorMode = false
    }
    uploadFiles(files) {
        console.log('length:', files)
        if (files.length === 0)
            return;

        const formData = new FormData();

        for (let file of files) {
            var ext = file.name.substring(file.name.lastIndexOf('.')).toLowerCase()
            if (ext !== '.jpg' && ext != '.png' && ext != '.bmp' && ext != '.tiff') {
                this.toastr.error('Invalid file selected for poster')
                return
            }

            formData.append(file.name, file);
        }

        this.uploadService.uploadFile(formData).subscribe(
            data => {

                console.log(data.filePath)
                this.imgSrc = data.filePath
            }
            ,
            err => this.toastr.error(err.error)

        )
    }

    cancel() {
        this.router.navigate(['/movies'])
    }

}