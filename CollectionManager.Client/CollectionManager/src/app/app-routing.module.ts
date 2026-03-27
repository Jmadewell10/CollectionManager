import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CollectionsComponent } from './collections/collections.component';
import { HomeComponent } from './home/home.component';
import { authGuard } from './guards/auth.guard';
import { LoggedOutHomeComponent } from './logged-out-home/logged-out-home.component';
import { loggedOutGuard } from './guards/logged-out.guard';

const routes: Routes = [
  {path: '', component: LoggedOutHomeComponent, canActivate: [loggedOutGuard]},
  {path: 'home', component: HomeComponent, canActivate: [authGuard]},
  {path: 'collections', component: CollectionsComponent, canActivate: [authGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
