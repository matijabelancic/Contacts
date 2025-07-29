import 'bootstrap/dist/css/bootstrap.min.css'
import * as bootstrap from 'bootstrap'
import { createApp } from 'vue'
import App from './App.vue'
import router from './router/router.js'
import { createI18n } from 'vue-i18n';
import hr from './i18n/hr.json';

const app = createApp(App)
const i18n = createI18n({
    locale: 'hr',
    fallbackLocale: 'hr',
    messages: {
        hr,
    },
});

app.use(router)
app.use(i18n);
app.mount('#app')

// Make Bootstrap available globally
window.bootstrap = bootstrap