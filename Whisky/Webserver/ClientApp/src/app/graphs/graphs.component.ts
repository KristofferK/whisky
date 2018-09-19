import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { MeasurementService } from '../measurement.service';

@Component({
  selector: 'app-graphs',
  templateUrl: './graphs.component.html',
  styleUrls: ['./graphs.component.css']
})
export class GraphsComponent implements OnInit, OnDestroy {
  private measurementSubscription: Subscription;
  multi: any[] = [];

  // options
  view: any[] = [700, 400];
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = 'Time';
  showYAxisLabel = true;
  yAxisLabel = 'Temperature';
  autoScale = true;
  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };

  constructor(private measurementService: MeasurementService) {
  }

  ngOnInit(): void {
    this.measurementSubscription = this.measurementService.measurements.subscribe(measurement => {
      const element = { name: new Date(measurement.dateMeasured), value: measurement.temperature };
      const sensorIndex = this.multi.findIndex(e => e["name"] == measurement.sensorID);
      if (sensorIndex !== -1) {
        this.multi[sensorIndex]["series"].push(element);
      }
      else {
        this.multi.push({ name: measurement.sensorID, series: [element] });
      }
      this.multi = [...this.multi];
    });
  }

  ngOnDestroy(): void {
    this.measurementSubscription.unsubscribe();
  }
}
