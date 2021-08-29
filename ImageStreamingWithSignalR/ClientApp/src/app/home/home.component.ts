import { Component } from '@angular/core';
import {SignalRService} from '../core/services/signal-r.service';
import {ImageStreamingService} from '../core/services/image-streaming.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],

})
export class HomeComponent {
  startedStreaming = false;

  constructor(
    public signalRService: SignalRService,
    private imageStreamingService: ImageStreamingService
  ) {}


  startStreaming() {
    this.imageStreamingService.startImageStreaming();
    this.signalRService.startConnection();
    this.signalRService.addImageStreamListenerListener();
    this.startedStreaming = true;
  }
}
