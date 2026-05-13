import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ContactService } from '../../services/contact';
import { Contact } from '../../models/contact';

@Component({
  selector: 'app-contact-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './contact-list.html'
})
export class ContactListComponent implements OnInit {

  contacts: Contact[] = [];

  constructor(private service: ContactService) {}

  ngOnInit(): void {
    this.loadContacts();
  }

  loadContacts() {
    this.service.getContacts().subscribe(data => {
      this.contacts = data;
    });
  }

  deleteContact(id: number) {
    this.service.deleteContact(id).subscribe(() => {
      alert('Deleted Successfully');
      this.loadContacts();
    });
  }
}