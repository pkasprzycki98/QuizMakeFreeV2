import { Component, Inject, Input, OnChanges, SimpleChanges } from "@angular/core";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({

	selector: "question-list",
	templateUrl: './question-list.component.html',
	styleUrls: ['./question-list.component.css']
})

export class QuestionListComponent implements OnChanges {
	@Input() quiz: Quiz;
	questions: Question[];
	title: string;

	constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) {
		this.questions = [];
	}

	ngOnChanges(changes: SimpleChanges) {
		if (typeof changes['quiz'] !== "undefined")
		{
			var change = changes['quiz'];

			if (!change.isFirstChange()) {
				this.loadData();
			}
		}
	}

	loadData() {
		var url = this.baseUrl + "api/question/All/" + this.quiz.Id;
		this.http.get<Question[]>(url).subscribe(result => {
			this.questions = result;
		}, error => console.error(error));
	}

	onCreate() {
		this.router.navigate(["/question/create", this.quiz.Id]);
	}

	onEdit(question: Question) {
		this.router.navigate(["/question/edit", question.Id]);
	}
	onDelete(question: Question) {
		if (confirm("Czy naprawdę chcesz usnąć to pytanie?")) {
			var url = this.baseUrl + "api/question/" + question.Id;

			this.http
				.delete(url)
				.subscribe(res => {
					console.log("Pytanie" + question.Id + "zostało usunięte.");

					this.loadData();
				}, error => console.error(error))
		}
	}
}
