import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Route } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

@Component({

	selector: "quiz-edit",
	templateUrl: './quiz-edit.component.html',
	styleUrls: ['./quiz-edit.component.css']
})

export class QuizEditComponent {
	title: string;
	quiz: Quiz;
	form: FormGroup;
	editMode: boolean;

	constructor(private activatedRoute: ActivatedRoute,
		private router: Router,
		private http: HttpClient,private fb: FormBuilder,
		@Inject('BASE_URL') private baseUrl: string) {
		this.quiz = <Quiz>{};

		this.createForm(); //zainicjonowanie formularza
		var id = +this.activatedRoute.snapshot.params["id"];

		if (id) {
			this.editMode = true;

			var url = this.baseUrl + "api/quiz/" + id;
			this.http.get<Quiz>(url).subscribe(result => {
				this.quiz = result;
				this.title = "Edycja" + this.quiz.Title;
			this.updateForm();

			}, error => console.error(error));

		}
		else {
			this.editMode = false;
			this.title = "Utworz nowy quiz";
		}



	}
	onSubmit() {

		var tempQuiz = <Quiz>{};
		tempQuiz.Title = this.form.value.Title;
		tempQuiz.Description = this.form.value.Description;
		tempQuiz.Text = this.form.value.Text;

			var url = this.baseUrl + "api/quiz/";

		if (this.editMode) {

			//ustawienie tempQuiz.Id na quiz.Id.
			tempQuiz.Id = this.quiz.Id;
				this.http
					.put<Quiz>(url, tempQuiz)
					.subscribe(result => {
						var v = result;
						console.log("Quiz" + v.Id + "został uaktualniony.");
						this.router.navigate(["home"]);
					}, error => console.log(error));
			}
			else {
				this.http
					.post<Quiz>(url, tempQuiz)
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

	createForm() {
		this.form = this.fb.group({
			Title: ['', Validators.required],
			Description: '',
			Text: ''
		});
	}
	updateForm() {
		this.form.setValue({
			Title: this.quiz.Title,
			Description: this.quiz.Description || '',
			Text: this.quiz.Text || ''
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



