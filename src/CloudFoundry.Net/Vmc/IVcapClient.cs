﻿namespace CloudFoundry.Net.Vmc
{
    using System.Collections.Generic;
    using System.IO;
    using Types;

    public interface IVcapClient
    {
        string CurrentToken { get; }
        string CurrentUri { get; }

        VcapClientResult Info();

        VcapClientResult Target(string uri);

        VcapClientResult Login();
        VcapClientResult Login(string email, string password);
        VcapClientResult ChangePassword(string newpassword);

        VcapClientResult Push(
            string name, string deployFQDN, ushort instances, DirectoryInfo path,
            uint memoryKB, string[] provisionedServiceNames);

        VcapClientResult Update(string appname, DirectoryInfo di);

        VcapClientResult BindService(string appName, string provisionedServiceName);
        VcapClientResult CreateService(string serviceName, string provisionedServiceName);
        VcapClientResult DeleteService(string provisionedServiceName);
        VcapClientResult UnbindService(string provisionedServiceName, string appName);

        IEnumerable<SystemService> GetSystemServices();
        IEnumerable<ProvisionedService> GetProvisionedServices();

        void Stop(Application app);
        void Start(Application app);
        void Restart(Application app);
        void Delete(string appName);

        Application GetApplication(string name);
        IEnumerable<Application> GetApplications();

        string GetLogs(Application application, ushort instanceNumber);

        IEnumerable<StatInfo> GetStats(Application application);

        IEnumerable<ExternalInstance> GetInstances(Application application);

        IEnumerable<Crash> GetAppCrash(Application application);

        IEnumerable<Application> ListApps();

        VcapResponse UpdateApplication(Application application);
    }
}