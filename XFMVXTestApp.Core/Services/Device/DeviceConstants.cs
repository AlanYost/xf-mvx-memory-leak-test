// *****************************************************************************
// <copyright file="DebugUtils.cs" company="Yambay Technologies Pty Ltd">
// Copyright (c) Yambay Technologies Pty Ltd
// </copyright>
// *****************************************************************************


namespace Yambay.mDrover.Core.Services.Device
{
    public class DeviceConstants
    {
        public enum MemoryObjectTypes
        {
            ViewModel,
            View,
            Model
        }

        public enum SortOrderTypes
        {
            Name = 0,
            FullName = 1,
            Active = 2
        }

        public enum RuleTypes
        {
            NameEquals = 0,
            FullNameEquals = 1,
            NameBegins = 2,
            FullNameBegins = 3,
            Contains = 4
        }
    }
}
