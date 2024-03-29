﻿using System;
using System.IO;
using DeviceId;

namespace SecureLocalStorage
{
    public class CustomLocalStorageConfig : ISecureLocalStorageConfig
    {
        public CustomLocalStorageConfig(string defaultPath, string applicationName)
        {
            DefaultPath = defaultPath ?? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            ApplicationName = ApplicationName;
            StoragePath = Path.Combine(DefaultPath, applicationName);
        }

        public CustomLocalStorageConfig(string defaultPath, string applicationName, string key)
        {
            DefaultPath = defaultPath ?? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            ApplicationName = ApplicationName;
            StoragePath = Path.Combine(DefaultPath, applicationName);
            BuildLocalSecureKey = () => key;
        }

        public CustomLocalStorageConfig WithDefaultKeyBuilder()
        {
            BuildLocalSecureKey = () => new DeviceIdBuilder()
                .AddMachineName()
                .AddProcessorId()
                .AddMotherboardSerialNumber()
                .AddSystemDriveSerialNumber()
                .ToString();

            return this;
        }

        public string DefaultPath { get; }
        public string ApplicationName { get; }
        public string StoragePath { get; }
        public Func<string> BuildLocalSecureKey { get; set; }
    }
}
