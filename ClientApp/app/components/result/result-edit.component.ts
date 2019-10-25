import { Component, Inject, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { ReactiveFormsModule } from "@angular/forms";

@Component({

	selector: "result-edit",
	templateUrl: './result-edit.component.html',
	styleUrls: ['./result-edit.component.css']

})

export class ResultEditComponent {

	title: string;
	result: Result;
	editMode: boolean;

	constructor(private activatedRoute: ActivatedRoute,
		private router: Router,
		private http: HttpClient,
		@Inject('BASE_URL') private baseUrl: string) {


		this.result = <Result>{};

		var id = +this.activatedRoute.snapshot.params["id"];

		this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

		if (this.editMode) {
			var url = this.baseUrl + "api/result/" + id;
			this.http.get<Result>(url).subscribe(result => {
				this.result = result;
				this.title = "Edycja - " + this.result.Text;
			}, error => console.log(error));
		}
		else {
			this.result.QuizId = id;
			this.title = "Utwórż nowy wynik";
		}
	}

	onSubmite(result: Result) {
		var url = this.baseUrl + "api/result";

		if (this.editMode) {
			this.http
				.put<Result>(url, result)
				.subscribe(res => {
					var v = res;
					console.log("Wynik " + v.Id + "został zaktualizowany.");
					this.router.navigate(["quiz/edit", v.QuizId]);
				}, error => console.log(error));
		}
		else {
			this.http
				.post<Result>(url, result)
				.subscribe(res => {
					var v = res;
					console.log("wynik " + v.Id + "zostało utworzony.");
					this.router.navigate(["quiz/edit", v.QuizId]);
				}, error => console.log(error));
		}
	}

	onBack() {
		this.router.navigate(["quiz/edit", this.result.QuizId]);
	}
}