import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { ListComponent } from './list/list.component';
import { TypeRoutingModule } from './type-routing.module';



@NgModule({
  declarations: [
    ListComponent
  ],
  imports: [
    CommonModule,
    TypeRoutingModule,
    SharedModule
  ]
})
export class TypeModule { }
