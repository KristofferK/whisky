import { Component, OnInit } from '@angular/core';
import { Measurement } from '../../models/measurement';
import { MeasurementService } from '../measurement.service';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Component({
  selector: 'app-statistics-component',
  templateUrl: './statistics.component.html'
})
export class StatisticsComponent implements OnInit {
  public measurements: Measurement[] = [];
  private hubConnection: HubConnection;

  constructor(private measurementService: MeasurementService) {
  }

  ngOnInit(): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:44330/whiskyHub')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this.hubConnection.on('MeasurementAdded', (measurement: Measurement) => {
      console.log('Adding', measurement);
      this.measurements.unshift(measurement);
    });
  }
}
