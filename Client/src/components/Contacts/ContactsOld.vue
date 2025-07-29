<template>
  <div class="container py-5">
    <ContactsHeader @add="addContact" />
    <SearchBar v-model="searchQuery" />
    <ContactsTable
        :contacts="paginatedFilteredContacts"
        @update="updateContact"
        @delete="showDeleteDialog"
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
import ContactsHeader from '@/components/Contacts/ContactsHeader.vue'
import SearchBar from '@/components/Common/SearchBar.vue'
import ContactsTable from '@/components/Contacts/ContactsTable.vue'
import Pagination from '@/components/Common/Pagination.vue'
import DeleteDialog from '@/components/Common/DeleteDialog.vue'


export default {
  name: 'Contacts',
  components: {
    ContactsHeader,
    SearchBar,
    ContactsTable,
    Pagination,
    DeleteDialog
  },
  data() {
    return {
      contacts: [
        { name: 'John', surname: 'Doe', email: 'john.doe@company.com' },
        { name: 'Emma', surname: 'Wilson', email: 'emma.w@designstudio.com' },
        { name: 'Michael', surname: 'Chen', email: 'michael.chen@tech.io' },
        { name: 'Sofia', surname: 'Rodriguez', email: 'sofia.r@creative.net' },
        { name: 'James', surname: 'Anderson', email: 'j.anderson@systems.co' },
        { name: 'Olivia', surname: 'Taylor', email: 'olivia.t@digital.com' },
        { name: 'Lucas', surname: 'Martinez', email: 'lucas.m@agency.com' },
        { name: 'Isabella', surname: 'Brown', email: 'i.brown@solutions.net' },
        { name: 'Alexander', surname: 'Johnson', email: 'alex.j@innovation.io' },
        { name: 'Sophia', surname: 'Davis', email: 'sophia.d@global.com' },
        { name: 'William', surname: 'Garcia', email: 'w.garcia@enterprise.co' },
        { name: 'Victoria', surname: 'Lee', email: 'victoria.l@studio.net' },
        { name: 'Benjamin', surname: 'Wong', email: 'ben.wong@creative.com' },
        { name: 'Mia', surname: 'Patel', email: 'mia.p@technology.io' },
        { name: 'Ethan', surname: 'Nguyen', email: 'ethan.n@solutions.com' }
      ],
      currentPage: 1,
      pageSize: 5,
      searchQuery: '',
      contactToDelete: null
    }
  },
  computed: {
    filteredContacts() {
      if (!this.searchQuery) return this.contacts

      const query = this.searchQuery.toLowerCase()
      return this.contacts.filter(contact =>
          contact.name.toLowerCase().includes(query) ||
          contact.surname.toLowerCase().includes(query) ||
          contact.email.toLowerCase().includes(query)
      )
    },
    totalPages() {
      return Math.ceil(this.filteredContacts.length / this.pageSize)
    },
    paginatedFilteredContacts() {
      const start = (this.currentPage - 1) * this.pageSize
      const end = start + this.pageSize
      return this.filteredContacts.slice(start, end)
    }
  },
  watch: {
    '$route'(to) {
      if (to.name === 'Contacts') {
        this.contactToDelete = null
      }
    },
    searchQuery() {
      this.currentPage = 1;
    },
    filteredContacts() {
      if (this.currentPage > this.totalPages) {
        this.currentPage = this.totalPages || 1;
      }
    }
  },
  methods: {
    addContact() {
      this.$router.push({ name: 'NewContact' })
    },
    updateContact(index) {
      const globalIndex = (this.currentPage - 1) * this.pageSize + index
      const contact = this.filteredContacts[globalIndex]
      this.$router.push({
        name: 'EditContact',
        params: {
          id: globalIndex,
          contact: contact
        }
      })
    },
    showDeleteDialog(index) {
      const globalIndex = (this.currentPage - 1) * this.pageSize + index
      this.contactToDelete = this.filteredContacts[globalIndex]
      this.$nextTick(() => {
        this.$refs.deleteDialog.show()
      })
    },
    deleteContact() {
      const index = this.contacts.findIndex(c =>
          c.name === this.contactToDelete.name &&
          c.surname === this.contactToDelete.surname &&
          c.email === this.contactToDelete.email
      )
      if (index !== -1) {
        this.contacts.splice(index, 1)
        this.contactToDelete = null

        // Adjust current page if necessary
        const maxPage = Math.ceil(this.filteredContacts.length / this.pageSize)
        if (this.currentPage > maxPage) {
          this.currentPage = Math.max(1, maxPage)
        }
      }
    }
  }
}
</script>