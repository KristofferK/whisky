import { Component, OnInit } from '@angular/core';
import { Measurement } from '../../models/measurement';
import { MeasurementService } from '../measurement.service';

@Component({
  selector: 'app-statistics-component',
  templateUrl: './statistics.component.html'
})
export class StatisticsComponent implements OnInit {
  public measurements: Measurement[] = [];

  constructor(private measurementService: MeasurementService) {
  }

  ngOnInit(): void {
    setInterval(() => {
      const measurement = new Measurement();
      measurement.id = this.getRandomInt(1, 3).toString();
      measurement.pressure = this.getRandomInt(10, 30) / 10;
      measurement.temperatureCelsius = this.getRandomInt(500, 700) / 10;
      this.measurements.unshift(measurement);
    }, 950)
  }

  private getRandomInt(min: number, max: number): number {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }
}
