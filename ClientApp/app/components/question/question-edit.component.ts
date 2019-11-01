import { Component, Inject, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({

	selector: "question-edit",
	templateUrl: './question-edit.component.html',
	styleUrls: ['./question-edit.component.less']

})

export class QuestionEditComponent {

	title: string;
	question: Question;
	editMode: boolean;

	constructor(private activatedRoute: ActivatedRoute,
		private router: Router,
		private http: HttpClient,
		@Inject('BASE_URL') private baseUrl: string) {


		this.question = <Question>{};

		var id = +this.activatedRoute.snapshot.params["id"];

		this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

		if (this.editMode) {
			var url = this.baseUrl + "api/question/" + id;
			this.http.get<Question>(url).subscribe(result => {
				this.question = result;
				this.title = "Edycja - " + this.question.Text;
			}, error => console.log(error));
		}
		else {
			this.question.QuizId = id;
			this.title = "Utwórż nowe pytanie";
		}
	}

	onSubmit(question: Question) {
		var url = this.baseUrl + "api/question";

		if (this.editMode) {
			this.http
				.put<Question>(url, question)
				.subscribe(res => {
					var v = res;
					console.log("Pytanie " + v.Id + "zostało zaktualizowane.");
					this.router.navigate(["quiz/edit", v.QuizId]);
				}, error => console.log(error));
		}
		else {
			this.http
				.post<Question>(url, question)
				.subscribe(res => {
					var v = res;
					console.log("Pytanie " + v.Id + "zostało utworzone.");
					this.router.navigate(["quiz/edit", v.QuizId]);
				}, error => console.log(error));
		}
	}

	onBack() {
		this.router.navigate(["quiz/edit", this.question.QuizId]);
	}
}