// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using static System.Environment;

namespace Microsoft.Build.Shared
{
    internal static partial class EnvironmentUtilities
    {
        public static bool Is64BitProcess => Marshal.SizeOf<IntPtr>() == 8;

        public static bool Is64BitOperatingSystem =>
#if FEATURE_64BIT_ENVIRONMENT_QUERY
            Environment.Is64BitOperatingSystem;
#else
            RuntimeInformation.OSArchitecture == Architecture.Arm64 ||
            RuntimeInformation.OSArchitecture == Architecture.X64;
#endif

        private static string UserProfile => Environment.GetEnvironmentVariable("UserProfile");
        private static string ApplicationData => Environment.GetEnvironmentVariable("AppData");
        private static string LocalApplicationData => Environment.GetEnvironmentVariable("LocalAppData");
        private static string ProgramData => Environment.GetEnvironmentVariable("ProgramData");
        private static string Public => Environment.GetEnvironmentVariable("Public");
        private static string CommonProgramFiles => Environment.GetEnvironmentVariable("CommonProgramFiles");
        private static string CommonProgramFilesX86 => Environment.GetEnvironmentVariable("CommonProgramFiles(x86)");
        private static string ProgramFiles => Environment.GetEnvironmentVariable("ProgramFiles");
        private static string ProgramFilesX86 => Environment.GetEnvironmentVariable("ProgramFiles(x86)");
        private static string Windows => Environment.GetEnvironmentVariable("SystemRoot");

        internal static string GetFolderPath(SpecialFolder folder) {
            var path = folder switch {

                // user folder 
                SpecialFolder.UserProfile =>        UserProfile,
                SpecialFolder.Desktop =>            Path.Combine(UserProfile, "/Desktop"),
                SpecialFolder.DesktopDirectory =>   Path.Combine(UserProfile, "/Desktop"),
                SpecialFolder.MyDocuments =>        Path.Combine(UserProfile, "/Documents"),
                SpecialFolder.Favorites =>          Path.Combine(UserProfile, "/Favorites"),
                SpecialFolder.MyMusic =>            Path.Combine(UserProfile, "/Music"),
                SpecialFolder.MyPictures =>         Path.Combine(UserProfile, "/Pictures"),
                SpecialFolder.MyVideos =>           Path.Combine(UserProfile, "/Videos"),

                // local profile
                SpecialFolder.LocalApplicationData =>   LocalApplicationData,
                SpecialFolder.InternetCache =>          Path.Combine(LocalApplicationData, "/Microsoft/Windows/INetCache"),
                SpecialFolder.Cookies =>                Path.Combine(LocalApplicationData, "/Microsoft/Windows/INetCookies"),
                SpecialFolder.History =>                Path.Combine(LocalApplicationData, "/Microsoft/Windows/History"),
                SpecialFolder.CDBurning =>              Path.Combine(LocalApplicationData, "/Microsoft/Windows/Burn/Burn"),

                // roaming profile
                SpecialFolder.ApplicationData =>    ApplicationData,
                SpecialFolder.StartMenu =>          Path.Combine(ApplicationData, "/Microsoft/Windows/Start Menu"),
                SpecialFolder.Programs =>           Path.Combine(ApplicationData, "/Microsoft/Windows/Start Menu/Programs"),
                SpecialFolder.Startup =>            Path.Combine(ApplicationData, "/Microsoft/Windows/Start Menu/Programs/Startup"),
                SpecialFolder.AdminTools =>         Path.Combine(ApplicationData, "/Microsoft/Windows/Start Menu/Programs/Administrative Tools"),
                SpecialFolder.Templates =>          Path.Combine(ApplicationData, "/Microsoft/Windows/Templates"),

                // roaming windows profile (items available to users but not guests)
                SpecialFolder.SendTo =>             Path.Combine(ApplicationData, "/Microsoft/Windows/SendTo"),
                SpecialFolder.Recent =>             Path.Combine(ApplicationData, "/Microsoft/Windows/Recent"),
                SpecialFolder.PrinterShortcuts =>   Path.Combine(ApplicationData, "/Microsoft/Windows/Printer Shortcuts"),
                SpecialFolder.NetworkShortcuts =>   Path.Combine(ApplicationData, "/Microsoft/Windows/Network Shortcuts"),

                // Common / ProgramData
                SpecialFolder.CommonApplicationData =>  ProgramData,
                SpecialFolder.CommonStartMenu =>        Path.Combine(ProgramData, "/Microsoft/Windows/Start Menu"),
                SpecialFolder.CommonPrograms =>         Path.Combine(ProgramData, "/Microsoft/Windows/Start Menu/Programs"),
                SpecialFolder.CommonStartup =>          Path.Combine(ProgramData, "/Microsoft/Windows/Start Menu/Programs/Startup"),
                SpecialFolder.CommonAdminTools =>       Path.Combine(ProgramData, "/Microsoft/Windows/Start Menu/Programs/Administrative Tools"),
                SpecialFolder.CommonTemplates =>        Path.Combine(ProgramData, "/Microsoft/Windows/Templates"),

                // Common / Public
                SpecialFolder.CommonDesktopDirectory => Path.Combine(Public, "/Desktop"),
                SpecialFolder.CommonDocuments =>        Path.Combine(Public, "/Documents"),
                SpecialFolder.CommonMusic =>            Path.Combine(Public, "/Music"),
                SpecialFolder.CommonPictures =>         Path.Combine(Public, "/Pictures"),
                SpecialFolder.CommonVideos =>           Path.Combine(Public, "/Videos"),

                SpecialFolder.Windows =>    Windows,
                SpecialFolder.Resources =>  Path.Combine(Windows, "/resources"),
                SpecialFolder.System =>     Path.Combine(Windows, "/System32"),
                SpecialFolder.SystemX86 =>  Path.Combine(Windows, "/System32"),
                SpecialFolder.Fonts =>      Path.Combine(Windows, "/Fonts"),

                SpecialFolder.ProgramFiles =>           ProgramFiles,
                SpecialFolder.ProgramFilesX86 =>        ProgramFilesX86,
                SpecialFolder.CommonProgramFiles =>     CommonProgramFiles,
                SpecialFolder.CommonProgramFilesX86 =>  CommonProgramFilesX86,

                // returns nothing in normal circumstances
                SpecialFolder.MyComputer => String.Empty,

                // returns nothing, or unknown (test on a specially configured machine)
                SpecialFolder.LocalizedResources => String.Empty,
                SpecialFolder.CommonOemLinks =>     String.Empty,

                // todo: should this preserve win32 behavior of returning null?
                _ => String.Empty
            };
            return path;
        }
    }
}
