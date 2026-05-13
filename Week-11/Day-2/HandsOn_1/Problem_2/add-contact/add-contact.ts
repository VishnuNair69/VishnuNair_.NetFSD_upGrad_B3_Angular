import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ContactService } from '../contact.service';

@Component({
  selector: 'app-add-contact',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-contact.html'
})
export class AddContactComponent {
  contact = {
    id: 0,
    name: '',
    email: '',
    phone: ''
  };

  constructor(
    private contactService: ContactService,
    private router: Router
  ) {}

  addContact() {
    this.contactService.addContact(this.contact);

    this.contact = {
      id: 0,
      name: '',
      email: '',
      phone: ''
    };

    this.router.navigate(['/contacts']);
  }
}