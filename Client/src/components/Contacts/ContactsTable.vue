<template>
  <div class="card bg-dark-subtle shadow-lg rounded-4 overflow-hidden">
    <div class="table-responsive">
      <table class="table table-hover mb-0">
        <thead class="table-dark">
        <tr>
          <th class="px-4" @click="$emit('sort', 'Name')">
            {{ $t('name') }}
            <span v-if="sortBy === 'Name'">
        <i :class="sortByAscending ? 'fas fa-arrow-up' : 'fas fa-arrow-down'"></i>
      </span>
          </th>
          <th @click="$emit('sort', 'Surname')">
            {{ $t('surname') }}
            <span v-if="sortBy === 'Surname'">
        <i :class="sortByAscending ? 'fas fa-arrow-up' : 'fas fa-arrow-down'"></i>
      </span>
          </th>
          <th @click="$emit('sort', 'Email')">
            {{ $t('email') }}
            <span v-if="sortBy === 'Email'">
        <i :class="sortByAscending ? 'fas fa-arrow-up' : 'fas fa-arrow-down'"></i>
      </span>
          </th>
          <th class="text-end px-4">{{ $t('actions') }}</th>
        </tr>
        </thead>

        <tbody>
        <tr v-for="(contact, index) in contacts"
            :key="index"
            class="align-middle">
          <td class="px-4">
            <div class="d-flex align-items-center">
              <div class="avatar me-3">
                {{ contact.name[0] }}{{ contact.surname[0] }}
              </div>
              {{ contact.name }}
            </div>
          </td>
          <td>{{ contact.surname }}</td>
          <td>{{ contact.email }}</td>
          <td class="text-end px-4">
            <button class="btn btn-outline-primary btn-sm me-2" @click="$emit('update', index)">
              <i class="fas fa-edit me-1"></i>{{ $t('editContact') }}
            </button>
            <button class="btn btn-outline-danger btn-sm" @click="$emit('delete', index)">
              <i class="fas fa-trash-alt me-1"></i>{{ $t('deleteContact') }}
            </button>
          </td>
        </tr>
        <tr v-if="contacts.length === 0">
          <td colspan="4" class="text-center py-4">
            <i class="fas fa-search me-2"></i>{{ $t('contactsNotFound') }}
          </td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ContactsTable',
  props: {
    contacts: {
      type: Array,
      required: true,
    },
    sortBy: {
      type: String, // Current column being sorted
      default: '',
    },
    sortByAscending: {
      type: Boolean, // Sort order (asc or desc)
      default: true,
    },
  },
  emits: ['update', 'delete', 'sort'],
}
</script>

<style scoped>
.avatar {
  width: 36px;
  height: 36px;
  background: linear-gradient(45deg, #6366f1, #8b5cf6);
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  color: white;
  font-weight: 600;
  font-size: 0.9rem;
}

.table {
  color: #e2e8f0;
}

.table-hover tbody tr:hover {
  background-color: rgba(99, 102, 241, 0.1);
}

.bg-dark-subtle {
  background-color: #1e293b !important;
}

.btn-outline-primary {
  color: #6366f1;
  border-color: #6366f1;
}

.btn-outline-primary:hover {
  background-color: #6366f1;
  border-color: #6366f1;
  color: white !important;
}

.btn-outline-danger {
  color: #ef4444;
  border-color: #ef4444;
}

.btn-outline-danger:hover {
  background-color: #ef4444;
  border-color: #ef4444;
  color: white !important;
}

.btn-outline-primary:hover i,
.btn-outline-danger:hover i {
  color: white !important;
}
</style>