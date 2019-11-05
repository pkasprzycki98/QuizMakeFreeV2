import { Component, Inject, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { FormGroup, FormControl, FormBuilder, Validators } from "@angular/forms";
@Component({

	selector: "question-edit",
	templateUrl: './question-edit.component.html',
	styleUrls: ['./question-edit.component.css']

})

export class QuestionEditComponent {

	title: string;
	question: Question;
	form: FormGroup;
	editMode: boolean;

	constructor(private activatedRoute: ActivatedRoute,
		private router: Router,private fb: FormBuilder,
		private http: HttpClient,
		@Inject('BASE_URL') private baseUrl: string) {


		this.question = <Question>{};

		this.createForm() // inicjacja formularza

		var id = +this.activatedRoute.snapshot.params["id"];

		this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

		if (this.editMode) {
			var url = this.baseUrl + "api/question/" + id;
			this.http.get<Question>(url).subscribe(result => {
				this.question = result;
				this.title = "Edycja - " + this.question.Text;
			this.updateForm();
			}, error => console.log(error));
		}
		else {
			this.question.QuizId = id;
			this.title = "Utwórż nowe pytanie";
		}
	}

	onSubmit(question: Question) {

		var tempQuestion = <Question>{};
		tempQuestion.QuizId = this.form.value.QuizId;
		tempQuestion.Text = this.form.value.Text;
		var url = this.baseUrl + "api/question";

		if (this.editMode) {
			this.http
				.put<Question>(url, tempQuestion)
				.subscribe(res => {
					var v = res;
					console.log("Pytanie " + v.Id + "zostało zaktualizowane.");
					this.router.navigate(["quiz/edit", v.QuizId]);
				}, error => console.log(error));
		}
		else {
			this.http
				.post<Question>(url, tempQuestion)
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

	createForm() {

		this.form = this.fb.group({
			Text: ['', Validators.required]
		});
	}

	updateForm() {
		this.form.setValue({
			Text: this.question.Text
		});
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