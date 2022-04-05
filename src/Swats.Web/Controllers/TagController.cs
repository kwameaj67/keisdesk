using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swats.Model;
using Swats.Model.Commands;
using Swats.Model.Queries;
using Swats.Web.Extensions;

namespace Swats.Web.Controllers;

public class TagController : MethodController
{
    private readonly ILogger<TagController> logger;
    private readonly IMediator mediatr;

    public TagController(ILogger<TagController> logger, IMediator mediatr)
    {
        this.logger = logger;
        this.mediatr = mediatr;
    }

    [HttpGet("tag.list", Name = nameof(ListTags))]
    public async Task<IActionResult> ListTags([FromQuery] ListTagsCommand command)
    {
        const string msg = $"GET::{nameof(TagController)}::{nameof(ListTags)}";
        logger.LogInformation(msg);

        var result = await mediatr.Send(command);
        if (result.IsFailed)
        {
            return BadRequest(new ErrorResult
            {
                Ok = false,
                Errors = result.Reasons.Select(s => s.Message)
            });
        }

        return Ok(new ListResult<FetchTag>
        {
            Ok = true,
            Data = result.Value
        });
    }
    
    [HttpGet("tag.get", Name = nameof(GetTag))]
    public async Task<IActionResult> GetTag([FromQuery] GetTagCommand command)
    {
        const string msg = $"GET::{nameof(BusinessHourController)}::{nameof(GetTag)}";
        logger.LogInformation(msg);

        var result = await mediatr.Send(command);
        if (result.IsFailed)
        {
            return BadRequest(new ErrorResult
            {
                Ok = false,
                Errors = result.Reasons.Select(s => s.Message)
            });
        }

        return Ok(new SingleResult<FetchTag>
        {
            Ok = true,
            Data = result.Value
        });
    }

    [HttpPost("tag.create", Name = nameof(CreateTag))]
    public async Task<IActionResult> CreateTag(CreateTagCommand command)
    {
        const string msg = $"POST::{nameof(TagController)}::{nameof(CreateTag)}";
        logger.LogInformation(msg);
        
        command.CreatedBy = Request.HttpContext.UserId();
        var result = await mediatr.Send(command);

        if (result.IsFailed)
        {
            return BadRequest(new ErrorResult
            {
                Ok = false,
                Errors = result.Reasons.Select(s => s.Message)
            });
        }
        
        var uri = $"/methods/tag.get?id={result.Value}";
        return Created(uri, new SingleResult<string>
        {
            Ok = true,
            Data = result.Value
        });
    }
}