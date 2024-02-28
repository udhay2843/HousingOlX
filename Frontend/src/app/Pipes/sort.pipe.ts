import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'sort'
})
// export class SortPipe implements PipeTransform {

//   transform(value: Array<string>, args: any[]): any {
//     const sortField = args[0];
//     const sortDirection = args[1];
//     let multiplier = 1;

//     if (sortDirection === 'desc') {
//       multiplier = -1;
//     }

//     value.sort((a: any, b: any):any => {
//       if (a[sortField] < b[sortField]) {
//         return -1 * multiplier;
//       } else if (a[sortField] > b[sortField]) {
//         return 1 * multiplier;
//       } else {
//         return 0;
//       }
//     }
//     );

//     return value;
//   }

// }

export class SortPipe implements PipeTransform {

  transform(value: any[], args: any[]): any[] {
    // Check if value is undefined or null
    if (!value) {
      return [];
    }

    const sortField = args[0];
    const sortDirection = args[1];
    let multiplier = 1;

    // Ensure sortDirection is valid
    if (sortDirection === 'desc') {
      multiplier = -1;
    }

    // Ensure sortField is valid
    if (!sortField) {
      return value;
    }

    // Sort the array based on sortField and sortDirection
    value.sort((a: any, b: any): number => {
      if (a[sortField] < b[sortField]) {
        return -1 * multiplier;
      } else if (a[sortField] > b[sortField]) {
        return 1 * multiplier;
      } else {
        return 0;
      }
    });

    return value;
  }

}
