// *****************************************************************************
// <copyright file="MemoryTrackingRule.cs" company="Yambay Technologies Pty Ltd">
// Copyright (c) Yambay Technologies Pty Ltd
// </copyright>
// *****************************************************************************

using Yambay.mDrover.Core.Services.Device;

namespace XFMVXTestApp.Core.Services.Device
{
    public class MemoryTrackingRule
    {
        public DeviceConstants.RuleTypes RuleType { get; set; }

        public string RuleValue { get; set; }
    }
}
