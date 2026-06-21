import '@quasar/extras/material-icons/material-icons.css';
import 'quasar/src/css/index.sass';
import '@/css/app.scss';

import { createPinia } from 'pinia';
import { Notify, Quasar } from 'quasar';
import { createApp } from 'vue';

import App from './App.vue';
import { registerGlobalComponents } from './boot';
import router from './router';

const app = createApp(App);

app.use(createPinia());
app.use(router);
app.use(Quasar, {
  plugins: {
    Notify,
  },
});

registerGlobalComponents(app);

app.mount('#app');
