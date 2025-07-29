<template>
  <div class="container py-5">
    <ToastMessage
        v-if="showToast"
        :message="toastMessage"
        :type="toastType"
        @close="showToast = false"
    />
    <ContactsHeader @add="addContact" />
    <SearchBar v-model="searchQuery" />
    <ContactsTable
        :contacts="contacts"
        :sortColumn="sortColumn"
        :sortAscending="sortAscending"
        @update="updateContact"
        @delete="showDeleteDialog"
        @sort="updateSorting"
    />
    <Pagination
        v-if="totalPages > 1"
        v-model:currentPage="currentPage"
        :total-pages="totalPages"
    />
    <DeleteDialog
        ref="deleteDialog"
        :contact="contactToDelete"
        @confirm="deleteContact"
    />
  </div>
</template>

<script>
import ContactsHeader from '@/components/Contacts/ContactsHeader.vue';
import SearchBar from '@/components/Common/SearchBar.vue';
import ContactsTable from '@/components/Contacts/ContactsTable.vue';
import Pagination from '@/components/Common/Pagination.vue';
import DeleteDialog from '@/components/Common/DeleteDialog.vue';
import ToastMessage from "@/components/Common/ToastMessage.vue";
import { fetchContacts, fetchContactById, deleteContact } from '@/services/contactService.js';

const DEFAULT_PAGE_SIZE = 10;
const DEFAULT_SORT_COLUMN = '';
const DEFAULT_SORT_ASCENDING = true;

export default {
  name: 'Contacts',
  components: {
    ContactsHeader,
    SearchBar,
    ContactsTable,
    Pagination,
    DeleteDialog,
    ToastMessage,
  },
  data() {
    return {
      contacts: [],
      currentPage: 1,
      totalPages: 1,
      pageSize: DEFAULT_PAGE_SIZE,
      totalCount: 0,
      searchQuery: '',
      sortColumn: DEFAULT_SORT_COLUMN,
      sortAscending: DEFAULT_SORT_ASCENDING,
      contactToDelete: null,
      showToast: false,
      toastMessage: '',
      toastType: 'success',
    };
  },
  watch: {
    currentPage: "reloadOnChange",
    searchQuery: "reloadOnChange",
  },
  methods: {
    async loadContacts() {
      try {
        const response = await fetchContacts(
            this.currentPage,
            this.pageSize,
            this.searchQuery,
            this.sortColumn,
            this.sortAscending
        );
        this.contacts = response.items;
        this.currentPage = response.currentPage;
        this.totalPages = response.totalPages;
        this.pageSize = response.pageSize;
        this.totalCount = response.totalCount;
      } catch (error) {
        this.handleError('Error loading contacts', error);
      }
    },
    addContact() {
      this.$router.push({ name: 'NewContact' });
    },
    async updateContact(index) {
      try {
        const contactId = this.contacts[index].id;
        const contact = await fetchContactById(contactId);
        this.$router.push({
          name: 'EditContact',
          params: { id: contact.id, contact },
        });
      } catch (error) {
        this.handleError('Error fetching contact details', error);
      }
    },
    showDeleteDialog(index) {
      this.contactToDelete = this.contacts[index];
      this.$nextTick(() => {
        this.$refs.deleteDialog.show();
      });
    },
    async deleteContact() {
      try {
        await deleteContact(this.contactToDelete.id);
        this.contacts = this.contacts.filter(
            (c) => c.id !== this.contactToDelete.id
        );
        this.showToastNotification(this.$t('removingContactSuccess'), 'success');
        this.contactToDelete = null;
        this.$refs.deleteDialog.hide();
        await this.loadContacts();
      } catch (error) {
        this.showToastNotification(this.$t('removingError'), 'error');
        this.handleError('Error while deleting contact', error);
      }
    },
    updateSorting(column) {
      if (this.sortColumn === column) {
        this.sortAscending = !this.sortAscending;
      } else {
        this.sortColumn = column;
        this.sortAscending = true;
      }
      this.loadContacts();
    },
    reloadOnChange() {
      this.loadContacts();
    },
    showToastNotification(message, type = 'success') {
      this.toastMessage = message;
      this.toastType = type;
      this.showToast = true;
    },
    handleError(prefix, error) {
      console.error(`${prefix}:`, error.message);
    },
  },
  created() {
    const { message, type } = this.$route.query;
    if (message && type) {
      this.showToastNotification(message, type);
      this.$router.replace({ query: null });
    }
    this.loadContacts();
  },
};
</script>