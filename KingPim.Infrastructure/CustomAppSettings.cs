using System;
using System.Collections.Generic;
using System.Text;

namespace KingPim.Infrastructure
{
    public class CustomAppSettings
    {
        public CustomAppSettings()
        {
            // Ändra värdena i appsettings.json
            SendGridApiKey = "LoremIpsum";
        }

        public string SendGridApiKey { get; set; }
    }
}