import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BagEditorComponent } from './pages/bag-editor/bag-editor.component';

const routes: Routes = [
  { path: 'userbags', component: BagEditorComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
