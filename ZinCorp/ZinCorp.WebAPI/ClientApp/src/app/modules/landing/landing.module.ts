import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LandingRoutingModule } from './landing-routing.module';
import { StartComponent } from './components/start/start.component';
import { LandingPageComponent } from './pages/landing-page/landing-page.component';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  declarations: [StartComponent, LandingPageComponent],
  imports: [CommonModule, LandingRoutingModule, SharedModule]
})
export class LandingModule {}
