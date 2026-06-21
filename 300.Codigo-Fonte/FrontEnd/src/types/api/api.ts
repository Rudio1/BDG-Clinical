export interface ApiResponse<TData> {
  data: TData;
  success: boolean;
  message?: string;
}

export interface ApiError {
  status?: number;
  title?: string;
  detail?: string;
  message?: string;
}

export interface ValidationProblemDetails extends ApiError {
  errors?: Record<string, string[]>;
}
