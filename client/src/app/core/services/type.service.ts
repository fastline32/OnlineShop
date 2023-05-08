import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TypeService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllTypes() {
    return this.http.get(this.baseUrl+'type/get-all')
  }
}
