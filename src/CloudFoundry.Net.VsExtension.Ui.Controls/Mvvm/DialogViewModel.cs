﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using CloudFoundry.Net.VsExtension.Ui.Controls.Model;
using CloudFoundry.Net.VsExtension.Ui.Controls.Utilities;

namespace CloudFoundry.Net.VsExtension.Ui.Controls.Mvvm
{
    public abstract class DialogViewModel : ViewModelBase
    {
        public RelayCommand ConfirmedCommand { get; private set; }
        public RelayCommand CancelledCommand { get; private set; }
        protected event EventHandler OnConfirmed;
        protected event EventHandler OnCancelled;
        private string resultMessageId;
        protected CloudFoundryProvider provider;
        private string errorMessage;

        public DialogViewModel(string resultMessageId)
        {
            Messenger.Default.Send<NotificationMessageAction<CloudFoundryProvider>>(new NotificationMessageAction<CloudFoundryProvider>(Messages.GetCloudFoundryProvider, p => this.provider = p));
            this.resultMessageId = resultMessageId;
            this.ConfirmedCommand = new RelayCommand(Confirmed, CanExecuteConfirmed);
            this.CancelledCommand = new RelayCommand(Cancelled, CanExecuteCancelled);

            InitializeData();
            RegisterGetData();
        }

        protected virtual void InitializeData() { }
        protected virtual void RegisterGetData() { }

        private void Confirmed()
        {
            if (OnConfirmed != null)
                OnConfirmed(this, null);
            Messenger.Default.Send(new NotificationMessage<bool>(this, true, resultMessageId));            
        }

        protected virtual bool CanExecuteConfirmed()
        {
            return true;
        }

        private void Cancelled()
        {
            if (OnCancelled != null)
                OnCancelled(this, null);
            Messenger.Default.Send(new NotificationMessage<bool>(this, false, resultMessageId));
            Cleanup();
        }

        protected virtual bool CanExecuteCancelled()
        {
            return true;
        }

        public string ErrorMessage
        {
            get { return this.errorMessage; }
            set { this.errorMessage = value; RaisePropertyChanged("ErrorMessage"); }
        }
    }
}
