import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule} from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Pipe, NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HeaderComponent } from './shared/header/header.component';
import { ArticlePreviewComponent } from './shared/article-preview/article-preview.component';
import { FiltersComponent } from './main-page/filters/filters.component';
import { TopicsFilterComponent } from './main-page/filters/topics-filter/topics-filter.component';
import { RatingFilterComponent } from './main-page/filters/rating-filter/rating-filter.component';
import { TagsFilterComponent } from './main-page/filters/tags-filter/tags-filter.component';
import { SearchComponent } from './main-page/search/search.component';
import { DateFilterComponent } from './main-page/filters/date-filter/date-filter.component';
import { MainPageComponent } from './main-page/main-page.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { ProfileComponent } from './profile/profile.component';
import { ArticleComponent } from './article/article.component';
import { CreateArticleComponent } from './article/create-article/create-article.component';
import { EditProfileComponent } from './profile/edit-profile/edit-profile.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { MaterialModule } from './material/material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TagsListComponent } from './shared/tags-list/tags-list.component';
import { RatingComponent } from './shared/rating/rating.component';
import { EditArticleComponent } from './article/edit-article/edit-article.component';
import { EditableTagsListComponent } from './shared/editable-tags-list/editable-tags-list.component';
import { LoginService } from '../services/login-service';
import { EditProfileGuard } from '../guards/edit-profile.guard';
import { EditArticleGuard } from '../guards/edit-article.guard';
import { AuthGuard } from '../guards/auth.guard';
import { UserService } from '../services/user-service';
import { CommentListComponent } from './shared/comment-list/comment-list.component';
import { CommentComponent } from './shared/comment-list/comment/comment.component';
import { EditDialogComponent } from './shared/edit-dialog/edit-dialog.component';
import { DeleteDialogComponent } from './shared/delete-dialog/delete-dialog.component';
import { AlertDialogComponent } from './shared/alert-dialog/alert-dialog.component';
import { AddCommentComponent } from './article/add-comment/add-comment.component';

const routes: Routes = [
  { path: 'main', component: MainPageComponent},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegistrationComponent},
  { path: 'profile/:id', component: ProfileComponent },
  { path: 'profile/:id/edit', component: EditProfileComponent, canActivate: [EditProfileGuard]},
  { path: 'article/create', component: CreateArticleComponent, canActivate: [AuthGuard]},
  { path: 'article/:id', component: ArticleComponent },
  { path: 'article/:id/edit', component: EditArticleComponent, canActivate: [EditArticleGuard]},
  { path: '', redirectTo: 'main', pathMatch: 'full'},
  { path: '**', component: NotFoundComponent }
]

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    ArticlePreviewComponent,
    FiltersComponent,
    TopicsFilterComponent,
    RatingFilterComponent,
    TagsFilterComponent,
    SearchComponent,
    DateFilterComponent,
    MainPageComponent,
    LoginComponent,
    RegistrationComponent,
    ProfileComponent,
    ArticleComponent,
    CreateArticleComponent,
    EditProfileComponent,
    NotFoundComponent,
    TagsListComponent,
    RatingComponent,
    EditArticleComponent,
    EditableTagsListComponent,
    CommentListComponent,
    CommentComponent,
    EditDialogComponent,
    DeleteDialogComponent,
    AlertDialogComponent,
    AddCommentComponent
  ],
  entryComponents: [
    EditDialogComponent,
    DeleteDialogComponent,
    AlertDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    MaterialModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(routes),
  ],
  providers: [LoginService, UserService, EditProfileGuard, EditArticleGuard, AuthGuard],
  bootstrap: [AppComponent]
})


export class AppModule {


}
