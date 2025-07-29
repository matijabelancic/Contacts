<template>
  <div class="header-section mb-4">
    <h2 class="text-gradient"> {{ $t('contactsList') }}</h2>
    <button class="btn btn-primary add-btn" @click="$emit('add')">
      <i class="fas fa-plus me-2"></i>{{ $t('newContact') }}
    </button>
  </div>
</template>

<script>
export default {
  name: 'ContactsHeader',
  emits: ['add']
}
</script>

<style scoped>
.header-section {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.text-gradient {
  background: linear-gradient(45deg, #6366f1, #8b5cf6);
  -webkit-background-clip: text;
  background-clip: text;
  color: transparent;
  font-weight: 700;
}

.add-btn {
  background: linear-gradient(45deg, #6366f1, #8b5cf6);
  border: none;
  padding: 0.7rem 1.5rem;
  border-radius: 12px;
  transition: all 0.3s ease;
}

.add-btn:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(99, 102, 241, 0.3);
}
</style>