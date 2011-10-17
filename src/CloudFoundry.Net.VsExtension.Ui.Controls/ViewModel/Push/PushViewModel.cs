﻿namespace CloudFoundry.Net.VsExtension.Ui.Controls.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Threading;
    using CloudFoundry.Net.Types;
    using CloudFoundry.Net.VsExtension.Ui.Controls.Model;
    using CloudFoundry.Net.VsExtension.Ui.Controls.Utilities;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using CloudFoundry.Net.VsExtension.Ui.Controls.Mvvm;

    public class PushViewModel : DialogViewModel
    {
        private Cloud selectedCloud;
        private ObservableCollection<ProvisionedService> applicationServices;
        private string name;
        private string url;
        private int selectedMemory;
        private ushort instances;

        public RelayCommand ManageCloudsCommand { get; private set; }
        public RelayCommand AddAppServiceCommand { get; private set; }

        public PushViewModel() : base(Messages.PushDialogResult)
        {
            this.applicationServices = new ObservableCollection<ProvisionedService>();
            ManageCloudsCommand = new RelayCommand(ManageClouds);
            AddAppServiceCommand = new RelayCommand(AddAppService, CanAddAppService);
            SelectedMemory = MemoryLimits[0];
        }

        protected override void RegisterGetData()
        {
            Messenger.Default.Register<NotificationMessageAction<PushViewModel>>(this,
                message =>
                {
                    if (message.Notification.Equals(Messages.GetPushAppData))
                        message.Execute(this);
                    Messenger.Default.Unregister(this);
                });
        }

        protected override void InitializeData()
        {
            Messenger.Default.Send(new NotificationMessageAction<Guid>(Messages.SetPushAppData,
                (id) =>
                {                    
                    this.SelectedCloud = Clouds.SingleOrDefault(i => i.ID == id);
                }));
        }

        private void ManageClouds()
        {
            Messenger.Default.Send(new NotificationMessageAction<bool>(Messages.ManageClouds, (confirmed) => { }));
        }
       
        private void AddAppService()
        {
            
            Messenger.Default.Register<NotificationMessageAction<Cloud>>(this,
                message =>
                {
                    if (message.Notification.Equals(Messages.SetAddApplicationServiceData))
                        message.Execute(this.SelectedCloud);
                });

            Messenger.Default.Send(new NotificationMessageAction<bool>(Messages.AddApplicationService, (confirmed) =>
            {
                Messenger.Default.Send(new NotificationMessageAction<AddApplicationServiceViewModel>(Messages.GetAddApplicationServiceData,
                    (viewModel) =>
                    {
                        if (!this.ApplicationServices.Contains(viewModel.SelectedService,new ProvisionedServiceEqualityComparer()))
                            this.ApplicationServices.Add(viewModel.SelectedService);
                    }));
            }));
            
        }

        private bool CanAddAppService()
        {
            return this.SelectedCloud != null && this.SelectedCloud.Services.Count > 0;
        }

        public Cloud SelectedCloud
        {
            get { return this.selectedCloud; }
            set
            {
                this.selectedCloud = value;
                if (this.selectedCloud != null)
                {
                    var local = this.provider.Connect(this.selectedCloud);
                    if (local.Response != null)
                    {
                        this.selectedCloud.Services.Synchronize(local.Response.Services, new ProvisionedServiceEqualityComparer());
                        this.selectedCloud.Applications.Synchronize(local.Response.Applications, new ApplicationEqualityComparer());
                        this.selectedCloud.AvailableServices.Synchronize(local.Response.AvailableServices, new SystemServiceEqualityComparer());
                    }
                    else
                    {
                        this.ErrorMessage = local.Message;
                    }
                }
                RaisePropertyChanged("SelectedCloud");
            }
        }
        
        public string Name
        {
            get { return this.name; }
            set { this.name = value; RaisePropertyChanged("Name"); }
        }

        public string Url
        {
            get { return this.url; }
            set { this.url = value; RaisePropertyChanged("Url"); }
        }

        public ObservableCollection<Cloud> Clouds
        {
            get { return provider.Clouds; }
        }

        public int[] MemoryLimits 
        { 
            get { return Constants.MemoryLimits; } 
        }      

        public int SelectedMemory
        {
            get { return this.selectedMemory; }
            set { this.selectedMemory = value; RaisePropertyChanged("SelectedMemory"); }
        }

        public ushort Instances
        {
            get { return this.instances; }
            set { this.instances = value; RaisePropertyChanged("Instances"); }
        }

        public ObservableCollection<ProvisionedService> ApplicationServices
        {
            get { return this.applicationServices; }
            set { this.applicationServices = value; RaisePropertyChanged("ApplicationServices"); }
        }
    }
}