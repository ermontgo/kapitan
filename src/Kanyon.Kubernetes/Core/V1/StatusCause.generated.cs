using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Kanyon.Kubernetes;

namespace Kanyon.Kubernetes.Core.V1
{
    public partial class StatusCause
    {
        /** <summary>The field of the resource that has caused this error, as named by its JSON serialization. May include dot and postfix notation for nested attributes. Arrays are zero-indexed.  Fields may appear more than once in an array of causes due to fields having multiple errors. Optional.

Examples:
  "name" - the field "name" on the current resource
  "items[0].name" - the field "name" on the first array entry in "items"</summary> */
        public string field { get; set; }
        /** <summary>A human-readable description of the cause of the error.  This field may be presented as-is to a reader.</summary> */
        public string message { get; set; }
        /** <summary>A machine-readable description of the cause of the error. If this value is empty there is no information available.</summary> */
        public string reason { get; set; }
    }
}