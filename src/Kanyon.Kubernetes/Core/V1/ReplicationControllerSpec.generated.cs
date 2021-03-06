using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Kanyon.Kubernetes;

namespace Kanyon.Kubernetes.Core.V1
{
    public partial class ReplicationControllerSpec
    {
        /** <summary>Minimum number of seconds for which a newly created pod should be ready without any of its container crashing, for it to be considered available. Defaults to 0 (pod will be considered available as soon as it is ready)</summary> */
        public int minReadySeconds { get; set; }
        /** <summary>Replicas is the number of desired replicas. This is a pointer to distinguish between explicit zero and unspecified. Defaults to 1. More info: https://kubernetes.io/docs/concepts/workloads/controllers/replicationcontroller#what-is-a-replicationcontroller</summary> */
        public int replicas { get; set; }
        /** <summary>Selector is a label query over pods that should match the Replicas count. If Selector is empty, it is defaulted to the labels present on the Pod template. Label keys and values that must match in order to be controlled by this replication controller, if empty defaulted to labels on Pod template. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/labels/#label-selectors</summary> */
        public object selector { get; set; }
        /** <summary>PodTemplateSpec describes the data a pod should have when created from a template</summary> */
        public Core.V1.PodTemplateSpec template { get; set; }
    }
}