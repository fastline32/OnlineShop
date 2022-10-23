import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes } from '@angular/router';

const routes : Routes = [
  {path:'create'},
  {path:'delete'}
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class BrandRoutingModule { }
