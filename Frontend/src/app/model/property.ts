import { IPropertyBase } from './ipropertybase';
import{Photo} from'./Photo';

export class Property implements IPropertyBase {
  id: number;
  sellRent: number;
  name: string;
  propertyType: string;
  propertyTypeId: number;
  bhk: number;
  furnishingType: string;
  furnishingTypeId: number;
  price: number;
  builtArea: number;
  carpetArea?: number;
  address: string;
  address2: string;
  city: string;
  CityId:number;
  floorNo?: string;
  totalFloors?: string;
  readyToMove: boolean;
  age: number;
  mainEntrance?: string;
  security:number;
  gated?: boolean;
  maintenance?: number;
  estPossessionOn?: Date;
  Image?: string;
  description?: string;
  photos?: Photo[];

}
