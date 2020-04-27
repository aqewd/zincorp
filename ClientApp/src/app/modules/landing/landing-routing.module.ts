import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LandingPageComponent } from './pages/landing-page/landing-page.component';
import { StartComponent } from './components/start/start.component';

const routes: Routes = [
  {
    path: '',
    component: LandingPageComponent,
    data: {
      title: 'Start page'
    },
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'start/en'
      },
      {
        path: 'start/:language',
        component: StartComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LandingRoutingModule {}
