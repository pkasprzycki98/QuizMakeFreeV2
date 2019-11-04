import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Route } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { resetFakeAsyncZone } from '@angular/core/testing';

@Component({

	selector: "quiz-edit",
	templateUrl: './quiz-edit.component.html',
	styleUrls: ['./quiz-edit.component.css']
})

export class QuizEditComponent {
	title: string;
	quiz: Quiz;

	editMode: boolean;

	constructor(private activatedRoute: ActivatedRoute,
		private router: Router,
		private http: HttpClient,
		@Inject('BASE_URL') private baseUrl: string) {
		this.quiz = <Quiz>{};

		var id = +this.activatedRoute.snapshot.params["id"];

		if (id) {
			this.editMode = true;

			var url = this.baseUrl + "api/quiz/" + id;
			this.http.get<Quiz>(url).subscribe(result => {
				this.quiz = result;
				this.title = "Edycja" + this.quiz.Title;
			}, error => console.error(error));

		}
		else {
			this.editMode = false;
			this.title = "Utworz nowy quiz";
		}

	}
		onSubmit(quiz: Quiz) {
			var url = this.baseUrl + "api/quiz/";

			if (this.editMode) {
				this.http
					.put<Quiz>(url, this.quiz)
					.subscribe(result => {
						var v = result;
						console.log("Quiz" + v.Id + "został uaktualniony.");
						this.router.navigate(["home"]);
					}, error => console.log(error));
			}
			else {
				this.http
					.post<Quiz>(url, this.quiz)
					.subscribe(result => {
						var v = result;
						console.log("Quiz" + v.Id + "Został utworzony.");
						this.router.navigate(["home"])
					}, error => console.log(error));
			}
		}
		onBack()
		{
			this.router.navigate(["home"]);
		}
	}



