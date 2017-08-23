import { VehicleService } from './../../Services/vehicle.service';
import { Vehicle, KeyValuePair } from './../../models/vehicle';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.scss'],  
  providers: [VehicleService]
})
export class VehicleListComponent implements OnInit {

  private readonly PAGE_SIZE = 9;
  makes: KeyValuePair[];
  query: any = {
    pageSize: this.PAGE_SIZE,
    page: 1
  };

  queryResult: any = {};

  columns = [
    { title: 'Id', },
    { title: 'Contact Name', key: 'contactNAme', isSortable: true },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { }
  ];
  constructor(private vehicleService: VehicleService) { }

  onFilterChange() {
    this.query.page = 1;
    this.populateVehicles();
  }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe(makes => this.makes = makes);
    this.populateVehicles();
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.query).subscribe(result => this.queryResult = result);
  }

  onPageChange(page) {
    this.query.page = page;
    this.populateVehicles();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;

    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateVehicles();
  }

  resetFilter() {
    this.query = { page: 1, pageSize: this.PAGE_SIZE };
    this.populateVehicles();
  }

}
