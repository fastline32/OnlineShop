import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './list/list.component';

const routes : Routes = [
  {path:'create'},
  {path:'delete'},
  {path:'list',component:ListComponent},
  {path:'edit'}
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class TypeRoutingModule { }
