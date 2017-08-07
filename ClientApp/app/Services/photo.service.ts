import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

@Injectable()
export class PhotoService {
    private readonly vehicliesEndpoint = 'http://localhost:5000/api/vehicles';
    
    getVehicle(id) {
        return this.http.get(this.vehicliesEndpoint + '/' + id).map(res => res.json());
    }
    constructor(private http: Http) { }

    upload(vehicleId, photo) {
        var formData = new FormData();
        formData.append('file', photo);
        return this.http.post(`${this.vehicliesEndpoint}/${vehicleId}/photos`, photo).map(res => res.json());
    }

    getPhotos(vehicleId){
        return this.http.get(`${this.vehicliesEndpoint}/${vehicleId}/photos`).map(res => res.json());
    }
}