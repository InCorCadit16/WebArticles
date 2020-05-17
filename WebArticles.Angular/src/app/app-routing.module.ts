import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from './user/main-page/main-page.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { ProfileComponent } from './user/profile/profile.component';
import { EditProfileComponent } from './user/profile/edit-profile/edit-profile.component';
import { CreateArticleComponent } from './user/article/create-article/create-article.component';
import { UserIdGuard } from './guards/user-id';
import { AuthGuard } from './guards/auth.guard';
import { ArticleComponent } from './user/article/article.component';
import { EditArticleComponent } from './user/article/edit-article/edit-article.component';
import { WriterIdGuard } from './guards/writer-id.guard';
import { NotFoundComponent } from './shared/not-found/not-found.component';
import { AdminGuard } from './guards/admin.guard';

const routes: Routes = [
    { path: 'main', component: MainPageComponent},
    { path: 'login', component: LoginComponent},
    { path: 'register', component: RegistrationComponent},
    { path: 'profile/:id', component: ProfileComponent },
    { path: 'profile/:id/edit', component: EditProfileComponent, canActivate: [UserIdGuard]},
    { path: 'article/create', component: CreateArticleComponent, canActivate: [AuthGuard]},
    { path: 'article/:id', component: ArticleComponent },
    { path: 'article/:id/edit', component: EditArticleComponent, canActivate: [WriterIdGuard]},
    { path: 'admin', loadChildren: 'src/app/admin/admin.module#AdminModule', canActivate: [AdminGuard] },
    { path: '', redirectTo: 'main', pathMatch: 'full'},
    { path: '**', component: NotFoundComponent }
  ]


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
