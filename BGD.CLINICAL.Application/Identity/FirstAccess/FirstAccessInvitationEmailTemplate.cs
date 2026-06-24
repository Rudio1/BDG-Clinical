using System.Net;

namespace BGD.CLINICAL.Application.Identity.FirstAccess;

internal static class FirstAccessInvitationEmailTemplate
{
    private const string ColorBackground = "#ecf5f0";
    private const string ColorSurface = "#ffffff";
    private const string ColorText = "#142820";
    private const string ColorMuted = "#5c8572";
    private const string ColorPrimary = "#059669";
    private const string ColorPrimaryDark = "#047857";
    private const string ColorPrimaryLight = "#ecfdf5";
    private const string ColorPrimaryBorder = "#d5e8dd";

    public static string Build(string nome, string empresaNome, string firstAccessUrl, int expirationHours)
    {
        var safeNome = Encode(nome);
        var safeEmpresa = Encode(empresaNome);
        var safeUrl = Encode(firstAccessUrl);
        var validityLabel = FormatValidity(expirationHours);

        return $"""
            <!DOCTYPE html>
            <html lang="pt-BR">
            <head>
              <meta charset="UTF-8" />
              <meta name="viewport" content="width=device-width, initial-scale=1.0" />
              <meta http-equiv="X-UA-Compatible" content="IE=edge" />
              <title>Convite de primeiro acesso — BGD Clinical</title>
              <!--[if mso]>
              <noscript>
                <xml>
                  <o:OfficeDocumentSettings>
                    <o:PixelsPerInch>96</o:PixelsPerInch>
                  </o:OfficeDocumentSettings>
                </xml>
              </noscript>
              <![endif]-->
            </head>
            <body style="margin:0;padding:0;background-color:{ColorBackground};font-family:'Segoe UI',Roboto,Helvetica,Arial,sans-serif;color:{ColorText};-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;">
              <div style="display:none;max-height:0;overflow:hidden;mso-hide:all;">
                Você foi convidado(a) para acessar o BGD Clinical. Conclua seu primeiro acesso em {validityLabel}.
              </div>
              <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0" style="background-color:{ColorBackground};padding:40px 16px;">
                <tr>
                  <td align="center">
                    <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0" style="max-width:520px;background-color:{ColorSurface};border:1px solid {ColorPrimaryBorder};border-radius:12px;overflow:hidden;box-shadow:0 8px 24px rgba(20,40,32,0.08);">
                      <tr>
                        <td style="background-color:{ColorPrimaryDark};background-image:linear-gradient(135deg,{ColorPrimaryDark} 0%,{ColorPrimary} 55%,#10b981 100%);padding:32px 32px 28px;">
                          <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                              <td>
                                <table role="presentation" cellspacing="0" cellpadding="0" border="0">
                                  <tr>
                                    <td style="background-color:rgba(255,255,255,0.16);border:1px solid rgba(255,255,255,0.22);border-radius:999px;padding:6px 14px;">
                                      <span style="font-size:11px;font-weight:700;letter-spacing:0.1em;text-transform:uppercase;color:#ffffff;">
                                        BGD Clinical
                                      </span>
                                    </td>
                                  </tr>
                                </table>
                                <p style="margin:14px 0 0;font-size:13px;line-height:1.5;color:rgba(255,255,255,0.82);">
                                  Gestão clínica inteligente
                                </p>
                                <h1 style="margin:18px 0 0;font-size:24px;font-weight:700;line-height:1.25;color:#ffffff;">
                                  Primeiro acesso
                                </h1>
                                <p style="margin:10px 0 0;font-size:14px;line-height:1.6;color:rgba(255,255,255,0.88);">
                                  Convite para <strong style="color:#ffffff;">{safeEmpresa}</strong>
                                </p>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td style="padding:32px 32px 28px;background-color:{ColorSurface};">
                          <p style="margin:0 0 16px;font-size:16px;line-height:1.7;color:{ColorText};">
                            Olá, <strong>{safeNome}</strong>!
                          </p>
                          <p style="margin:0 0 24px;font-size:15px;line-height:1.7;color:{ColorText};">
                            Você foi convidado(a) para acessar a plataforma. Para começar, confirme seu e-mail e defina uma senha de acesso — o mesmo fluxo da tela de primeiro acesso do sistema.
                          </p>
                          <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0" style="margin:0 0 28px;background-color:{ColorPrimaryLight};border:1px solid {ColorPrimaryBorder};border-radius:8px;">
                            <tr>
                              <td style="padding:18px 20px;">
                                <p style="margin:0 0 6px;font-size:11px;font-weight:700;letter-spacing:0.08em;text-transform:uppercase;color:{ColorPrimary};">
                                  Como funciona
                                </p>
                                <p style="margin:0;font-size:14px;line-height:1.65;color:{ColorText};">
                                  1. Clique no botão abaixo<br />
                                  2. Informe o e-mail cadastrado pela clínica<br />
                                  3. Crie sua senha e entre no sistema
                                </p>
                              </td>
                            </tr>
                          </table>
                          <table role="presentation" cellspacing="0" cellpadding="0" border="0" align="center" style="margin:0 auto 28px;">
                            <tr>
                              <td align="center" bgcolor="{ColorPrimary}" style="border-radius:8px;background-color:{ColorPrimary};">
                                <a href="{safeUrl}" target="_blank" style="display:inline-block;padding:14px 28px;font-size:15px;font-weight:600;line-height:1.2;color:#ffffff;text-decoration:none;border-radius:8px;mso-padding-alt:0;">
                                  <!--[if mso]>
                                  <i style="letter-spacing:28px;mso-font-width:-100%;mso-text-raise:18pt;">&nbsp;</i>
                                  <![endif]-->
                                  <span style="mso-text-raise:9pt;">Concluir primeiro acesso</span>
                                  <!--[if mso]>
                                  <i style="letter-spacing:28px;mso-font-width:-100%;">&nbsp;</i>
                                  <![endif]-->
                                </a>
                              </td>
                            </tr>
                          </table>
                          <p style="margin:0 0 8px;font-size:13px;line-height:1.6;color:{ColorMuted};">
                            Se o botão não funcionar, copie e cole este link no navegador:
                          </p>
                          <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0" style="margin:0 0 24px;">
                            <tr>
                              <td style="padding:12px 14px;background-color:{ColorBackground};border:1px dashed {ColorPrimaryBorder};border-radius:8px;">
                                <a href="{safeUrl}" style="font-size:12px;line-height:1.55;word-break:break-all;color:{ColorPrimaryDark};text-decoration:underline;">
                                  {safeUrl}
                                </a>
                              </td>
                            </tr>
                          </table>
                          <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0" style="border-top:1px solid {ColorPrimaryBorder};">
                            <tr>
                              <td style="padding-top:20px;">
                                <p style="margin:0 0 8px;font-size:12px;line-height:1.6;color:{ColorMuted};">
                                  Este convite expira em <strong style="color:{ColorText};">{validityLabel}</strong>.
                                </p>
                                <p style="margin:0;font-size:12px;line-height:1.6;color:{ColorMuted};">
                                  O link é pessoal e intransferível. Se você não esperava este e-mail, ignore-o com segurança.
                                </p>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td style="background-color:{ColorPrimaryLight};border-top:1px solid {ColorPrimaryBorder};padding:16px 32px;text-align:center;">
                          <p style="margin:0;font-size:11px;line-height:1.5;color:{ColorMuted};">
                            © BGD Clinical · Mensagem automática — não responda este e-mail.
                          </p>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </body>
            </html>
            """;
    }

    private static string Encode(string value) =>
        WebUtility.HtmlEncode(value);

    private static string FormatValidity(int expirationHours)
    {
        if (expirationHours <= 0)
        {
            return "pouco tempo";
        }

        if (expirationHours % 24 == 0)
        {
            var days = expirationHours / 24;
            return days == 1 ? "1 dia" : $"{days} dias";
        }

        return expirationHours == 1 ? "1 hora" : $"{expirationHours} horas";
    }
}
