import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map, of, ReplaySubject, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IAddress } from '../../shared/models/address';
import { EmailConfirm } from '../../shared/models/EmailConfirm';
import { IUser } from '../../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<IUser>(1);
  currentUser$ = this.currentUserSource.asObservable();
  tokenresp:string;

  constructor(private http: HttpClient, private router: Router) { }

  loadCurrentUser(token: string){
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(this.baseUrl + 'account', {headers}).pipe(
      map((user: IUser) => {
        if(user){
          user.roles = [];
          const roles = this.getRoleByToken(user.token);
          Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  login(values: any){
    return this.http.post(this.baseUrl+'account/login',values).pipe(
      map((user: IUser) => {
        if(user){
          localStorage.setItem('token',user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  register(values: any) {
    return this.http.post(this.baseUrl + 'account/register',values);
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExist(email: string){
    return this.http.get(this.baseUrl+'account/emailexist?email=' + email);
  }

  getUserAddress() {
    return this.http.get<IAddress>(this.baseUrl + 'account/address');
  }

  updateUserAddress(address: IAddress) {
    return this.http.put<IAddress>(this.baseUrl + 'account/address', address);
  }

  getRoleByToken (token: any){
    let _token = token.split('.')[1]
    this.tokenresp=window.atob(_token)
    let _decoded = JSON.parse(this.tokenresp)
    return _decoded.role;
  }

  getToken(){
    return localStorage.getItem('token') || '';
  }
  
  emailConfirm(uid: string,token: string) {
    let params = new HttpParams({ encoder: new EmailConfirm() })
    params = params.append('token', token);
    params = params.append('uid', uid);
    return this.http.get(this.baseUrl+"account/confirm-email",{params: params});
    };
  }