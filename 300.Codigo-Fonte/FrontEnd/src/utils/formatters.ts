export function formatarData(value: string | Date): string {
  return new Intl.DateTimeFormat('pt-BR').format(new Date(value));
}

export function formatarDocumento(value: string): string {
  return value.replace(/\D/g, '');
}
