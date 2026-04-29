import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'phoneFormatter'
})
export class PhoneFormatterPipe implements PipeTransform {
  transform(value: string): string {
    return value.replace(/(\d{3})(\d{3})(\d{4})/, '$1-$2-$3');
  }
}