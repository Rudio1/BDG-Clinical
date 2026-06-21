import type { ValidationProblemDetails } from '@/types/api/api';

export function useTratarErroFormulario() {
  function obterMensagem(error: unknown): string {
    if (error instanceof Error) {
      return error.message;
    }

    return 'Não foi possível concluir a operação.';
  }

  function obterErrosValidacao(problemDetails?: ValidationProblemDetails): Record<string, string> {
    if (!problemDetails?.errors) {
      return {};
    }

    return Object.fromEntries(
      Object.entries(problemDetails.errors).map(([campo, mensagens]) => [
        campo,
        mensagens.join(' '),
      ]),
    );
  }

  return {
    obterMensagem,
    obterErrosValidacao,
  };
}
