import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ContactService } from '../../services/contact';
import { Contact } from '../../models/contact';

@Component({
  selector: 'app-contact-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './contact-form.html'
})
export class ContactFormComponent {

  contact: Contact = {
    name: '',
    email: '',
    phone: '',
    categoryId: 1
  };

  constructor(
    private service: ContactService,
    private router: Router
  ) {}

  save() {
    this.service.addContact(this.contact).subscribe(() => {
      alert('Contact Added Successfully');
      this.router.navigate(['/']);
    });
  }
}