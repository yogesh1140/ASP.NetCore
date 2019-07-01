import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IActor } from '../shared/actor.model';
import { PATTERN_VALIDATOR, PatternValidator } from '@angular/forms/src/directives/validators';
import { IProducer } from '../shared/producer.model';
// import { ISession, restrictedWords } from '../shared/index';

@Component({
  selector: 'app-addnewproducer',
  templateUrl: 'add-new-producer.component.html',
  styles: [`
    em {float:right; color:#E05C65; padding-left:10px;}
    .error input, .error select, .error textarea {background-color:#E3C3C5;}
    .error ::-webkit-input-placeholder { color: #999; } 
    .error :-moz-placeholder { color: #999; }
    .error ::-moz-placeholder { color: #999; }
    .error :ms-input-placeholder  { color: #999; }
  `]
})
export class AddNewProducerComponent implements OnInit {
  @Output() newProducerAdded = new EventEmitter();
  @Output() cancelNewProducer = new EventEmitter();

  newProducerForm: FormGroup;
  firstName: FormControl;
  lastName: FormControl;
  dob: FormControl;
  bio: FormControl;
  sex: FormControl;

  ngOnInit() {
    this.firstName = new FormControl('', [Validators.required, Validators.pattern('[A-Za-z]*')]);
    this.lastName = new FormControl('', [Validators.required, Validators.pattern('[A-Za-z]*')]);
    this.dob = new FormControl('', Validators.required);
    this.bio = new FormControl('',[ Validators.required, Validators.minLength(20) ,  Validators.maxLength(500)] );
    this.sex = new FormControl('', Validators.required);

    this.newProducerForm = new FormGroup({
      firstName: this.firstName,
      lastName: this.lastName,
      dob: this.dob,
      bio: this.bio,
      sex: this.sex
    });
  }

  addProducer(formValues) {
    let producer: IProducer = {
      id: 0,
      firstName: formValues.firstName,
      lastName: formValues.lastName,
      dob: formValues.dob,
      sex: formValues.sex,
      bio: formValues.bio
    };
    // console.log(actor)

    this.newProducerAdded.emit(producer);
  }

  cancel() {
    this.cancelNewProducer.emit(null);
  }


}