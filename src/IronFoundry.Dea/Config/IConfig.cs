﻿namespace IronFoundry.Dea.Config
{
    using System;
    using System.Net;

    public interface IConfig
    {
        ushort MaxMemoryMB { get; }
        bool DisableDirCleanup { get; }
        string DropletDir { get; }
        string AppDir { get; }
        string NatsHost { get; }
        ushort NatsPort { get; }

        IPAddress LocalIPAddress { get; }

        ushort FilesServicePort { get; }
        Uri FilesServiceUri { get; }
        ServiceCredentials FilesCredentials { get; }

        ushort MonitoringServicePort { get; }
        Uri MonitoringServiceUri { get; }
        ServiceCredentials MonitoringCredentials { get; }
    }
}