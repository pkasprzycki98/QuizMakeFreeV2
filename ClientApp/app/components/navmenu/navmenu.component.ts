import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from "@angular/router";

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
	username: string | null;
	constructor(public auth: AuthService, private router: Router) {
	}

	logout(): boolean {

		if (this.auth.logout()) {
			this.router.navigate([""])
			
		}
		return false;
	}

}
