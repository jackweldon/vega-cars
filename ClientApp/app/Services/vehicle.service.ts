import { Vehicle, SaveVehicle } from './../models/vehicle';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs';

@Injectable()
export class VehicleService {

  private readonly vehicliesEndpoint = 'http://localhost:5000/api/vehicles';
  constructor(private http: Http) { }

  update(vehicle: SaveVehicle) {
    return this.http.put(this.vehicliesEndpoint + '/' + vehicle.id, vehicle).map(res => res.json());
  }
  delete(id) {
    return this.http.delete(this.vehicliesEndpoint + '/' + id).map(res => res.json());
  }

  getVehicle(id) {
    return this.http.get(this.vehicliesEndpoint + '/' + id).map(res => res.json());
  }

  toQueryString(obj) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined)
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }
    return parts.join('&');
  }

  getVehicles(filter) {
    console.log(this.vehicliesEndpoint);
    console.log(filter);
    console.log(this.toQueryString(filter));
    return this.http.get(this.vehicliesEndpoint + '?' + this.toQueryString(filter)).map(res => res.json());
  }

  getMakes() {
    return this.http.get('http://localhost:5000/api/makes').map(res => res.json());
  }
  getFeatures() {
    return this.http.get('http://localhost:5000/api/features').map(res => res.json());
  }

  create(vehicle) {
    return this.http.post(this.vehicliesEndpoint, vehicle).map(res => res.json());
  }
}
