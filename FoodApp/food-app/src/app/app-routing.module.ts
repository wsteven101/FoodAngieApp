import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BagEditorComponent } from './pages/bag-editor/bag-editor.component';
import { BagComponent } from './views/bag/bag.component';

const routes: Routes = [
  {
    path: 'foodapp', children: [
      { path: 'userbags', component: BagEditorComponent },
      { path: 'bag', component: BagComponent }
    ]
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
