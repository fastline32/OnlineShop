import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IUserList } from '../shared/models/userList';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  listOfUsers: IUserList[] = []
  baseApiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllUsers() {
      return this.http.get<IUserList[]>(this.baseApiUrl+'user/list').pipe(map(
      response => {
        this.listOfUsers = response
        return response
      }
    ))
  }
}
