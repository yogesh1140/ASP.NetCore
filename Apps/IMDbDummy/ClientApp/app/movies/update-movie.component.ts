import { Component, Input, OnInit, Output, EventEmitter } from "@angular/core";
import { IMovie } from "./shared/movie.model";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { UploadService } from "./shared/upload.service";
import { ToastrService } from "ngx-toastr";
import { Router } from "@angular/router";
import { IProducer } from "./shared/producer.model";
import { MovieService } from "./shared/movie.service";

@Component({
    selector: 'app-updatemovie',
    templateUrl: 'update-movie.component.html',
    styles: [`
    em {float:right; color:#E05C65; padding-left:10px;}
    .error input, .error select, .error textarea {background-color:#E3C3C5;}
    .error ::-webkit-input-placeholder { color: #999; } 
    .error :-moz-placeholder { color: #999; }
    .error ::-moz-placeholder { color: #999; }
    .error :ms-input-placeholder  { color: #999; }
  `]

})
export class UpdateMovieComponent implements OnInit {
    @Input() movie: IMovie
    @Output() cancelUpdateMovie = new EventEmitter()
    @Output() updateMovie = new EventEmitter()

    addProducerMode: boolean = false
    imgSrc: string

    producersList: IProducer[]


    updatedMovie: IMovie


    updateMovieForm: FormGroup
    name: FormControl
    yearOfRelease: FormControl
    plot: FormControl
    producer: FormControl
    posterUrl: FormControl

    constructor(private movieService: MovieService, private uploadService: UploadService,
        private toastr: ToastrService,
        private router: Router) { }

    ngOnInit() {
        this.updatedMovie = {
            name : this.movie.name,
            yearOfRelease: this.movie.yearOfRelease,
            plot: this.movie.plot,
            id :this.movie.id,
            posterUrl: this.movie.posterUrl,
            producer: {
                id :this.movie.producer.id,
                firstName: this.movie.producer.firstName,
                lastName: this.movie.producer.lastName,
                sex: this.movie.producer.sex,
                bio: this.movie.producer.bio,
                dob: this.movie.producer.dob
            },
            movieActors: []
        } 
        this.imgSrc = this.movie.posterUrl

        this.name = new FormControl(this.updatedMovie.name, [Validators.required]);
        this.yearOfRelease = new FormControl(this.updatedMovie.yearOfRelease, [Validators.required, Validators.pattern('[0-9]{4}')]);
        this.plot = new FormControl(this.updatedMovie.plot, [Validators.required, , Validators.minLength(20), Validators.maxLength(500)]);
        this.producer = new FormControl(this.updatedMovie.producer.id, [Validators.required]);
        this.posterUrl = new FormControl(this.updatedMovie.posterUrl)

        this.updateMovieForm = new FormGroup({
            name: this.name,
            yearOfRelease: this.yearOfRelease,
            plot: this.plot,
            producer: this.producer,
            posterUrl: this.posterUrl
        })

        this.movieService.getProducers().subscribe(
            data => //console.log(data) ,
            {
                this.producersList = data

            },
            err => this.toastr.error(err.error)
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
    cancelNewProducer() {
        this.addProducerMode = false
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
        this.cancelUpdateMovie.emit(null)
    }
    updateMovieDetails(formvalues: any) {
        this.updatedMovie.name = formvalues.name
        this.updatedMovie.yearOfRelease= formvalues.yearOfRelease
        this.updatedMovie.plot= formvalues.plot
        this.updatedMovie.producer.id =formvalues.producer
        this.updatedMovie.posterUrl= this.imgSrc
        // console.log(formvalues,this.movie, this.updatedMovie)
        if (this.updatedMovie.name != this.movie.name || this.updatedMovie.plot != this.movie.plot || this.updatedMovie.posterUrl != this.movie.posterUrl || this.updatedMovie.producer.id != this.movie.producer.id)
            this.updateMovie.emit(this.updatedMovie)
        else {
            this.toastr.info("No updations made")
            this.cancelUpdateMovie.emit(null)

        }
    }

}