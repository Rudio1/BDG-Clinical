import type { App } from 'vue';

import AppAuthShell from '@/components/design-system/AppAuthShell.vue';
import AppBrand from '@/components/design-system/AppBrand.vue';
import AppMetricCard from '@/components/design-system/AppMetricCard.vue';
import AppEmptyState from '@/components/shared/AppEmptyState.vue';
import AppPageHeader from '@/components/shared/AppPageHeader.vue';

export function registerGlobalComponents(app: App): void {
  app.component('AppAuthShell', AppAuthShell);
  app.component('AppBrand', AppBrand);
  app.component('AppMetricCard', AppMetricCard);
  app.component('AppEmptyState', AppEmptyState);
  app.component('AppPageHeader', AppPageHeader);
}
