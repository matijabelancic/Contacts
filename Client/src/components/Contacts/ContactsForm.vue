<template>
  <div class="container py-5">
    <ToastMessage
        v-if="showToast"
        :message="toastMessage"
        :type="toastType"
        @close="showToast = false"
    />
    <div class="header-section mb-4">
      <h2 class="text-gradient">{{ isEditMode ? $t('editContact') : $t('newContact') }}</h2>
      <button class="btn btn-secondary back-btn" @click="goBack">
        <i class="fas fa-arrow-left me-2"></i>{{ $t('back') }}
      </button>
    </div>
    <div class="card shadow-lg rounded-4">
      <div class="card-body p-4">
        <form @submit.prevent="handleSubmit">
          <div class="row g-4">
            <!-- Name Field -->
            <div class="col-md-6">
              <div class="form-group">
                <label class="form-label">{{ $t('name') }}</label>
                <input
                    type="text"
                    class="form-control custom-input"
                    v-model="formData.name"
                    :class="['form-control', { 'is-invalid': errors.name }]"
                    :placeholder="$t('enterName')"
                />
                <div class="invalid-feedback" v-if="errors.name">{{ errors.name }}</div>
              </div>
            </div>
            <!-- Surname Field -->
            <div class="col-md-6">
              <div class="form-group">
                <label class="form-label">{{ $t('surname') }}</label>
                <input
                    type="text"
                    class="form-control custom-input"
                    v-model="formData.surname"
                    :class="{ 'is-invalid': errors.surname }"
                    :placeholder="$t('enterSurname')"
                />
                <div class="invalid-feedback" v-if="errors.surname">{{ errors.surname }}</div>
              </div>
            </div>
            <!-- Email Field -->
            <div class="col-12">
              <div class="form-group">
                <label class="form-label">{{ $t('email') }}</label>
                <input
                    type="email"
                    class="form-control custom-input"
                    v-model="formData.email"
                    :class="{ 'is-invalid': errors.email }"
                    :placeholder="$t('enterEmail')"
                />
                <div class="invalid-feedback" v-if="errors.email">{{ errors.email }}</div>
              </div>
            </div>
            <!-- Avatar Preview -->
            <div class="col-12 text-center mb-4">
              <div class="avatar-preview mx-auto">
                {{ formData.name ? formData.name[0] : '' }}{{ formData.surname ? formData.surname[0] : '' }}
              </div>
            </div>
            <!-- Action Buttons -->
            <div class="col-12 text-center">
              <button type="button" class="btn btn-outline-danger me-3" @click="goBack">
                <i class="fas fa-times me-2"></i>{{ $t('cancel') }}
              </button>
              <button type="submit" class="btn btn-primary submit-btn">
                <i class="fas fa-check me-2"></i>{{ isEditMode ? $t('saveChanges')  : $t('createContact') }}
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import { createContact, updateContact } from "@/services/contactService.js";
import ToastMessage from "@/components/Common/ToastMessage.vue";

const DEFAULT_FORM = {
  name: "",
  surname: "",
  email: "",
};

export default {
  name: "ContactsForm",
  props: {
    contact: {
      type: Object,
      default: () => ({ ...DEFAULT_FORM }),
    },
    isEditing: {
      type: Boolean,
      default: false,
    },
  },
  components: { ToastMessage },
  data() {
    return {
      formData: { ...DEFAULT_FORM },
      errors: { ...DEFAULT_FORM },
      showToast: false,
      toastMessage: "",
      toastType: "success",
    };
  },
  computed: {
    isEditMode() {
      return this.isEditing;
    },
  },
  created() {
    if (this.isEditMode) {
      this.formData = { ...this.contact };
    }
  },
  watch: {
    contact(newContact) {
      if (this.isEditMode) {
        this.formData = { ...newContact };
      }
    },
  },
  methods: {
    goBack() {
      this.$router.push({ name: "Contacts" });
    },
    validateForm() {
      let isValid = true;
      this.errors = { ...DEFAULT_FORM };

      if (!this.formData.name.trim()) {
        this.errors.name = this.$t('required', { field: this.$t('name') });
        isValid = false;
      }
      if (!this.formData.surname.trim()) {
        this.errors.surname = this.$t('required', { field: this.$t('surname') });
        isValid = false;
      }
      if (!this.validateEmail(this.formData.email)) {
        this.errors.email = this.$t('invalidEmail');
        isValid = false;
      }
      return isValid;
    },
    validateEmail(email) {
      const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      return re.test(email);
    },
    async handleSubmit() {
      if (!this.validateForm()) return;

      try {
        const action = this.isEditMode ? updateContact : createContact;
        const message = this.$t('savingContactSuccess')

        if (this.isEditMode) {
          await action(this.contact.id, this.formData);
        } else {
          await action(this.formData);
        }

        this.$router.push({
          name: "Contacts",
          query: { message, type: "success" },
        });
      } catch (error) {
        this.showToastNotification(error.message || this.$t('savingError'), "error");
      }
    },
    showToastNotification(message, type) {
      this.toastMessage = message;
      this.toastType = type;
      this.showToast = true;
    },
  },
};
</script>

<style scoped>
.text-gradient {
  background: linear-gradient(45deg, #6366f1, #8b5cf6);
  -webkit-background-clip: text;
  background-clip: text;
  color: transparent;
  font-weight: 700;
}

.back-btn {
  color: #e2e8f0;
  border-color: #4b5563;
  padding: 0.7rem 1.5rem;
  border-radius: 12px;
  transition: all 0.3s ease;
}

.back-btn:hover {
  background-color: #4b5563;
  color: white;
  transform: translateY(-2px);
}

.custom-input {
  background-color: #ffffff;
  border: 1px solid #6366f1;
  color: #1e293b;
  padding: 0.75rem 1rem;
  border-radius: 0.5rem;
  transition: all 0.3s ease;
}

.custom-input:focus {
  background-color: #ffffff;
  border-color: #6366f1;
  color: #1e293b;
  box-shadow: 0 0 0 0.2rem rgba(99, 102, 241, 0.15);
}

.custom-input::placeholder {
  color: #64748b;
}

.form-label {
  color: #6366f1 !important;
  font-weight: 500;
  margin-bottom: 0.5rem;
}

.avatar-preview {
  width: 80px;
  height: 80px;
  background: linear-gradient(45deg, #6366f1, #8b5cf6);
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  color: white;
  font-weight: 600;
  font-size: 1.5rem;
  margin-top: 1rem;
}

.submit-btn {
  background: linear-gradient(45deg, #6366f1, #8b5cf6);
  border: none;
  padding: 0.7rem 1.5rem;
  border-radius: 12px;
  transition: all 0.3s ease;
}

.submit-btn:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(99, 102, 241, 0.3);
}

.invalid-feedback {
  color: #ef4444;
}

.is-invalid {
  border-color: #ef4444 !important;
}

.is-invalid:focus {
  box-shadow: 0 0 0 0.2rem rgba(239, 68, 68, 0.15) !important;
}
</style>