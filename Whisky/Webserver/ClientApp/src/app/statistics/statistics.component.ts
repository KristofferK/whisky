import { Component, OnInit, OnDestroy } from '@angular/core';
import { Measurement } from '../../models/measurement';
import { MeasurementService } from '../measurement.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-statistics-component',
  templateUrl: './statistics.component.html'
})
export class StatisticsComponent implements OnInit, OnDestroy {
  public measurements: Measurement[] = [];
  private measurementSubscription: Subscription;

  constructor(private measurementService: MeasurementService) {
  }

  ngOnInit(): void {
    this.measurementSubscription = this.measurementService.measurements.subscribe(measurement => {
      this.measurements = this.measurements.slice(0, 9);
      this.measurements.unshift(measurement);
      console.log('Added', measurement);
    })
  }

  ngOnDestroy(): void {
    this.measurementSubscription.unsubscribe();
  }
}
