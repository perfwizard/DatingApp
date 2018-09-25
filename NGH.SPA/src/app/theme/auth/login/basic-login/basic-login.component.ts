import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-basic-login',
  templateUrl: './basic-login.component.html',
  styleUrls: ['./basic-login.component.scss']
})
export class BasicLoginComponent implements OnInit {

  constructor(private router: Router) {
  }

  ngOnInit() {
  }

  signIn() {
    console.log('signing in');
    this.router.navigate(['/dashboard/default']);
  }

}
