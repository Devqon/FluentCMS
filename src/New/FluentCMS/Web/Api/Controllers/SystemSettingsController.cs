﻿using AutoMapper;
using FluentCMS.Entities;
using FluentCMS.Services;
using FluentCMS.Web.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FluentCMS.Web.Api.Controllers;

[Route("sys/api/[controller]/[action]")]
public class SystemSettingsController(
    ISystemSettingsService systemSettingsService,
    IAppService appService,
    IMapper mapper) : BaseController
{
    [HttpGet]
    public async Task<IApiResult<SystemSettings>> Get(CancellationToken cancellationToken = default)
    {
        var systemSettings = await systemSettingsService.Get(cancellationToken);
        return Ok(systemSettings);
    }

    [HttpGet]
    public async Task<IApiPagingResult<AppResponse>> GetAllApps(CancellationToken cancellationToken = default)
    {
        var apps = await appService.GetAll(cancellationToken);
        var appsResponse = mapper.Map<List<AppResponse>>(apps);
        return OkPaged(appsResponse);
    }

    [HttpPut]
    public async Task<IApiResult<SystemSettings>> Update(SystemSettingsUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var systemSettings = new SystemSettings
        {
            SuperUsers = request.SuperUsers
        };
        var updated = await systemSettingsService.Update(systemSettings, cancellationToken);
        return Ok(updated);
    }

    [HttpPost]
    public async Task<IApiResult<AppResponse>> CreateApp(AppCreateRequest request, CancellationToken cancellationToken = default)
    {
        var app = mapper.Map<App>(request);
        var created = await appService.Create(app, cancellationToken);
        var response = mapper.Map<AppResponse>(created);
        return Ok(response);
    }

    [HttpDelete("{appId}")]
    public async Task<IApiResult<bool>> DeleteApp([FromRoute] Guid appId, CancellationToken cancellationToken = default)
    {
        _ = await appService.Delete(appId, cancellationToken);
        return Ok(true);
    }
}