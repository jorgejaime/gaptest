import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'patient',
    templateUrl: './patient.component.html'
})
export class PatientComponent {
    public response: Response;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/patients').subscribe(result => {
            this.response = result.json() as Response;
        }, error => console.error(error));
    }
}

interface Patients {
    id: number;
    name: string;
    gender: string;
    ege: number;
}

interface Data {
    patients: Patients[]
}

interface Response {
    data: Data[]
}