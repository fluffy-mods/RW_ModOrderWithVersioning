using System;

namespace EdBFluffy.ModOrderWithVersionChecking
{
    public enum VersionStatus
    {
        Latest,
        Outdated,
        Fetching,
        NotImplemented,
        Error
    }

    public class VersionData
    {
        public string version = String.Empty;
        public string versionURL = String.Empty;
        public string date = String.Empty;
    }
}