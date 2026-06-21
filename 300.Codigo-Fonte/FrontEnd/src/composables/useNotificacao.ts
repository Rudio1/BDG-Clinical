import { Notify } from 'quasar';

export function useNotificacao() {
  function sucesso(message: string): void {
    Notify.create({ type: 'positive', message });
  }

  function erro(message: string): void {
    Notify.create({ type: 'negative', message });
  }

  function info(message: string): void {
    Notify.create({ type: 'info', message });
  }

  return {
    sucesso,
    erro,
    info,
  };
}
