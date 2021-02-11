// *****************************************************************************
// <copyright file="IMemoryTrackingService.cs" company="Yambay Technologies Pty Ltd">
// Copyright (c) Yambay Technologies Pty Ltd
// </copyright>
// *****************************************************************************

namespace Yambay.mDrover.Core.Services.Device
{
    public interface IMemoryTrackingService
    {
        void Add(object trackedObj, DeviceConstants.MemoryObjectTypes objectType);
        string LogDebugInfo();
        bool IsTrackingOn { get; }
        void Log(string data);
    }
}