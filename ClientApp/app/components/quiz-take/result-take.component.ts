import { Component, Inject, Input, OnChanges, SimpleChanges } from "@angular/core";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
	selector: "result-take",
	templateUrl: './result-take.component.html',
	styleUrls: ['./result-take.component.css']
})

export class ResultTakeComponent implements OnChanges {
	@Input() quiz: Quiz;
	results: Result[];
	result: Result;
	isFinished: boolean;
	title: string;
	point: number;

	constructor(private http: HttpClient,
		@Inject('BASE_URL') private baseUrl: string,
		private router: Router) {
		this.isFinished = false;
		this.results = [];
	}

	ngOnChanges(changes: SimpleChanges) {
		if (typeof changes['quiz'] !== "undefined") {

			// Pobierz z informacji o zawarte pod kluczem quiz
			var change = changes['quiz'];

			// Wykonaj zadanie tylko w przypadku, gdy wartość uległa zmianie
			if (!change.isFirstChange()) {
				// Wykonaj żądanie HTTP i pobierz wynik
				this.loadData();
			}
		}
	}

	loadData() {
		var url = this.baseUrl + "api/result/All/" + this.quiz.Id;
		this.http.get<Result[]>(url).subscribe(result => {
			this.results = result;
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
		
		document.getElementById("sumPoint").setAttribute("point",""+sumPoint);

		this.giveResult();
	}

	giveResult() {
		var e = document.getElementById("sumPoint").getAttribute("point");
		var i: number;
		i = 0;
		var xresult;
		console.log("punkty zebrane przez użytkownika:" + e);
		this.point = +e;
		this.results.forEach(function (value) {
			console.log(value);
		})
		for (var i = 0; i <= this.results.length; i++) {		
			console.log(this.results[i]);
			if (this.results[i].MinValue <= +e && this.results[i].MaxValue >= +e) {
				xresult = this.results[i];
				alert(this.results[i].Text);
				break;
			}
			else {
				i = i + 1;
			}
		}
		this.result = xresult;

		console.log("wartosc i:" + i);
		if (i == this.results.length) {
			alert("nie ma wyniku dla Twoich odpowiedzi :) skontaktuj się z twórcą quiziu!");
		}
		console.log("sumPoint");

		this.isFinished = true;
	}
}
