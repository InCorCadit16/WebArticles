import { NgModule } from '@angular/core';
import { AdminComponent } from './admin.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { NotFoundComponent } from '../user/not-found/not-found.component';
import { UsersTableComponent } from './users-table/users-table.component';
import { TopicsMenuComponent } from './topics-menu/topics-menu.component';

export const routes: Routes = [
  { path: '', component: AdminComponent },
  { path: '**', component: NotFoundComponent}
]

@NgModule({
  declarations: [
    AdminComponent,
    UsersTableComponent,
    TopicsMenuComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes),
  ],
})
export class AdminModule { }
