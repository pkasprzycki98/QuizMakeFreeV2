import { Component, Inject, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
	selector: "answer-edit",
	templateUrl: './answer-edit.component.html',
	styleUrls: ['./answer-edit.component.less']
})

export class AnswerEditComponent {
	title: string;
	answer: Answer;

	// Otrzyma wartość TRUE w przypadku edycji istniejącej odpowiedzi 
	// lub FALSE w przypadku nowej odpowiedzi
	editMode: boolean;

	constructor(private activatedRoute: ActivatedRoute,
		private router: Router,
		private http: HttpClient,
		@Inject('BASE_URL') private baseUrl: string) {

		// Utwórz pusty obiekt na podstawie interfejsu Answer
		this.answer = <Answer>{};

		var id = +this.activatedRoute.snapshot.params["id"];

		// Szybki sposób sprawdzenia, czy znajdujemy się w trybie edycji
		this.editMode = (this.activatedRoute.snapshot.url[1].path == "edit");

		if (this.editMode) {

			// Pobierz odpowiedź z serwera
			var url = this.baseUrl + "api/answer/" + id;
			this.http.get<Answer>(url).subscribe(result => {
				this.answer = result;
				this.title = "Edycja - " + this.answer.Text;
			}, error => console.error(error));
		}
		else {
			this.answer.QuestionId = id;
			this.title = "Utwórz nową odpowiedź";
		}
	}

	onSubmit(answer: Answer) {
		var url = this.baseUrl + "api/answer";

		if (this.editMode) {
			this.http
				.put<Answer>(url, answer)
				.subscribe(res => {
					var v = res;
					console.log("Odpowiedź " + v.Id + " została uaktualniona.");
					this.router.navigate(["question/edit", v.QuestionId]);
				}, error => console.log(error));
		}
		else {
			this.http
				.post<Answer>(url, answer)
				.subscribe(res => {
					var v = res;
					console.log("Odpowiedź " + v.Id + " została utworzona.");
					this.router.navigate(["question/edit", v.QuestionId]);
				}, error => console.log(error));
		}
	}

	onBack() {
		this.router.navigate(["question/edit", this.answer.QuestionId]);
	}
}

