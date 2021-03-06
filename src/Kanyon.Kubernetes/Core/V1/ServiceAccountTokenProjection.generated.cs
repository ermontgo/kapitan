using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Kanyon.Kubernetes;

namespace Kanyon.Kubernetes.Core.V1
{
    public partial class ServiceAccountTokenProjection
    {
        /** <summary>Audience is the intended audience of the token. A recipient of a token must identify itself with an identifier specified in the audience of the token, and otherwise should reject the token. The audience defaults to the identifier of the apiserver.</summary> */
        public string audience { get; set; }
        /** <summary>ExpirationSeconds is the requested duration of validity of the service account token. As the token approaches expiration, the kubelet volume plugin will proactively rotate the service account token. The kubelet will start trying to rotate the token if the token is older than 80 percent of its time to live or if the token is older than 24 hours.Defaults to 1 hour and must be at least 10 minutes.</summary> */
        public int expirationSeconds { get; set; }
        /** <summary>Path is the path relative to the mount point of the file to project the token into.</summary> */
        public string path { get; set; }
    }
}