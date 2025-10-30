import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {
  DxGanttModule,
  DxPopupModule,
  DxFormModule,
} from 'devextreme-angular';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DxGanttModule,
    DxPopupModule,
    DxFormModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
