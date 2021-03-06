using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Kanyon.Kubernetes;

namespace Kanyon.Kubernetes.Core.V1
{
    public partial class NodeDaemonEndpoints
    {
        /** <summary>DaemonEndpoint contains information about a single Daemon endpoint.</summary> */
        public Core.V1.DaemonEndpoint kubeletEndpoint { get; set; }
    }
}