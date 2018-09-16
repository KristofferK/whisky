import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Measurement } from '../models/measurement';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Observable, ReplaySubject } from 'rxjs';
import { environment } from './../environments/environment';

@Injectable()
export class MeasurementService {
  private hubConnection: HubConnection;
  public measurements: ReplaySubject<Measurement> = new ReplaySubject<Measurement>();

  constructor(private http: HttpClient) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(environment.serverUrl + 'whiskyHub')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this.hubConnection.on('MeasurementAdded', (measurement: Measurement) => {
      this.measurements.next(measurement);
    });

    this.getTasksFromApi();
  }

  private getTasksFromApi(): void {
    this.http.get<Measurement[]>(environment.serverUrl + 'api/Measurement/GetExistingMeasurements').subscribe((measurements: Measurement[]) => {
      measurements.forEach(task => this.measurements.next(task))
    });
  }
}
