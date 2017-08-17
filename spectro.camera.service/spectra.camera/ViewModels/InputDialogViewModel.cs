using System;
using System.Collections.Generic;
using System.Text;

namespace spectra.camera.ViewModels
{
    public class InputDialogViewModel
    {
        public string Input { get; set; }
        public string Header { get; set; }

        public InputDialogViewModel(string heading, string input)
        {
            Input = Input;
            Header = heading;
        }
    }
}
