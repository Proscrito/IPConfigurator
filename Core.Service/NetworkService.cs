﻿using System;
using System.Diagnostics;
using Microsoft.Owin.Hosting;

namespace Core.Service
{
    public class NetworkService : IDisposable
    {
        private const string BaseAddress = "http://localhost:12345/";

        private readonly IDisposable _selfHost;
        private bool _disposed;

        public NetworkService()
        {
            _selfHost = WebApp.Start<Startup>(BaseAddress);
            Trace.WriteLine("Self hosting started");
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _selfHost.Dispose();
                _disposed = true;
            }
        }
    }
}
