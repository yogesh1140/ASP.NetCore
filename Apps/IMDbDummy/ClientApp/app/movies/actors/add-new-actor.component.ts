import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IActor } from '../shared/actor.model';
import { PATTERN_VALIDATOR, PatternValidator } from '@angular/forms/src/directives/validators';
// import { ISession, restrictedWords } from '../shared/index';

@Component({
  selector: 'app-addnewactor',
  templateUrl: 'add-new-actor.component.html',
  styles: [`
    em {float:right; color:#E05C65; padding-left:10px;}
    .error input, .error select, .error textarea {background-color:#E3C3C5;}
    .error ::-webkit-input-placeholder { color: #999; } 
    .error :-moz-placeholder { color: #999; }
    .error ::-moz-placeholder { color: #999; }
    .error :ms-input-placeholder  { color: #999; }
  `]
})
export class AddNewActorComponent implements OnInit {
  @Output() newActorAdded = new EventEmitter();
  @Output() cancelNewActor = new EventEmitter();

  newActorForm: FormGroup;
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

    this.newActorForm = new FormGroup({
      firstName: this.firstName,
      lastName: this.lastName,
      dob: this.dob,
      bio: this.bio,
      sex: this.sex
    });
  }

  addActor(formValues) {
    let actor: IActor = {
      id: 0,
      firstName: formValues.firstName,
      lastName: formValues.lastName,
      dob: formValues.dob,
      sex: formValues.sex,
      bio: formValues.bio
    };
    // console.log(actor)

    this.newActorAdded.emit(actor);
  }

  cancel() {
    this.cancelNewActor.emit(null);
  }


}