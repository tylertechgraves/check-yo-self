import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { FlexLayoutModule } from '@angular/flex-layout';

import { SharedModule } from '../shared/shared.module';
import { routing } from './app-config.routes';
import { AppConfigComponent } from './app-config.component';

@NgModule({
  imports: [
    routing,
    FlexLayoutModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    SharedModule,
    CommonModule
  ],
  declarations: [AppConfigComponent]
})
export class AppConfigModule { }
