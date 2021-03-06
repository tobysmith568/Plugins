﻿using AnyStatus.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AnyStatus.Plugins.Tests.Widgets
{
    [TestClass]
    public class SystemInformationTests
    {
        [TestMethod]
        public async Task CpuUsageTest()
        {
            var widget = new CpuUsage();

            var request = MetricQueryRequest.Create(widget);

            var handler = new CpuUsageQuery();

            await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.AreEqual(State.Ok, widget.State);
        }

        [TestMethod]
        public async Task ProcessCpuUsageTest()
        {
            var widget = new ProcessCpuUsage
            {
                ProcessName = "System",
            };

            var request = MetricQueryRequest.Create(widget);

            var handler = new ProcessCpuUsageQuery();

            await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.AreEqual(State.Ok, widget.State);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task ProcessCpuUsage_ProcessNotFound_Test()
        {
            var widget = new ProcessCpuUsage
            {
                ProcessName = "DoesNotExist"
            };

            var request = MetricQueryRequest.Create(widget);

            var handler = new ProcessCpuUsageQuery();

            await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task RamUsageTest()
        {
            var widget = new RamUsage();

            var request = MetricQueryRequest.Create(widget);

            var handler = new RamUsageQuery();

            await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.AreEqual(State.Ok, widget.State);

            Assert.IsTrue(widget.Value > 0);
        }

        [TestMethod]
        public async Task ProcessCountTest()
        {
            var widget = new ProcessCount();

            var request = MetricQueryRequest.Create(widget);

            var handler = new ProcessCountQuery();

            await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.AreEqual(State.Ok, widget.State);

            Assert.IsTrue(widget.Value > 0);
        }

        [TestMethod]
        public async Task ThreadCountTest()
        {
            var widget = new ThreadCount();

            var request = MetricQueryRequest.Create(widget);

            var handler = new ThreadCountQuery();

            await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.AreEqual(State.Ok, widget.State);

            Assert.IsTrue(widget.Value > 0);
        }

        [TestMethod]
        public async Task PerformanceCounterTest()
        {
            var widget = new PerformanceCounter
            {
                CategoryName = "Memory",
                CounterName = "Available MBytes",
            };

            var request = MetricQueryRequest.Create(widget);

            var handler = new PerformanceCounterQuery();

            await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.AreEqual(State.Ok, widget.State);

            Assert.IsTrue(widget.Value > 0);
        }

        [TestMethod]
        public void FileExistsTest()
        {
            var widget = new FileExists
            {
                Path = Assembly.GetExecutingAssembly().Location,
            };

            var request = HealthCheckRequest.Create(widget);

            _ = new FileExistsCheck().Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.AreEqual(State.Ok, widget.State);
        }

        [TestMethod]
        public void FileNotExistsTest()
        {
            var widget = new FileExists
            {
                Path = "",
            };

            var request = HealthCheckRequest.Create(widget);

            _ = new FileExistsCheck().Handle(request, CancellationToken.None).ConfigureAwait(false);

            Assert.AreEqual(State.Failed, widget.State);
        }
    }
}
