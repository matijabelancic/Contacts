<template>
  <div class="search-container mb-4">
    <div class="input-group">
      <span class="input-group-text bg-dark-subtle border-0">
        <i class="fas fa-search text-muted"></i>
      </span>
      <input
          type="text"
          class="form-control search-input"
          :value="modelValue"
          @input="$emit('update:modelValue', $event.target.value)"
          :placeholder="$t('searchPlaceholder')"
      >
    </div>
  </div>
</template>

<script>
export default {
  name: 'SearchBar',
  props: {
    modelValue: {
      type: String,
      default: ''
    }
  },
  emits: ['update:modelValue']
}
</script>

<style scoped>
.search-input {
  background-color: #f8fafc;
  border: 1px solid #e2e8f0;
  color: #1e293b;
  padding: 0.75rem 1rem;
  transition: all 0.3s ease;
}

.search-input:focus {
  background-color: #ffffff;
  border-color: #6366f1;
  color: #1e293b;
  box-shadow: 0 0 0 0.2rem rgba(99, 102, 241, 0.15);
}

.search-input::placeholder {
  color: #94a3b8;
}

.input-group-text {
  background-color: #f8fafc;
  border: 1px solid #e2e8f0;
  border-right: none;
  color: #94a3b8;
}

.search-container:hover .search-input,
.search-container:hover .input-group-text {
  background-color: #ffffff;
}

.search-container .input-group {
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
  border-radius: 0.5rem;
  overflow: hidden;
}

.input-group-text {
  color: #64748b;
}
</style>