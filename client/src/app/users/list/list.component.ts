import { Component, OnInit } from '@angular/core';
import { IUserList } from 'src/app/shared/models/userList';
import { UserService } from '../user.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  userList: IUserList[];
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.getList()
  }

  getList() {
    this.userService.getAllUsers().subscribe(responce => {
      this.userList = responce
    },error => {
      console.log(error)
    })
  }
}
