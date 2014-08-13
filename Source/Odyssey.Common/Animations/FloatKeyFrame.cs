﻿using System;
using System.Globalization;
using Odyssey.Graphics;
using Odyssey.Serialization;
using Odyssey.Utilities.Text;
using SharpDX;

namespace Odyssey.Animations
{
    public class FloatKeyFrame : KeyFrame<float>
    {
        protected override void OnReadXml(XmlDeserializationEventArgs e)
        {
            base.OnReadXml(e);
            string value = e.XmlReader.GetAttribute("Value");
            Value = float.Parse(value, CultureInfo.InvariantCulture);

            e.XmlReader.ReadStartElement();
        }
    }
}