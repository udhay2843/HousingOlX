import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({
  name: 'liveDate',
  pure: false // Set pure to false to update the date every time the view is checked
})
export class LiveDatePipe implements PipeTransform {

  constructor(private datePipe: DatePipe) {}

  transform(value: any, format: string): any {
    setInterval(() => {}, 1000); // Ensure that the pipe is called every second
    const transformedDate = this.datePipe.transform(new Date(), format);
    return transformedDate;
  }

}
