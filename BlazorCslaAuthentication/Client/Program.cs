﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Csla.Configuration;
using Microsoft.AspNetCore.Components.Authorization;
using Csla.Blazor.Client.Authentication;

namespace BlazorCslaAuthentication.Client
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("app");

      builder.Services.AddOptions();
      builder.Services.AddAuthorizationCore();
      builder.Services.AddSingleton
        <AuthenticationStateProvider, CslaAuthenticationStateProvider>();
      builder.Services.AddSingleton<CslaUserService>();

      builder.UseCsla(c =>
      {
        c.DataPortal()
        .DefaultProxy(typeof(Csla.DataPortalClient.HttpProxy), "https://localhost:44325/api/dataportaltext/");
      });

      await builder.Build().RunAsync();
    }
  }
}
