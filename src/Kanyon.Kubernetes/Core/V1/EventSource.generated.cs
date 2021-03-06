using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Kanyon.Kubernetes;

namespace Kanyon.Kubernetes.Core.V1
{
    public partial class EventSource
    {
        /** <summary>Component from which the event is generated.</summary> */
        public string component { get; set; }
        /** <summary>Node name on which the event is generated.</summary> */
        public string host { get; set; }
    }
}