import { createRouter, createWebHistory } from 'vue-router'
import Contacts from '@/components/Contacts/Contacts.vue'
import ContactsForm from '@/components/Contacts/ContactsForm.vue'
import { fetchContactById } from '@/services/contactService.js';


const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            name: 'Contacts',
            component: Contacts
        },
        {
            path: '/contacts/new',
            name: 'NewContact',
            component: ContactsForm
        },
        {
            path: '/contacts/:id/edit',
            name: 'EditContact',
            component: ContactsForm,
            props: route => ({
                isEditing: true,
                contact: route.params.contact
            }),
            beforeEnter: async (to, from, next) => {
                try {
                    // Fetch contact by ID if not passed in route params
                    if (!to.params.contact) {
                        const contact = await fetchContactById(to.params.id);
                        to.params.contact = contact; // Attach fetched data to params
                    }
                    next(); // Navigate to the route
                } catch (error) {
                    console.error('Failed to fetch contact:', error.message);
                    next(false); // Prevent navigation if there's an error
                }
            }
        }
    ]
});

export default router;