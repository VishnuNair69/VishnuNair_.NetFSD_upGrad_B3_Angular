import { Routes } from '@angular/router';
import { ContactListComponent } from './contact-list/contact-list';
import { AddContactComponent } from './add-contact/add-contact';
import { ContactDetailComponent } from './contact-detail/contact-detail';

export const routes: Routes = [
  { path: 'contacts', component: ContactListComponent },
  { path: 'add-contact', component: AddContactComponent },
  { path: 'contact/:id', component: ContactDetailComponent },
  { path: '', redirectTo: '/contacts', pathMatch: 'full' }
];