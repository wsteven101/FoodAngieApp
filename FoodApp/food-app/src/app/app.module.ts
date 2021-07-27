import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatCardModule } from '@angular/material/card';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BagComponent } from './views/bag/bag.component';
import { FoodComponent } from './views/food/food.component';
import { MainComponent } from './pages/main/main.component';
import { BagEditorComponent } from './pages/bag-editor/bag-editor.component';
import { HttpClientModule } from '@angular/common/http';
import { BagCardComponent } from './pages/bag-card/bag-card.component';

@NgModule({
  declarations: [
    AppComponent,
    BagComponent,
    FoodComponent,
    MainComponent,
    BagEditorComponent,
    BagCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatCardModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
