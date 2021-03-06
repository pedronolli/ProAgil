import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { TooltipModule, BsDropdownModule, ModalModule } from 'ngx-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { EventoService } from './_services/evento.service';

import { NavComponent } from './nav/nav.component';
import { EventosComponent } from './eventos/eventos.component';
import { AppComponent } from './app.component';

import { DateTimeFormatPipe } from './_helps/DateTimeFormat.pipe';

@NgModule({
   declarations: [
      AppComponent,
      EventosComponent,
      NavComponent,
      DateTimeFormatPipe
   ],
   imports: [
      BrowserModule,
      BsDropdownModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      AppRoutingModule,
      HttpClientModule,
      FormsModule
   ],
   providers: [EventoService],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
