import { Component, OnDestroy, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter, map, mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  constructor(
    private translate: TranslateService,
    private titleService: Title,
    private router: Router,
    private route: ActivatedRoute
  ) {
    translate.setDefaultLang('ru');
    translate.use('ru');
  }

  ngOnDestroy(): void {}

  ngOnInit(): void {
    // https://toddmotto.com/dynamic-page-titles-angular-2-router-events
    this.router.events
      .pipe(
        filter((event) => event instanceof NavigationEnd),
        map(() => this.route),
        map((route) => {
          while (route.firstChild) {
            route = route.firstChild;
          }
          return route;
        }),
        filter((route) => route.outlet === 'primary'),
        mergeMap((route) => route.data)
      )
      .subscribe((routeData) => {
        const titleDetail = routeData.title ? `${routeData.title}` : '';
        this.titleService.setTitle(`${titleDetail}`);
      });
  }
}
