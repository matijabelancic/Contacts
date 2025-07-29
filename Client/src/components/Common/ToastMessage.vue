<template>
  <div
      v-if="visible"
      class="toast-message"
      :class="typeClass"
      role="alert"
      aria-live="assertive"
      aria-atomic="true"
  >
    <div class="toast-content">
      <p class="message-text">{{ message }}</p>
      <button
          type="button"
          class="toast-close"
          @click="hideToast"
          aria-label="Close"
      >
        &times;
      </button>
    </div>
  </div>
</template>


<script>
export default {
  name: "ToastMessage",
  props: {
    message: {
      type: String,
      required: true
    },
    type: {
      type: String, // "success" or "error"
      default: "success"
    },
    duration: {
      type: Number,
      default: 3000 // Toast hides automatically after 5 seconds
    }
  },
  data() {
    return {
      visible: true
    };
  },
  computed: {
    typeClass() {
      // Add a class based on the type
      return this.type === "success" ? "toast-success" : "toast-error";
    }
  },
  methods: {
    hideToast() {
      this.visible = false; // Hide toast on user action
    }
  },
  mounted() {
    // Automatically hide toast after the duration
    setTimeout(() => {
      this.hideToast();
    }, this.duration);
  }
};
</script>

<style scoped>
.toast-message {
  position: fixed;
  top: 20px;
  right: 20px;
  z-index: 9999;
  padding: 1rem 1.5rem;
  border: none;
  border-radius: 8px;
  color: #ffffff;
  font-size: 1rem;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  animation: fade-in-out 0.2s ease;
  display: flex;
  flex-direction: column;
  gap: 6px;
}

.toast-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 10px; /* Space between message and button */
}

.toast-success {
  background-color: #22c55e; /* Green */
}

.toast-error {
  background-color: #ef4444; /* Red */
}

.message-text {
  margin: 0;
  flex-grow: 1; /* Allows the text to take up the remaining space */
}

.toast-close {
  background: transparent;
  border: none;
  color: white;
  font-size: 1.5rem;
  cursor: pointer;
  margin-left: auto; /* Keeps the button on the far right */
  padding: 0;
}

.toast-close:hover {
  opacity: 0.6; /* Slightly less intense on hover */
}

@keyframes fade-in-out {
  from {
    opacity: 0;
    transform: translateY(-20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}
</style>
