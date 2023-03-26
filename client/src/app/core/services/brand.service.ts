import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BrandService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllBrands() {
    return this.http.get(this.baseUrl+'brand/get-all');
  }
}
