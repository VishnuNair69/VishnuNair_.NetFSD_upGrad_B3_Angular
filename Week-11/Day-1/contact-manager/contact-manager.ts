import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { PhoneFormatterPipe } from '../pipes/phone-formatter-pipe';
import { StatusPipe } from '../pipes/status-pipe';
import { SearchFilterPipe } from '../pipes/search-filter-pipe';

@Component({
  selector: 'app-contact-manager',
  imports: [
    FormsModule,
    CommonModule,
    PhoneFormatterPipe,
    StatusPipe,
    SearchFilterPipe
  ],
  templateUrl: './contact-manager.html',
  styleUrl: './contact-manager.css'
})
export class ContactManagerComponent {
  searchText: string = '';
  showCount: number = 5;

  contacts = [
    { name: 'Geetha Lakshmi', email: 'geetha@gmail.com', phone: '9876543210', status: true },
    { name: 'Chandana', email: 'chandhu@gmail.com', phone: '9123456789', status: false },
    { name: 'Pooji', email: 'pooji@gmail.com', phone: '9988776655', status: true },
    { name: 'Ayesha', email: 'ayesha@gmail.com', phone: '9871234567', status: true },
    { name: 'Bhavana', email: 'bhavana@gmail.com', phone: '9998887776', status: false },
    { name: 'Priyanka', email: 'priya@gmail.com', phone: '9876549876', status: true },
    { name: 'Vyshu', email: 'vyshu@gmail.com', phone: '9012345678', status: false },
    { name: 'Likki', email: 'likki@gmail.com', phone: '9870001112', status: true },
    { name: 'Mahe', email: 'mahe@gmail.com', phone: '9988112233', status: true },
    { name: 'Bhargav', email: 'bhargav@gmail.com', phone: '9090909090', status: false }
  ];

  toggleStatus(contact: any) {
    contact.status = !contact.status;
  }

  showMore() {
    this.showCount = this.contacts.length;
  }

  showLess() {
    this.showCount = 5;
  }
}