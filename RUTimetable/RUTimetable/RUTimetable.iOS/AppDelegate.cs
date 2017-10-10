﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Acr.UserDialogs;
using Realms;
using LocalNotifications.Plugin;
using RUTimetable;
using System.Threading.Tasks;
using TK.CustomMap.Api;
using TK.CustomMap.Api.Google;
using TK.CustomMap.Api.OSM;
using TK.CustomMap.Interfaces;
using TK.CustomMap.Overlays;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using TK.CustomMap;
using Xamarin;
using TK.CustomMap.iOSUnified;
using RUTimetableIOS.Helpers;

namespace RUTimetableIOS.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            FormsMaps.Init();
            TKCustomMapRenderer.InitMapRenderer();
            NativePlacesApi.Init();
            LoadApplication(new App(new ResourceHelper().Process()));
            return base.FinishedLaunching(app, options);
        }
    }
}
