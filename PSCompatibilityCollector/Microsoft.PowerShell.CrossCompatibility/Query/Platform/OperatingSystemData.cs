// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using OperatingSystemDataMut = Microsoft.PowerShell.CrossCompatibility.Data.OperatingSystemData;

namespace Microsoft.PowerShell.CrossCompatibility.Query
{
    /// <summary>
    /// Readonly query object for platform operating system information.
    /// </summary>
    public class OperatingSystemData
    {
        private readonly OperatingSystemDataMut _operatingSystemData;

        /// <summary>
        /// Create an operating system query object from operating system information.
        /// </summary>
        /// <param name="operatingSystemData">Operating system data object from a profile.</param>
        public OperatingSystemData(OperatingSystemDataMut operatingSystemData)
        {
            _operatingSystemData = operatingSystemData;
        }

        /// <summary>
        /// The name of the operating system as reported by $PSVersionTable.OS.
        /// </summary>
        public string Name => _operatingSystemData.Name;

        /// <summary>
        /// The name of the platform as reported by $PSVersionTable.Platform.
        /// </summary>
        public string Platform => _operatingSystemData.Platform;

        /// <summary>
        /// The OS machine architecture, from System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.
        /// </summary>
        public Architecture Architecture => _operatingSystemData.Architecture;

        /// <summary>
        /// Specifies whether the OS is Windows, Linux or macOS.
        /// </summary>
        public OSFamily Family => _operatingSystemData.Family;

        /// <summary>
        /// The self declared version of the operating system (kernel on Linux).
        /// </summary>
        public string Version => _operatingSystemData.Version;

        /// <summary>
        /// The Windows Service Pack of the OS, if any.
        /// </summary>
        public string ServicePack => _operatingSystemData.ServicePack;

        /// <summary>
        /// The Windows SKU ID of the OS, if any.
        /// </summary>
        public uint? SkuId => _operatingSystemData.SkuId;

        /// <summary>
        /// The Linux distribution ID of the OS, if any.
        /// </summary>
        public string DistributionId => _operatingSystemData.DistributionId;

        /// <summary>
        /// The version of the Linux distribution, if any.
        /// </summary>
        public string DistirbutionVersion => _operatingSystemData.DistributionVersion;

        /// <summary>
        /// The self-reported "pretty name" of the Linux distribution, if any.
        /// </summary>
        public string DistributionPrettyName => _operatingSystemData.DistributionPrettyName;

        /// <summary>
        /// The human-readable name of this operating system
        /// </summary>
        public string FriendlyName => Family == OSFamily.Linux ? DistributionPrettyName : Name;

        /// <summary>
        /// A descriptive enum form of the Windows SKU, if one is available.
        /// </summary>
        public WindowsSku? Sku
        {
            get
            {
                // Type inference fails on a ternary, so we are forced to write this out...
                if (SkuId.HasValue) { return (WindowsSku)SkuId.Value; }
                return null;
            }
        }
    }
}
