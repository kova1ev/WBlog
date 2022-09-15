using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace WBlog.WebUI.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    public string? RequestId { get; set; }
    public string? statusCode { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public string? Message { get; set; }
    private readonly ILogger<ErrorModel> _logger;

    public ErrorModel(ILogger<ErrorModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        //TODO log
        Message = "Упс что-то пошло не так...";
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }

    public void OnGetByStatusCode([FromQuery] string code)
    {
        this.statusCode = code;
        switch (statusCode)
        {
            case "400":
                Message = "Страница не найдена";
                break;
            case "401":
                Message = "Требуется авторизация";
                break;
            case "403":
                Message = "Доступ ограничен";
                break;
            case "404":
                Message = "Страница не найдена";
                break;
            case "500":
                Message = "Внутрення ошибка сервера";
                break;
            case "503":
                Message = "Cервер временно не работает по техническим причинам";
                break;
            default:
                Message = "Что-то пошло не так...";
                break;
        }
    }
}
