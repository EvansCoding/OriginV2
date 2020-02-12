using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OriginV2.Web.ViewModels
{
    public class GenericMessageViewModel
    {
        public GenericMessageViewModel()
        {
            MessageType = GenericMessages.info;
        }

        public string Message { get; set; }
        public GenericMessages MessageType { get; set; }
        public bool ConstantMessage { get; set; }
    }

    public enum GenericMessages
    {
        warning,
        danger,
        success,
        info,
        error
    }
}