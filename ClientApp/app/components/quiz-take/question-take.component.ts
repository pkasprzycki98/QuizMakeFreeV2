import { Component, Inject, Input, OnChanges, SimpleChanges } from "@angular/core";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({

	selector: "question-take",
	templateUrl: './question-take.component.html',
	styleUrls: ['./question-take.component.css']
})

export class QuestionTakeComponent implements OnChanges {
	@Input() quiz: Quiz;
	questions: Question[];
	title: string;
	constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) {
		this.questions = [];
		
	}

	ngOnChanges(changes: SimpleChanges) {
		console.log("eluwina");
		if (typeof changes['quiz'] !== "undefined") {
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
			for (let i in this.questions) {
				console.log("QuestionId " + this.questions[i].Id);
			}
			
		}, error => console.error(error));
	}

	sumPoint() {
		
		var answers = document.getElementsByName("checkbox");	
		var actuallPoint = 0;
		var sumPoint = 0;
		console.log("wielkość tablicy" + answers.length)
		for (var i = 0; i < answers.length; i++) {
			var isChecked = answers[i].getAttribute("data-ischecked");
			actuallPoint = +answers[i].getAttribute("data-answervalue");
			console.log("punkty pobrane z atrubutu:" + actuallPoint);
			console.log(isChecked);
			if (isChecked == "true") {
				console.log(true);				
				sumPoint += actuallPoint;
				console.log(sumPoint);

			}

		}

		alert("Twoje punkty wynoszą:" + sumPoint);
	}
}