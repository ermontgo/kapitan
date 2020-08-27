﻿using Humanizer;
using Kapitan.ResourceGenerator.Heuristics;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Kapitan.ResourceGenerator
{
    public class TypeMapper
    {
        private readonly Dictionary<string, ApiVersion> namespaceInfo;
        private readonly IManifestObjectHeuristic heuristic;
        private Dictionary<string, string> ReservedPropertyNames = new Dictionary<string, string>
        {
            { "namespace", "@namespace" },
            { "object", "@object" },
            { "default", "@default" },
            { "operator", "@operator" },
            { "continue", "@continue" }
        };

        public TypeMapper(Dictionary<string, ApiVersion> namespaceInfo, IManifestObjectHeuristic heuristic)
        {
            this.namespaceInfo = namespaceInfo;
            this.heuristic = heuristic;
        }

        public ApiTypeDefinition BuildTypeDefinition(string fullName, OpenApiSchema value)
        {
            var namespaceKey = StripPrefix(fullName);
            var typedef = new ApiTypeDefinition();
            typedef.IsManifestObject = heuristic.Detect(value, fullName);
            typedef.IsEnumObject = value.Enum.Any();

            if (namespaceInfo.ContainsKey(namespaceKey))
            {
                PopulateNamespaceInfo(namespaceKey, typedef);
            }
            else if (typedef.IsEnumObject && namespaceInfo.ContainsKey(StripPrefix(namespaceKey)))
            {
                PopulateNamespaceInfo(StripPrefix(namespaceKey), typedef);
            }

            typedef.TypeName = fullName.Replace(namespaceKey, string.Empty).Trim('.');            
            typedef.Schema = value;
            typedef.PropertyDefinitions = BuildPropertyDefinitions(value);
            typedef.EnumValues = BuildEnumValues(value);

            return typedef;
        }

        private IEnumerable<string> BuildEnumValues(OpenApiSchema value)
        {
            return value.Enum.Select(v => (v as OpenApiString)?.Value);
        }

        private void PopulateNamespaceInfo(string namespaceKey, ApiTypeDefinition typedef)
        {
            var groupVersion = namespaceInfo[namespaceKey];

            string group = GetNormalizedGroup(groupVersion);
            string version = GetNormalizedVersion(groupVersion);

            typedef.ApiVersion = groupVersion;
            typedef.Namespace = $".{GetDotNetNamespace(group, version)}";
            typedef.Directory = Path.Combine(group, version);
        }

        public IEnumerable<PropertyDefinition> BuildPropertyDefinitions(OpenApiSchema schema)
        {
            return schema.Properties.Select(p => new PropertyDefinition()
            {
                Name = GetProperPropertyName(p.Key),
                Description = p.Value.Description?.Replace("*", "&#42;"),
                Type = TranslateType(p.Value)
            });
        }

        private string GetProperPropertyName(string name)
        {
            if (ReservedPropertyNames.ContainsKey(name)) return ReservedPropertyNames[name];

            return name;
        }

        private string GetTypeNameFromId(string id)
        {
            var key = StripPrefix(id);
            var typeName = id.Replace(key, "").Trim('.');

            if (typeName == "JSONSchemaProps") return "Microsoft.OpenApi.Models.OpenApiSchema";
            if (namespaceInfo.ContainsKey(key))
            {
                var versionInfo = namespaceInfo[key];
                return $"{GetDotNetNamespace(GetNormalizedGroup(versionInfo), GetNormalizedVersion(versionInfo))}.{typeName}";
            }
            else return typeName;
        }

        private string TranslateType(OpenApiSchema value)
        {
            if (value.Reference?.Id != null) return GetTypeNameFromId(value.Reference.Id);
            switch (value.Type)
            {
                case "string": return "string";
                case "number": return "double";
                case "integer": return "int";
                case "boolean": return "bool";
                case "array": 
                    if (value.Items.Reference?.Id != null)
                    {
                        var id = value.Items.Reference.Id;
                        return $"IEnumerable<{GetTypeNameFromId(id)}>";
                    }
                    else
                    {
                        return $"IEnumerable<{TranslateType(value.Items)}>";
                    }
                default: return "object";
            }
        }

        private string GetDotNetNamespace(string group, string version) => $"{group}.{version}";

        private static string GetNormalizedVersion(ApiVersion gvp)
        {
            return PascalCase(gvp.Version);
        }

        private static string GetNormalizedGroup(ApiVersion gvp)
        {
            var root = gvp.Group.Split('.')[0];

            if (String.IsNullOrWhiteSpace(root)) root = "core";

            var ns = PascalCase(root);
            return ns;
        }

        private static string PascalCase(string root)
        {
            return root.Pascalize();
        }

        public static string StripPrefix(string s)
        {
            return s.Substring(0, s.LastIndexOf('.'));
        }
    }
}
