import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http';
import { NgModule, APP_INITIALIZER } from '@angular/core';

import { AppComponent } from './app.component';
import { HeaderComponent } from './user/reusable/header/header.component';
import { ArticlePreviewComponent } from './user/reusable/article-preview/article-preview.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TagsListComponent } from './user/reusable/tags-list/tags-list.component';
import { RatingComponent } from './user/reusable/rating/rating.component';
import { EditableTagsListComponent } from './user/reusable/editable-tags-list/editable-tags-list.component';
import { CommentListComponent } from './user/reusable/comment-list/comment-list.component';
import { CommentComponent } from './user/reusable/comment-list/comment/comment.component';
import { MainPageComponent } from './user/main-page/main-page.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { ProfileComponent } from './user/profile/profile.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginService } from './services/login-service';
import { UserService } from './services/user-service';
import { UserIdGuard } from './guards/user-id';
import { WriterIdGuard } from './guards/writer-id.guard';
import { AuthGuard } from './guards/auth.guard';
import { AddCommentComponent } from './user/article/add-comment/add-comment.component';
import { FiltersComponent } from './user/main-page/filters/filters.component';
import { TopicsFilterComponent } from './user/main-page/filters/topics-filter/topics-filter.component';
import { RatingFilterComponent } from './user/main-page/filters/rating-filter/rating-filter.component';
import { TagsFilterComponent } from './user/main-page/filters/tags-filter/tags-filter.component';
import { SearchComponent } from './user/main-page/search/search.component';
import { DateFilterComponent } from './user/main-page/filters/date-filter/date-filter.component';
import { CreateArticleComponent } from './user/article/create-article/create-article.component';
import { ArticleComponent } from './user/article/article.component';
import { EditProfileComponent } from './user/profile/edit-profile/edit-profile.component';
import { EditArticleComponent } from './user/article/edit-article/edit-article.component';
import { AdminGuard } from './guards/admin.guard';
import { SharedModule } from './shared/shared.module';
import { AuthInterceptorProvider } from './providers/auth-interceptor';
import { OAuthModule, OAuthService } from 'angular-oauth2-oidc';
import { EditorModule } from "@tinymce/tinymce-angular";


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
    TagsListComponent,
    RatingComponent,
    EditArticleComponent,
    EditableTagsListComponent,
    CommentListComponent,
    CommentComponent,
    AddCommentComponent
  ],
  imports: [
    SharedModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    EditorModule,
    OAuthModule.forRoot()
  ],
  providers: [
    LoginService,
    UserService,
    UserIdGuard,
    WriterIdGuard,
    AuthGuard,
    AdminGuard,
    AuthInterceptorProvider,
    OAuthService
  ],
  bootstrap: [AppComponent]
})


export class AppModule {
  constructor() {
  }

}
