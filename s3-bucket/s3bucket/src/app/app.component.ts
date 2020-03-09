import { Component, OnInit } from '@angular/core';
import { RestService } from './core/services/rest/rest.services';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 's3bucket';
  values;
  constructor(private rest: RestService) {

  }

  ngOnInit(): void {
    this.rest.get('api/values').subscribe(rest => {
      console.log(rest);
      this.values = rest;
    });
  }
}
