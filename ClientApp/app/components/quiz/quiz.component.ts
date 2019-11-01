﻿import { Component, Inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
	selector: "quiz",
	templateUrl: './quiz.component.html',
	styleUrls: ['./quiz.component.less']
})

export class QuizComponent {
	quiz: Quiz;

	constructor(private activatedRoute: ActivatedRoute,
		private router: Router,
		private http: HttpClient,
		@Inject('BASE_URL') private baseUrl: string) {

		var id = +this.activatedRoute.snapshot.params["id"];
		console.log(id);
		if (id) {
			var url = this.baseUrl + "api/quiz/" + id;
			this.http.get<Quiz>(url).subscribe(result => {
				this.quiz = result;
			}, error => console.error(error));
		}
		else {
			console.log("Nieprawidłowe id: powracam do adresu home...");
			this.router.navigate(["home"]);
		}
	}

	onEdit() {
		this.router.navigate(["quiz/edit", this.quiz.Id]);
	}
	onDelete() { 
		if (confirm("Czy na pewno chcesz usunąć ten quiz")) {
			var url = this.baseUrl + "api/quiz/" + this.quiz.Id;
			this.http
				.delete(url)
				.subscribe(res => {
					console.log("Quiz" + this.quiz.Id + "został usunięty.");
					this.router.navigate(["home"]);
				}, error => console.log(error));
		}
	}
}
