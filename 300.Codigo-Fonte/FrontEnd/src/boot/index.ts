import type { App } from 'vue';

import { api } from './axios';
import { registerGlobalComponents } from './components';

export { api, registerGlobalComponents };

export function boot(app: App): void {
  registerGlobalComponents(app);
}
