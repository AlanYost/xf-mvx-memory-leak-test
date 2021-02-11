// *****************************************************************************
// <copyright file="MemoryTrackingService.cs" company="Yambay Technologies Pty Ltd">
// Copyright (c) Yambay Technologies Pty Ltd
// </copyright>
// *****************************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using XFMVXTestApp.Core.Services.Device;

namespace Yambay.mDrover.Core.Services.Device
{
    public class MemoryTrackingService : IMemoryTrackingService
    {

        private const int memoryAsMB = 1048576;

        private const string wildcard = "*";

        private static List<ObjectInMemory> TrackedObjects = new List<ObjectInMemory>(200);

        private readonly List<MemoryTrackingRule> TrackingRules = null;

        private readonly DeviceConstants.SortOrderTypes TrackingSortOrder;

        public bool IsTrackingOn { get; }

        public MemoryTrackingService(bool isTrackingOn, List<MemoryTrackingRule> rules, DeviceConstants.SortOrderTypes sortOrder)
        {
            IsTrackingOn = isTrackingOn;
            TrackingRules = rules;
            TrackingSortOrder = sortOrder;
        }

        public void Add(object trackedObj, DeviceConstants.MemoryObjectTypes objectType)
        {
            if (IsTrackingOn && IsToBeLogged(trackedObj))
            {
                TrackedObjects.Add(new ObjectInMemory(trackedObj, objectType));
            }
        }


        private bool IsToBeLogged(object trackedObj)
        {
            bool logIt = false;

            // Check if restricting what we are logging
            if (TrackingRules != null && TrackingRules.Count > 0)
            {
                foreach (var item in TrackingRules)
                {
                    if (item.RuleType == DeviceConstants.RuleTypes.NameEquals)
                    {
                        var name = trackedObj.GetType().Name;
                        if (item.RuleValue == name || item.RuleValue == wildcard)
                        {
                            logIt = true;
                            break;
                        }
                    }
                    else if (item.RuleType == DeviceConstants.RuleTypes.FullNameEquals)
                    {
                        var name = trackedObj.GetType().FullName;
                        if (item.RuleValue == name || item.RuleValue == wildcard)
                        {
                            logIt = true;
                            break;
                        }
                    }
                    else if (item.RuleType == DeviceConstants.RuleTypes.NameBegins)
                    {
                        var name = trackedObj.GetType().Name;
                        if (name.StartsWith(item.RuleValue) || item.RuleValue == wildcard)
                        {
                            logIt = true;
                            break;
                        }
                    }
                    else if (item.RuleType == DeviceConstants.RuleTypes.FullNameBegins)
                    {
                        var name = trackedObj.GetType().FullName;
                        if (name.StartsWith(item.RuleValue) || item.RuleValue == wildcard)
                        {
                            logIt = true;
                            break;
                        }
                    }
                    else if (item.RuleType == DeviceConstants.RuleTypes.Contains)
                    {
                        var name = trackedObj.GetType().FullName;
                        if (name.Contains(item.RuleValue) || item.RuleValue == wildcard)
                        {
                            logIt = true;
                            break;
                        }
                    }
                }
                return logIt;
            }
            // If no checks then log it
            return true;
        }

        public string LogDebugInfo()
        {
            var debugInfo = new StringBuilder();

            debugInfo.AppendLine();
            debugInfo.AppendLine(GetMemoryUsage("before"));

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var debugList = new List<ObjectSummary>(TrackedObjects.Distinct().Count());
            ObjectSummary lastObjectSummary = null;

            foreach (var item in TrackedObjects.OrderBy(r => r.FullName))
            {
                if (lastObjectSummary == null || lastObjectSummary.FullName != item.FullName)
                {
                    var newItem = new ObjectSummary(item.Name, item.FullName, item.ObjectType, item.WeakRef.IsAlive);
                    debugList.Add(newItem);
                    lastObjectSummary = newItem;
                }
                else
                {
                    lastObjectSummary.UpdateCounts(item.WeakRef.IsAlive);
                }
            }

            List<ObjectSummary> objectsInMemoryOrdered = null;

            if (DeviceConstants.SortOrderTypes.Active == TrackingSortOrder)
            {
                objectsInMemoryOrdered = debugList.OrderByDescending(r => r.Active).ToList();
            }
            else if (DeviceConstants.SortOrderTypes.FullName == TrackingSortOrder)
            {
                objectsInMemoryOrdered = debugList.OrderBy(r => r.FullName).ToList();
            }
            else
            {
                objectsInMemoryOrdered = debugList.OrderBy(r => r.Name).ToList();
            }

            debugInfo.AppendLine();
            debugInfo.AppendLine("------------------------------------------------------------");
            debugInfo.AppendLine("Active | Dead | Name                           ");
            debugInfo.AppendLine("------------------------------------------------------------");
            foreach (var item in objectsInMemoryOrdered)
            {
                debugInfo.AppendLine(string.Format("{0,6} | {1,4} | {2,-30}", item.Active, item.Dead, item.Name));
            }

            debugInfo.AppendLine("------------------------------------------------------------");
            debugInfo.AppendLine(GetMemoryUsage("after"));

            Log(debugInfo.ToString());

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Debug.WriteLine(debugInfo.ToString());
            }

            return debugInfo.ToString();
        }

        private string GetMemoryUsage(string state)
        {

            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                return $"---->>> Memory usage {state} GC: {((ulong)Process.GetCurrentProcess().PrivateMemorySize64) / memoryAsMB} MB";
            }

            return $"---->>> Memory usage {state} GC: {((ulong)GC.GetTotalMemory(false)) / memoryAsMB} MB";

        }

        public void Log(string data)
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Debug.WriteLine($"{documents}");
            var filename = Path.Combine(documents, "memuse.log");
            File.AppendAllText(filename, data.ToString());
        }

        private class ObjectInMemory
        {

            public WeakReference WeakRef { get; }

            public string Name { get; }

            public string FullName { get; }

            public DeviceConstants.MemoryObjectTypes ObjectType { get; }

            public ObjectInMemory(object obj, DeviceConstants.MemoryObjectTypes objectType)
            {
                WeakRef = new WeakReference(obj);
                Name = obj.GetType().Name;
                FullName = obj.GetType().FullName;
                ObjectType = objectType;
            }

        }

        private class ObjectSummary
        {

            public string Name { get; }

            public string FullName { get; }

            public DeviceConstants.MemoryObjectTypes ObjectType { get; }

            public int Active { get; set; }

            public int Dead { get; set; }

            public void UpdateCounts(bool addToActive)
            {
                if (addToActive)
                {
                    Active++;
                }
                else
                {
                    Dead++;
                }
            }

            public ObjectSummary(string name, string fullName, DeviceConstants.MemoryObjectTypes objectType, bool addToCount)
            {
                Name = name;
                FullName = fullName;
                ObjectType = objectType;
                UpdateCounts(addToCount);
            }

        }
    }
}
