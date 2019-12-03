import { Component, Inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { AuthService } from "../../services/auth.service";
import { readdirSync } from "fs";

@Component({
	selector: "quiz",
	templateUrl: './quiz-take.component.html',
	styleUrls: ['./quiz-take.component.css']
})

export class QuizTakeComponent {
	quiz: Quiz;
	title: string;

	constructor(private activatedRoute: ActivatedRoute,
		private router: Router,
		private http: HttpClient,
		public auth: AuthService,
		@Inject('BASE_URL') private baseUrl: string) {

		var id = +this.activatedRoute.snapshot.params["id"];
		console.log(id);
		if (id) {
			var url = this.baseUrl + "api/quiz/" + id;
			this.http.get<Quiz>(url).subscribe(result => {
				this.quiz = result;
				this.title = result.Title;
			}, error => console.error(error));
		}
		else {
			console.log("Nieprawidłowe id: powracam do adresu home...");
			this.router.navigate(["home"]);
		}
	}

	sumPoint() {

		var check = document.getElementsByClassName("checkbox");

		console.log(check);

	}
}