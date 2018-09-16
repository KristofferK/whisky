import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { Measurement } from '../../models/measurement';
import { MeasurementService } from '../measurement.service';
import { Subscription } from 'rxjs';
import { environment } from './../../environments/environment';

@Component({
  selector: 'app-statistics-component',
  templateUrl: './statistics.component.html'
})
export class StatisticsComponent implements OnInit, OnDestroy {
  public measurements: Measurement[] = [];
  private measurementSubscription: Subscription;

  constructor(private measurementService: MeasurementService, private changeDetectorRef: ChangeDetectorRef) {
  }

  ngOnInit(): void {
    this.measurementSubscription = this.measurementService.measurements.subscribe(measurement => {
      this.measurements = this.measurements.slice(0, 49);
      this.measurements.unshift(measurement);
      if (environment.production) {
        this.changeDetectorRef.detectChanges();
      }
    })
  }

  ngOnDestroy(): void {
    this.measurementSubscription.unsubscribe();
  }
}
