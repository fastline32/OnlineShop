import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { BrandRoutingModule } from './brand-routing.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    ListComponent
  ],
  imports: [
    CommonModule,
    BrandRoutingModule,
    SharedModule
  ]
})
export class BrandModule { }
