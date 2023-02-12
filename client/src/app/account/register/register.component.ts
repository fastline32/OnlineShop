import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { map, of, switchMap, timer } from 'rxjs';
import { AccountService } from 'src/app/core/services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errors: string[];

  constructor(private fb: FormBuilder, private accoutService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      displayName: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
      [this.validateEmailNotTaken()]],
      password: [null, [Validators.required]]
    });
  }

  onSubmit() {
    this.accoutService.register(this.registerForm.value).subscribe(responce => {
      this.router.navigateByUrl('account/please-confirm');
    }, error => {
      console.log(error);
      this.errors = error.errors;
    });
  }

  validateEmailNotTaken() : AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of (null);
          }
          return this.accoutService.checkEmailExist(control.value).pipe(
            map(responce => {
              return responce ? {emailExist: true} : null;
            })
          );
        })
      );
    };
  }
}
