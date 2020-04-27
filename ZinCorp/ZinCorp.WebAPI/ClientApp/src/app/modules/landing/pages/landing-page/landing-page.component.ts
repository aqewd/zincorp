import { Component, OnInit } from '@angular/core';
import {CommonService} from '../../../../core/services/common.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent implements OnInit {
  languageName = 'en';
  model = {};
  year: number = new Date().getFullYear();

  constructor(private commonService: CommonService) {
  }

  ngOnInit() {
    this.get();
  }

  get() {
    return this.commonService.get();
  }
}
