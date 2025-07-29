<template>
  <nav class="mt-4" v-if="totalPages > 1">
    <ul class="pagination justify-content-center">
      <li class="page-item" :class="{ disabled: currentPage === 1 }">
        <a class="page-link" href="#" @click.prevent="$emit('update:currentPage', currentPage - 1)">
          <i class="fas fa-chevron-left"></i>
        </a>
      </li>
      <template v-for="page in displayedPages" :key="page">
        <li v-if="page === '...'" class="page-item disabled">
          <span class="page-link">...</span>
        </li>
        <li v-else class="page-item" :class="{ active: currentPage === page }">
          <a class="page-link" href="#" @click.prevent="$emit('update:currentPage', page)">{{ page }}</a>
        </li>
      </template>
      <li class="page-item" :class="{ disabled: currentPage === totalPages }">
        <a class="page-link" href="#" @click.prevent="$emit('update:currentPage', currentPage + 1)">
          <i class="fas fa-chevron-right"></i>
        </a>
      </li>
    </ul>
  </nav>
</template>

<script>
export default {
  name: 'Pagination',
  props: {
    currentPage: {
      type: Number,
      required: true
    },
    totalPages: {
      type: Number,
      required: true
    }
  },
  emits: ['update:currentPage'],
  computed: {
    displayedPages() {
      const total = this.totalPages;
      const current = this.currentPage;

      if (total <= 5) {
        return Array.from({ length: total }, (_, i) => i + 1);
      }

      if (current <= 3) {
        return [1, 2, 3, 4, '...', total];
      }

      if (current >= total - 2) {
        return [1, '...', total - 3, total - 2, total - 1, total];
      }

      return [1, '...', current - 1, current, current + 1, '...', total];
    }
  }
}
</script>

<style scoped>
.pagination {
  --bs-pagination-active-bg: #6366f1;
  --bs-pagination-active-border-color: #6366f1;
}

.pagination .page-link {
  padding: 0.5rem 0.75rem;
  font-size: 0.875rem;
  background-color: #1e293b;
  border-color: #2d3748;
  color: #e2e8f0;
}

.pagination .page-item.disabled .page-link {
  background-color: #1e293b;
  border-color: #2d3748;
  color: #64748b;
}

.page-item.active .page-link {
  background: linear-gradient(45deg, #6366f1, #8b5cf6);
  border-color: transparent;
}

.page-link:hover {
  background-color: #2d3748;
  color: #ffffff;
}
</style>