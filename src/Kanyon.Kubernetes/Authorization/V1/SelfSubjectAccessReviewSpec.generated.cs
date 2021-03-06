using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Kanyon.Kubernetes;

namespace Kanyon.Kubernetes.Authorization.V1
{
    public partial class SelfSubjectAccessReviewSpec
    {
        /** <summary>NonResourceAttributes includes the authorization attributes available for non-resource requests to the Authorizer interface</summary> */
        public Authorization.V1.NonResourceAttributes nonResourceAttributes { get; set; }
        /** <summary>ResourceAttributes includes the authorization attributes available for resource requests to the Authorizer interface</summary> */
        public Authorization.V1.ResourceAttributes resourceAttributes { get; set; }
    }
}