﻿using System.Collections.Generic;
using System.IO;
using Odyssey.Logging;
using SharpDX.IO;

namespace Odyssey.Content
{
    public class FileSystemResourceResolver : IResourceResolver
    {
        public string RootDirectory { get; private set; }

        public FileSystemResourceResolver(string rootDirectory)
        {
            RootDirectory = rootDirectory;
        }

        public bool Exists(string resourceName)
        {
            return NativeFile.Exists(Path.Combine(RootDirectory, resourceName));
        }

        public Stream Resolve(string resourceName)
        {
            try {
                return new NativeFileStream(Path.Combine(RootDirectory, resourceName),
                    NativeFileMode.Open,
                    NativeFileAccess.Read);
            }
            catch (FileNotFoundException ex)
                {
                    LogEvent.Io.Error("Unable to find file {0}", ex.FileName);
                    LogEvent.Engine.TraceSource(
                        new TraceData(GetType(), "Resolve",
                            new KeyValuePair<string, string>("resourceName", resourceName)),
                            ex,
                            EventCode.CriticalFault);

                }
            return null;
        }
    }
}
