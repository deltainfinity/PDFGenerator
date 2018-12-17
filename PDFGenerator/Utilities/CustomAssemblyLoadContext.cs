using System;
using System.Reflection;
using System.Runtime.Loader;

namespace PDFGenerator.Utilities
{
    /// <summary>
    /// Load custom assembly at run time
    /// </summary>
    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        /// <summary>
        /// Load an unmanaged library from an absolute path
        /// </summary>
        /// <param name="absolutePath">The absolute path where the assembly is located</param>
        /// <returns>A pointer to the unmanaged library</returns>
        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDll(absolutePath);
        }

        ///<inheritdoc />
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            return LoadUnmanagedDllFromPath(unmanagedDllName);
        }

        /// <summary>
        /// This method is not implemented as this class is intended solely for loading unmanaged libraries.
        /// Please use the standard AssemblyLoadContext class to load managed libraries.
        /// </summary>
        /// <param name="assemblyName">Not implemented</param>
        /// <returns>NotImplementedException</returns>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            throw new System.NotImplementedException();
        }
    }
}
