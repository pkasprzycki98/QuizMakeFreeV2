import { Component, Inject, Input, OnChanges, SimpleChanges,OnInit, ElementRef, ViewChild} from "@angular/core";
import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { elementStart } from "@angular/core/src/render3/instructions";
import { forEach } from "@angular/router/src/utils/collection";
import { isNumber } from "util";

@Component({
	selector: "answer-take",
	templateUrl: './answer-take.component.html',
	styleUrls: ['./answer-take.component.css']
})

export class AnswerTakeComponent implements OnInit {
	@Input() question: Question;
	answers: Answer[];
	title: string;
	Value: number;
	SumValue: number;

	constructor(private http: HttpClient,
		@Inject('BASE_URL') private baseUrl: string,
		private router: Router) {

		this.answers = [];
	}

	ngOnInit() {

		// Wykonaj żądanie HTTP i pobierz wynik
		this.loadData();

	}

	loadData() {
		var url = this.baseUrl + "api/answer/All/" + this.question.Id;
		console.log("value form antakecom: " + this.question.Id);
		this.http.get<Answer[]>(url).subscribe(result => {
			this.answers = result;
		}, error => console.error(error));
	}

	isChange(id: number) {

		var answer = document.getElementById("Answer:" + id);

		var ischecked = answer.getAttribute("data-ischecked");

		if (ischecked == "false") {
			console.log("IF");
			answer.setAttribute("data-ischecked", "true");

		}
		else {
			console.log("ELSE");
			answer.setAttribute("data-ischecked", "false");
		}

		console.log(answer);
	
		var questionId = answer.getAttribute("data-questionId");

		let a = document.getElementsByClassName(questionId);


		
	
		Array.prototype.forEach.call(a, function(el: any){

			console.log("Wartość z pętli:" + el.getAttribute("data-ischecked"));

			if (el.getAttribute("data-ischecked") == "true") {
				if (el == answer) {

				}
				else {
					el.setAttribute("data-ischecked", "false");
					el.checked = false;
				}
				
			}
		})

		if (ischecked == "false") {
			console.log("IF");
			answer.setAttribute("data-ischecked", "true");
			ischecked = answer.getAttribute("data-ischecked");
			console.log("WartośćIsChecked:" + ischecked)


		}
		else {
			console.log("ELSE");
			answer.setAttribute("data-ischecked", "false");
			ischecked = answer.getAttribute("data-ischecked");
			console.log("WartośćIsChecked:" + ischecked)

		}
		//for (var index in a) {

		//	console.log(index);
		//	if (+index )
		//	{
		//		console.log("index:" + index);
		//		var ok = JSON.parse(document.getElementsByClassName(questionId)[index].getAttribute("data-ischecked"));
		//		console.log(ok);
		//	}
		//	if (ok == "true") {
		//		a[index].removeAttribute("Checked");
		//	}

		//}

		
	}
	
}


