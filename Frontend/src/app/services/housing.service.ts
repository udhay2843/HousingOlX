import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { IProperty } from '../model/iproperty';
import { IPropertyBase } from '../model/ipropertybase';
import { Property } from '../model/property';
import { Ikeyvaluepair } from '../model/ikeyvaluepair';
import { error } from '@angular/compiler/src/util';

@Injectable({
  providedIn: 'root'
})
export class HousingService {

  constructor(private http: HttpClient) { }
GetAllCities():Observable<string[]>{
  return this.http.get<string[]>('http://localhost:5283/api/city');
}
  getProperty(id: number) {
    return this.http.get<Property>('http://localhost:5283/api/property/detail/'+id);
    // return this.getAllProperties(2).pipe(  for use of local json file
    //   map(propertiesArray => {
    //     // throw new Error('Some error');
    //     return propertiesArray.find(p => p.id === id);
    //   })
    // );
  }
  getPropertyTypes():Observable<Ikeyvaluepair[]> {
    return this.http.get<Ikeyvaluepair[]>('http://localhost:5283/api/propertyType/list');
  }
  getfurnishingTypes():Observable<Ikeyvaluepair[]> {
    return this.http.get<Ikeyvaluepair[]>('http://localhost:5283/api/furnishingType/list');
  }

  getAllProperties(SellRent?: number): Observable<Property[]> {
    return this.http.get<Property[]>('http://localhost:5283/api/property/list/'+SellRent.toString());
    // return this.http.get('data/properties.json').pipe(
    //   map(data => {
    //   const propertiesArray: Array<Property> = [];
    //   const localProperties = JSON.parse(localStorage.getItem('newProp'));

    //   if (localProperties) {
    //     for (const id in localProperties) {
    //       if (SellRent) {
    //       if (localProperties.hasOwnProperty(id) && localProperties[id].SellRent === SellRent) {
    //         propertiesArray.push(localProperties[id]);
    //       }
    //     } else {
    //       propertiesArray.push(localProperties[id]);
    //     }
    //     }
    //   }

    //   for (const id in data) {
    //     if (SellRent) {
    //       if (data.hasOwnProperty(id) && data[id].SellRent === SellRent) {
    //         propertiesArray.push(data[id]);
    //       }
    //       } else {
    //         propertiesArray.push(data[id]);
    //     }
    //   }
    //   return propertiesArray;
    //   })
    // );

    // return this.http.get<Property[]>('data/properties.json');
  }
  addProperty(property: Property) {
    const httpOptions={
      headers: new HttpHeaders({
        Authorization: 'Bearer '+localStorage.getItem('token')
      })
    };
    return this.http.post("http://localhost:5283/api/property/add",property,httpOptions);
  }

  newPropID() {
    if (localStorage.getItem('PID')) {
      localStorage.setItem('PID', String(+localStorage.getItem('PID') + 1));
      return +localStorage.getItem('PID');
    } else {
      localStorage.setItem('PID', '101');
      return 101;
    }
  }
  getPropertyAge(dateofEstablishment: Date): string
    {
        const today = new Date();
        const estDate = new Date(dateofEstablishment);
        let age = today.getFullYear() - estDate.getFullYear();
        const m = today.getMonth() - estDate.getMonth();

        // Current month smaller than establishment month or
        // Same month but current date smaller than establishment date
        if (m < 0 || (m === 0 && today.getDate() < estDate.getDate())) {
            age --;
        }

        // Establshment date is future date
        if(today < estDate) {
            return '0';
        }

        // Age is less than a year
        if(age === 0) {
            return 'Less than a year';
        }

        return age.toString();
    }
    setPrimaryPhoto(propertyid:number,propertyPhotoid:string){
      const httpOptions={
        headers: new HttpHeaders({
          Authorization: 'Bearer '+localStorage.getItem('token')
        })
      };
return this.http.post("http://localhost:5283/api/property/set-primary-photo/"
+propertyid+"/"+propertyPhotoid,{},httpOptions);
    }
    DeletePhoto(propertyid:number,propertyPhotoid:string){
      const httpOptions={
        headers: new HttpHeaders({
          Authorization: 'Bearer '+localStorage.getItem('token')
        })
      };
return this.http.delete("http://localhost:5283/api/property/delete-photo/"
+propertyid+"/"+propertyPhotoid,httpOptions);
    }


    DeleteProperty(propertyid:number){
      const httpOptions={
        headers: new HttpHeaders({
          Authorization: 'Bearer '+localStorage.getItem('token')
        })
      };
return this.http.delete("http://localhost:5283/api/property/delete/"
+propertyid,httpOptions);
    }
}
