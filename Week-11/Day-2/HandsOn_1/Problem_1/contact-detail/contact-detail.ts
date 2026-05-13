import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactService } from '../contact.service';
import { Contact } from '../contact';

@Component({
  selector: 'app-contact-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './contact-detail.html'
})
export class ContactDetailComponent implements OnInit {
  selectedContact?: Contact;

  constructor(private contactService: ContactService) {}

  ngOnInit(): void {
    this.selectedContact = this.contactService.getContactById(1);
  }
}