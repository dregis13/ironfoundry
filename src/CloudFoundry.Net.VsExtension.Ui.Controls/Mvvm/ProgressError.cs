﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudFoundry.Net.VsExtension.Ui.Controls.Mvvm
{
    public class ProgressError
    {
        public ProgressError(string text)
        {
            this.Text = text;
        }

        public string Text { get; private set; }
    }
}
