import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IOrder } from 'src/app/shared/models/order';
import { BreadcrumbService } from 'xng-breadcrumb';
import { OrdersService } from '../orders.service';

@Component({
  selector: 'app-order-detailes',
  templateUrl: './order-detailes.component.html',
  styleUrls: ['./order-detailes.component.scss']
})
export class OrderDetailesComponent implements OnInit {
order: IOrder;


  constructor(private route: ActivatedRoute, private breadCrumbsService: BreadcrumbService,
    private orderService : OrdersService) {
      this.breadCrumbsService.set('@OrderDetailed','')
     }

  ngOnInit(): void {
    this.orderService.getOrderDetailed(+this.route.snapshot.paramMap.get('id')
    ).subscribe((order: IOrder) => {
      this.order = order;
      this.breadCrumbsService.set('@OrderDetailed', `Order# ${order.id} - ${order.status}`);
    }, error => {
      console.log(error)
    })
  }

}
