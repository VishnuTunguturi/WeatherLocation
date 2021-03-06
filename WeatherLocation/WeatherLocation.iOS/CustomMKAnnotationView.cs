﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MapKit;
using UIKit;

namespace WeatherLocation.iOS
{
    public class CustomMKAnnotationView : MKAnnotationView
    {
        public string Id { get; set; }

        public CustomMKAnnotationView(IMKAnnotation annotation, string id)
            : base(annotation, id)
        {
        }
    }
}