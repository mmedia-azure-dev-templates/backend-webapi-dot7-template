﻿using Boilerplate.Application.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.Diagnostics;

namespace Boilerplate.Api.Configurations;

public static class OpenTelemetrySetup
{
    public static void AddOpenTemeletrySetup(this WebApplicationBuilder builder)
    {
        Activity.DefaultIdFormat = ActivityIdFormat.W3C;

        var jaegerConfig = builder.Configuration.GetSection("Jaeger");
        builder.Services
            .AddOpenTelemetry()
            .WithTracing(builder =>
            {
                builder
                .AddSource(OpenTelemetryExtensions.ServiceName)
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName: OpenTelemetryExtensions.ServiceName,
                            serviceVersion: OpenTelemetryExtensions.ServiceVersion)
                        .AddTelemetrySdk()
                        .AddEnvironmentVariableDetector());
                //.AddAspNetCoreInstrumentation(o =>
                //{
                //    o.RecordException = true;
                //})
                //.AddEntityFrameworkCoreInstrumentation(o =>
                //{
                //    o.SetDbStatementForText = true;
                //});

                if (jaegerConfig != null && !string.IsNullOrWhiteSpace(jaegerConfig.GetValue<string>("AgentHost")))
                {
                    builder.AddJaegerExporter(o =>
                    {
                        o.AgentHost = jaegerConfig["AgentHost"];
                        o.AgentPort = Convert.ToInt32(jaegerConfig["AgentPort"]);
                        o.MaxPayloadSizeInBytes = 4096;
                        o.ExportProcessorType = ExportProcessorType.Batch;
                        o.BatchExportProcessorOptions = new BatchExportProcessorOptions<Activity>
                        {
                            MaxQueueSize = 2048,
                            ScheduledDelayMilliseconds = 5000,
                            ExporterTimeoutMilliseconds = 30000,
                            MaxExportBatchSize = 512,
                        };
                    });
                }
            });
    }
}