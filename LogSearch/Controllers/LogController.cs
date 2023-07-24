using System.ComponentModel.DataAnnotations;
using LogSearch.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LogSearch.Controllers;

[ApiController]
[Route("[controller]")]
public class LogController: ControllerBase
{
    private readonly ILogSearchService _logSearchService;

    public LogController(ILogSearchService logSearchService)
    {
        _logSearchService = logSearchService;
    }

    [HttpGet]
    public IEnumerable<string> SearchLog(
        [Required] string fileName, 
        string? keyword,
        [BindRequired] int numberOfResults)
    {
        return _logSearchService.Search(fileName, keyword, numberOfResults);
    }
}