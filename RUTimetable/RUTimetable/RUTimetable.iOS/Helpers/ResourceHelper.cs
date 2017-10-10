using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using RUTimetable;

namespace RUTimetableIOS.Helpers
{
    class ResourceHelper
    {
        public ResourceHelper()
        {
        }
        public GeoJSONData Process()
        {
            var content = "";
            var text = System.IO.File.OpenText("Files/RhodesMap.geojson");
            string temp = text.ReadLine();
            while (true)
            {
                if (string.IsNullOrEmpty(temp)) break;
                content += temp + "\n";
                temp = text.ReadLine();
            }
            return new GeoJSONData { Data = content };


        }
    }
}