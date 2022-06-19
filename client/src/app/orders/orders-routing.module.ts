import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { OrdersComponent } from './orders.component';
import { OrderDetailesComponent } from './order-detailes/order-detailes.component';

const routes: Routes = [
  {path: '', component: OrdersComponent},
  {path: ':id', component:OrderDetailesComponent, data: {breadcrumb:{alias:'OrderDetailed'}}}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class OrdersRoutingModule { }
