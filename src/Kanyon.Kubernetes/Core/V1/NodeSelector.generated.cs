using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Kanyon.Kubernetes;

namespace Kanyon.Kubernetes.Core.V1
{
    public partial class NodeSelector
    {
        /** <summary>Required. A list of node selector terms. The terms are ORed.</summary> */
        public IEnumerable<Core.V1.NodeSelectorTerm> nodeSelectorTerms { get; set; }
    }
}