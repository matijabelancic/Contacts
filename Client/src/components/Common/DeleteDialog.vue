<template>
  <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content bg-white rounded-4 shadow">
        <div class="modal-header border-0 pt-4 px-4">
          <h5 class="modal-title text-gradient" id="deleteModalLabel">{{ $t('deleteContact') }}</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body px-4">
          <p class="mb-0">{{ $t('deleteContactWarning') }} <span class="fw-semibold">{{ contact?.name }} {{ contact?.surname }}</span>?</p>
        </div>
        <div class="modal-footer border-0 px-4 pb-4">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
            <i class="fas fa-times me-2"></i>{{ $t('cancel') }}
          </button>
          <button type="button" class="btn btn-danger" @click="confirmDelete">
            <i class="fas fa-trash-alt me-2"></i>{{ $t('delete') }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'DeleteDialog',
  props: {
    contact: {
      type: Object,
      default: () => ({
        name: '',
        surname: '',
        email: ''
      })
    }
  },
  methods: {
    show() {
      const modal = new bootstrap.Modal(document.getElementById('deleteModal'))
      modal.show()
    },
    confirmDelete() {
      this.$emit('confirm');
      const modal = bootstrap.Modal.getInstance(document.getElementById('deleteModal'));
      modal.hide();
    }
  }
}
</script>

<style scoped>
.text-gradient {
  background: linear-gradient(45deg, #6366f1, #8b5cf6);
  -webkit-background-clip: text;
  background-clip: text;
  color: transparent;
  font-weight: 700;
}

.modal-content {
  border: none;
}

.btn-danger {
  background: #ef4444;
  border: none;
  transition: all 0.3s ease;
}

.btn-danger:hover {
  background: #dc2626;
  box-shadow: 0 4px 12px rgba(239, 68, 68, 0.3);
}

.btn-secondary {
  background: #64748b;
  border: none;
  transition: all 0.3s ease;
}

.btn-secondary:hover {
  background: #475569;
}
</style>