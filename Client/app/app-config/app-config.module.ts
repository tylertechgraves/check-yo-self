import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule, MatIconModule, MatListModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { TranslateModule } from '@ngx-translate/core';

import { SharedModule } from '../shared/shared.module';
import { routing } from './app-config.routes';
import { AppConfigComponent } from './app-config.component';

@NgModule({
  imports: [
    routing,
    TranslateModule.forChild(),
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
