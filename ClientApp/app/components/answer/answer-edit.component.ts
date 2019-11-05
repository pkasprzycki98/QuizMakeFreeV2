import { Component, Inject, OnInit } from "@angular/core";
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
	selector: "answer-edit",
	templateUrl: './answer-edit.component.html',
	styleUrls: ['./answer-edit.component.css']
})

export class AnswerEditComponent {
	title: string;
	answer: Answer;
	form: FormGroup;

	
	editMode: boolean;

	constructor(private activatedRoute: ActivatedRoute,
		private router: Router,
		private http: HttpClient,
		private fb: FormBuilder,
		@Inject('BASE_URL') private baseUrl: string) {

		
		this.answer = <Answer>{};

		
		this.createForm();

		var id = +this.activatedRoute.snapshot.params["id"];

		
		this.editMode = (this.activatedRoute.snapshot.url[1].path == "edit");

		if (this.editMode) {

			var url = this.baseUrl + "api/answer/" + id;
			this.http.get<Answer>(url).subscribe(result => {
				this.answer = result;
				this.title = "Edycja - " + this.answer.Text;

				
				this.updateForm();
			}, error => console.error(error));
		}
		else {
			this.answer.QuestionId = id;
			this.title = "Utwórz nową odpowiedź";
		}
	}

	createForm() {
		this.form = this.fb.group({
			Text: ['', Validators.required],
			Value: ['',
				[Validators.required,
				Validators.min(-5),
				Validators.max(5)]
			]
		});
	}

	updateForm() {
		this.form.setValue({
			Text: this.answer.Text || '',
			Value: this.answer.Value || 0
		});
	}

	onSubmit() {
		
		var tempAnswer = <Answer>{};
		tempAnswer.Text = this.form.value.Text;
		tempAnswer.Value = this.form.value.Value;
		tempAnswer.QuestionId = this.answer.QuestionId;

		var url = this.baseUrl + "api/answer";

		if (this.editMode) {
			
			tempAnswer.Id = this.answer.Id;

			this.http
				.put<Answer>(url, tempAnswer)
				.subscribe(res => {
					var v = res;
					console.log("Odpowiedź " + v.Id + " została uaktualniona.");
					this.router.navigate(["question/edit", v.QuestionId]);
				}, error => console.log(error));
		}
		else {
			this.http
				.post<Answer>(url, tempAnswer)
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

	// Pobierz FormControl
	getFormControl(name: string) {
		return this.form.get(name);
	}

	// Zwróć TRUE, jeśli element FormControl jest poprawny
	isValid(name: string) {
		var e = this.getFormControl(name);
		return e && e.valid;
	}

	// Zwróć TRUE, jeśli element FormControl uległ zmianie
	isChanged(name: string) {
		var e = this.getFormControl(name);
		return e && (e.dirty || e.touched);
	}

	// Zwróć TRUE, jeśli element FormControl nie jest poprawny po wprowadzeniu zmian
	hasError(name: string) {
		var e = this.getFormControl(name);
		return e && (e.dirty || e.touched) && !e.valid;
	}
}