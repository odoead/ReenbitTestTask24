import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MessengerComponent } from './messenger/messenger.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,MessengerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Frontend';
}
