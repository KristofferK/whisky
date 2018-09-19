import { Component, OnInit } from '@angular/core';
import { single, multi } from './data';

@Component({
  selector: 'app-graphs',
  templateUrl: './graphs.component.html',
  styleUrls: ['./graphs.component.css']
})
export class GraphsComponent implements OnInit {
  multi: any[];

  view: any[] = [700, 400];

  // options
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = 'Country';
  showYAxisLabel = true;
  yAxisLabel = 'Population';

  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };

  // line, area
  autoScale = true;

  ngOnInit(): void {
    this.multi = [];
    let data: any[] = [];
    for (let i = 1; i < 20; i++) {
      data.push({ "name": "" + i, "value": Math.random() * 0.6 / i });
    }
    this.multi.push({
      "name": "Denmark",
      "series": data
    });


    let data2: any[] = [];
    for (let i = 1; i < 20; i++) {
      data2.push({ "name": "" + i, "value": Math.random() * 0.6 / i });
    }
    this.multi.push({
      "name": "Sweden",
      "series": data2
    });

    this.multi = [...this.multi];

    let i = 20;
    setInterval(() => {
      this.multi[0]["series"].push({ "name": "" + i, "value": Math.random() * 300 / (i*3) });
      this.multi[1]["series"].push({ "name": "" + i, "value": Math.random() * 300 / (i*3) });
      this.multi = [...this.multi];
      console.log(this.multi);
      i++;
    }, 100)
  }

  onSelect(event) {
    console.log(event);
  }

}
