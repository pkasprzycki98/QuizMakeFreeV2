import { Component, Inject, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({

	selector: "result-edit",
	templateUrl: './result-edit.component.html',
	styleUrls: ['./result-edit.component.css']

})

export class ResultEditComponent {

	title: string;
	result: Result;
	editMode: boolean;
	form: FormGroup;

	constructor(private activatedRoute: ActivatedRoute,
		private router: Router,
		private http: HttpClient,private fb: FormBuilder,
		@Inject('BASE_URL') private baseUrl: string) {


		this.result = <Result>{};

		this.createForm();
		var id = +this.activatedRoute.snapshot.params["id"];

		this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

		if (this.editMode) {
			var url = this.baseUrl + "api/result/" + id;
			this.http.get<Result>(url).subscribe(result => {
				this.result = result;
				this.title = "Edycja - " + this.result.Text;
				this.updateForm();
			}, error => console.log(error));
		}
		else {
			this.result.QuizId = id;
			this.title = "Utwórż nowy wynik";
		}
	}

	createForm() {
		this.form = this.fb.group({
			Text: ['', Validators.required],
			MinValue: ['', Validators.pattern("^-?[0-9]*$")],
			MaxValue: ['', Validators.pattern("^-?[0-9]*$")]
		});
	}

	updateForm() {
		this.form.setValue({
			Text: this.result.Text,
			MinValue: this.result.MinValue,
			MaxValue: this.result.MaxValue
		});
	}

	onSubmit() {
		var url = this.baseUrl + "api/result";

		var tempResult = <Result>{};
		tempResult.QuizId = this.result.QuizId;
		tempResult.MinValue = this.form.value.MinValue;
		tempResult.Text = this.form.value.Text;
		tempResult.MaxValue = this.form.value.MaxValue;

		if (this.editMode) {
			tempResult.Id = this.result.Id;

			this.http
				.put<Result>(url, tempResult)
				.subscribe(res => {
					var v = res;
					console.log("Wynik " + v.Id + "został zaktualizowany.");
					this.router.navigate(["quiz/edit", v.QuizId]);
				}, error => console.log(error));
		}
		else {
			this.http
				.post<Result>(url, tempResult)
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

	
	//pobiera fromControl
	getFormControl(name: string) {
		return this.form.get(name);
	}
	//True jak jest poprawny
	isValid(name: string) {
		var e = this.getFormControl(name);
		return e && e.valid;
	}
	//True jak uległ zmianie
	isChanged(name: string) {
		var e = this.getFormControl(name);
		return e && (e.dirty || e.touched);
	}
	//True jak element fromControl nie jest poprawny
	hasError(name: string) {
		var e = this.getFormControl(name);
		return e && (e.dirty || e.touched) && !e.valid;
	}
}