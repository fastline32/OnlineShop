import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../core/services/account.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {
  emailConfirm: false;
  urlParams: any = {};

  constructor(private route: ActivatedRoute,private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.urlParams.uid = this.route.snapshot.queryParamMap.get('uid');
    this.urlParams.token = this.route.snapshot.queryParamMap.get('token');
    this.confirmEmail();
  }

confirmEmail() {
  const uid = this.route.snapshot.queryParamMap.get('uid');
  const token = this.route.snapshot.queryParamMap.get('token');
  this.accountService.emailConfirm(uid,token).subscribe(() => {
    this.router.navigateByUrl('account/login');
  },error => (
    console.log(error)
  ));
}
}
