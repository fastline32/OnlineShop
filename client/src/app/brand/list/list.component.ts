import { Component, OnInit } from '@angular/core';
import { BrandService } from 'src/app/core/services/brand.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  brandList;

  constructor(private brandService: BrandService) { }

  ngOnInit(): void {
    this.getAll()
  }

  getAll() {
    this.brandService.getAllBrands().subscribe(brands => {
      this.brandList = brands;
    })
  }

}
