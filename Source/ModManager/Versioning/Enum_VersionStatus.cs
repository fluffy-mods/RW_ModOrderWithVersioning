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
}