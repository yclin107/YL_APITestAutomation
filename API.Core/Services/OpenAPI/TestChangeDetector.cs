using API.Core.Models;
using Microsoft.OpenApi.Models;

namespace API.Core.Services.OpenAPI
{
    public class TestChangeDetector
    {
        public static List<TestGenerationChange> DetectChanges(OpenApiTestSpec oldSpec, OpenApiTestSpec newSpec)
        {
            var changes = new List<TestGenerationChange>();

            // Detect removed endpoints
            foreach (var oldEndpoint in oldSpec.EndpointTests)
            {
                if (!newSpec.EndpointTests.ContainsKey(oldEndpoint.Key))
                {
                    changes.Add(new TestGenerationChange
                    {
                        Type = ChangeType.Removed,
                        Path = oldEndpoint.Value.Path,
                        Method = oldEndpoint.Value.Method,
                        Description = $"Endpoint {oldEndpoint.Value.Method} {oldEndpoint.Value.Path} was removed"
                    });
                }
            }

            // Detect added endpoints
            foreach (var newEndpoint in newSpec.EndpointTests)
            {
                if (!oldSpec.EndpointTests.ContainsKey(newEndpoint.Key))
                {
                    changes.Add(new TestGenerationChange
                    {
                        Type = ChangeType.Added,
                        Path = newEndpoint.Value.Path,
                        Method = newEndpoint.Value.Method,
                        Description = $"New endpoint {newEndpoint.Value.Method} {newEndpoint.Value.Path} was added"
                    });
                }
            }

            // Detect modified endpoints
            foreach (var newEndpoint in newSpec.EndpointTests)
            {
                if (oldSpec.EndpointTests.TryGetValue(newEndpoint.Key, out var oldEndpoint))
                {
                    var endpointChanges = DetectEndpointChanges(oldEndpoint, newEndpoint.Value);
                    changes.AddRange(endpointChanges);
                }
            }

            return changes;
        }

        private static List<TestGenerationChange> DetectEndpointChanges(OpenApiEndpointTest oldEndpoint, OpenApiEndpointTest newEndpoint)
        {
            var changes = new List<TestGenerationChange>();

            // Check summary/description changes
            if (oldEndpoint.Summary != newEndpoint.Summary)
            {
                changes.Add(new TestGenerationChange
                {
                    Type = ChangeType.Modified,
                    Path = newEndpoint.Path,
                    Method = newEndpoint.Method,
                    Description = "Summary changed",
                    OldValue = oldEndpoint.Summary,
                    NewValue = newEndpoint.Summary
                });
            }

            // Check parameter changes
            var oldParamNames = oldEndpoint.Parameters.Select(p => p.Name).ToHashSet();
            var newParamNames = newEndpoint.Parameters.Select(p => p.Name).ToHashSet();

            foreach (var removedParam in oldParamNames.Except(newParamNames))
            {
                changes.Add(new TestGenerationChange
                {
                    Type = ChangeType.ParameterRemoved,
                    Path = newEndpoint.Path,
                    Method = newEndpoint.Method,
                    Description = $"Parameter '{removedParam}' was removed"
                });
            }

            foreach (var addedParam in newParamNames.Except(oldParamNames))
            {
                changes.Add(new TestGenerationChange
                {
                    Type = ChangeType.ParameterAdded,
                    Path = newEndpoint.Path,
                    Method = newEndpoint.Method,
                    Description = $"Parameter '{addedParam}' was added"
                });
            }

            // Check response changes
            var oldResponseCodes = oldEndpoint.Responses.Keys.ToHashSet();
            var newResponseCodes = newEndpoint.Responses.Keys.ToHashSet();

            foreach (var removedResponse in oldResponseCodes.Except(newResponseCodes))
            {
                changes.Add(new TestGenerationChange
                {
                    Type = ChangeType.ResponseRemoved,
                    Path = newEndpoint.Path,
                    Method = newEndpoint.Method,
                    Description = $"Response code '{removedResponse}' was removed"
                });
            }

            foreach (var addedResponse in newResponseCodes.Except(oldResponseCodes))
            {
                changes.Add(new TestGenerationChange
                {
                    Type = ChangeType.ResponseAdded,
                    Path = newEndpoint.Path,
                    Method = newEndpoint.Method,
                    Description = $"Response code '{addedResponse}' was added"
                });
            }

            return changes;
        }
    }
}