using System;
using Microsoft.SPOT;
using System.Runtime.InteropServices;

namespace FusionWare.SPOT
{
    /// <summary>Class to contain version information</summary>
    /// <remarks>
    /// The version number assumes the normal for Major.Minor.Build.Revision
    /// </remarks>
    [Serializable, StructLayout( LayoutKind.Sequential ), Obsolete("Use System.Version instead")]
    public class VersionInfo
    {
        /// <summary>Version Major number</summary>
        public uint Major;
        /// <summary>Version Minor Number</summary>
        public uint Minor;
        /// <summary>Version Build Number</summary>
        public uint Build;
        /// <summary>Version Revision Number</summary>
        public uint Revision;

        /// <summary>Constructs an empty versionInfo (all fields 0)</summary>
        public VersionInfo( )
        {
        }

        /// <summary>Creates a new Version info that is a cloned copy of another</summary>
        /// <param name="rhs">"Right hand side" of the copy/assignment</param>
        public VersionInfo( VersionInfo rhs )
        {
            this.Major = rhs.Major;
            this.Minor = rhs.Minor;
            this.Build = rhs.Build;
            this.Revision = rhs.Revision;
        }

        /// <summary>Creates a new version info with the specified field values</summary>
        /// <param name="Major">Major version number</param>
        /// <param name="Minor">Minor Version number</param>
        /// <param name="Build">Build Number</param>
        /// <param name="Revision">Revision number</param>
        public VersionInfo( uint Major, uint Minor, uint Build, uint Revision )
        {
            this.Major = Major;
            this.Minor = Minor;
            this.Build = Build;
            this.Revision = Revision;
        }

        /// <summary>Compares two version info instances</summary>
        /// <param name="a">left hand side of comparison</param>
        /// <param name="b">right hand side of the comparison</param>
        /// <returns>true if a is less than b; false if b is greater than or equal to a</returns>
        /// <remarks>
        /// Provides operator overload for comparing two VersionInfo instances 
        /// </remarks>
        public static bool operator <( VersionInfo a, VersionInfo b )
        {
            if( a.Major < b.Major )
                return true;
            if( a.Major > b.Major )
                return false;

            // major values equal so test minor value
            if( a.Minor < b.Minor )
                return true;
            if( a.Minor > b.Minor )
                return false;

            // Minor number equal so test build
            if( a.Build < b.Build )
                return true;
            if( a.Build > b.Build )
                return false;

            // Build numbers equal so check revision
            if( a.Revision < b.Revision )
                return true;
            else
                return false;
        }


        /// <summary>Compares two version info instances</summary>
        /// <param name="a">left hand side of comparison</param>
        /// <param name="b">right hand side of the comparison</param>
        /// <returns>true if a is greater than b; false if b is less than or equal to a</returns>
        /// <remarks>
        /// Provides operator overload for comparing two VersionInfo instances 
        /// </remarks>
        public static bool operator >( VersionInfo a, VersionInfo b )
        {
            if( a.Major > b.Major )
                return true;
            if( a.Major < b.Major )
                return false;

            // major values equal so test minor value
            if( a.Minor > b.Minor )
                return true;
            if( a.Minor < b.Minor )
                return false;

            // Minor number equal so test build
            if( a.Build > b.Build )
                return true;
            if( a.Build < b.Build )
                return false;

            // Build numbers equal so check revision
            if( a.Revision > b.Revision )
                return true;
            else
                return false;
        }

        /// <summary>Converts the Version info to a string</summary>
        /// <returns>a string in the following format: V{Major}.{Minor}.{Build}.{Revision}</returns>
        public override string ToString( )
        {
            return "V" + this.Major + "." + this.Minor + "." + this.Build + "." + this.Revision;
        }
    }
}
