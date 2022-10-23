import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, of, ReplaySubject, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IAddress } from '../shared/models/address';
import { IUser } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<IUser>(1);
  currentUser$ = this.currentUserSource.asObservable();
  tokenresp:string;
  private _updateMenu = new Subject<void>();

  constructor(private http: HttpClient, private router: Router) { }

  get updateMenu(){
    return this._updateMenu;
  }

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
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          this._updateMenu.next();
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
          this._updateMenu.next()
        }
      })
    );
  }

  register(values: any) {
    return this.http.post(this.baseUrl + 'account/register',values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token',user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
    this._updateMenu.next();
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
    return localStorage.getItem("token") || '';
  }
}
