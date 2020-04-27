import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent implements OnInit {
  languageName = 'en';
  model = {};
  year: number = new Date().getFullYear();

  constructor() {}

  ngOnInit() {}
}
