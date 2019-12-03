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
	title: string;

	constructor(private http: HttpClient,
		@Inject('BASE_URL') private baseUrl: string,
		private router: Router) {

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
}
