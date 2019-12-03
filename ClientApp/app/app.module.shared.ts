import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from '../app/services/auth.interceptor';
import { AuthResponseInterceptor } from '../app/services/auth.respone.interceptor';
import { RouterModule } from '@angular/router';
import { AuthService } from './services/auth.service';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { QuizListComponent } from './components/quiz/quiz-list.component';
import { QuizComponent } from './components/quiz/quiz.component';
import { AboutComponent } from './components/about/about.component';
import { LoginComponent } from './components/login/login.component';
import { PageNotFoundComponent } from './components/pagenotfound/pagenotfound.component';
import { QuizEditComponent } from './components/quiz/quiz-edit.component';
import { QuestionListComponent } from './components/question/question-list.component';
import { QuestionEditComponent } from './components/question/question-edit.component';
import { AnswerListComponent } from './components/answer/answer-list.compnent';
import { AnswerEditComponent } from './components/answer/answer-edit.component';
import { ResultListComponent } from './components/result/result-list.component';
import { ResultEditComponent } from './components/result/result-edit.component';
import { QuizSearchComponent } from './components/quiz/quiz-search.component';
import { RegisterComponent } from './components/user/register.component';
import { QuizTakeComponent } from './components/quiz-take/quiz-take.component';
import { QuestionTakeComponent } from './components/quiz-take/question-take.component';
import { ResultTakeComponent } from './components/quiz-take/result-take.component';
import { AnswerTakeComponent } from './components/quiz-take/answer-take.component';

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
		HomeComponent,
		QuizListComponent,
		QuizEditComponent,
		QuizComponent,
		QuizSearchComponent,
		QuestionListComponent,
		QuestionEditComponent,
		QuizTakeComponent,
		QuestionTakeComponent,
		AnswerTakeComponent,
		ResultTakeComponent,
		AnswerListComponent,
		AnswerEditComponent,
		ResultListComponent,
		ResultEditComponent,
		AboutComponent,
		LoginComponent,
		RegisterComponent,
		PageNotFoundComponent
	],
	imports: [
		CommonModule,
		HttpClientModule,
		FormsModule,
		ReactiveFormsModule,
		RouterModule.forRoot([
			{ path: '', redirectTo: 'home', pathMatch: 'full' },
			{ path: 'home', component: HomeComponent },
			{ path: 'quiz/create', component: QuizEditComponent },
			{ path: 'quiz/edit/:id', component: QuizEditComponent },
			{ path: 'quiz/:id', component: QuizComponent },
			{ path: 'quiz/take/:id', component: QuizTakeComponent },
		  {path: 'question/create/:id', component: QuestionEditComponent},
		  { path: 'question/edit/:id', component: QuestionEditComponent },
		  { path: 'answer/create/:id', component: AnswerEditComponent },
		  { path: 'answer/edit/:id', component: AnswerEditComponent },
		  { path: 'result/edit/:id', component: ResultEditComponent },
		  { path: 'result/create/:id', component: ResultEditComponent },
		  { path: 'home', component: HomeComponent},
		  { path: 'about', component: AboutComponent },
		  { path: 'login', component: LoginComponent },
		  { path: 'register', component: RegisterComponent},
		  {path: '**', component: PageNotFoundComponent}
      ])
	],
	providers: [
		AuthService,	{
			provide: HTTP_INTERCEPTORS,
			useClass: AuthInterceptor,
			multi: true
		},
		{
			provide: HTTP_INTERCEPTORS,
			useClass: AuthResponseInterceptor,
			multi: true
		}
	]
})
export class AppModuleShared {
}


