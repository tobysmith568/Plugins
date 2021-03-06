﻿using AnyStatus.API;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnyStatus
{
    [DisplayName("Performance Counter")]
    [DisplayColumn("System Information")]
    [Description("Shows the value of a performance counter.")]
    public class PerformanceCounter : Sparkline, ISchedulable
    {
        private const string Category = "Performance Counter";

        public PerformanceCounter()
        {
            Interval = 30;
            Units = IntervalUnits.Seconds;
        }

        [DisplayName("Machine Name")]
        [Category(Category)]
        [Description("Optional. Leave blank for local computer.")]
        public string MachineName { get; set; }

        [Required]
        [DisplayName("Category")]
        [Category(Category)]
        //[Description("")]
        public string CategoryName { get; set; }

        [Required]
        [DisplayName("Counter")]
        [Category(Category)]
        //[Description("")]
        public string CounterName { get; set; }

        [DisplayName("Instance")]
        [Category(Category)]
        //[Description("")]
        public string InstanceName { get; set; }
    }
}