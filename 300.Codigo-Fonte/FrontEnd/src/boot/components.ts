import type { App } from 'vue';

import AppEmptyState from '@/components/shared/AppEmptyState.vue';
import AppPageHeader from '@/components/shared/AppPageHeader.vue';

export function registerGlobalComponents(app: App): void {
  app.component('AppEmptyState', AppEmptyState);
  app.component('AppPageHeader', AppPageHeader);
}
