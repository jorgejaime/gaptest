import { Component } from '@angular/core';

@Component({
    selector: 'appointment',
    templateUrl: './appointment.component.html'
})
export class CounterComponent {
    public currentCount = 0;

    public incrementCounter() {
        this.currentCount++;
    }
}
