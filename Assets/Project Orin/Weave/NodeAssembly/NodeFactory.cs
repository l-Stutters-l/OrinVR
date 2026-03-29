// New: NodeFactory.cs
// Central place to create any node type by string name.
// Add new nodes here as you create them. No reflection needed for performance/simplicity.
using System.Collections.Generic;

public static class NodeFactory
{
    private static readonly Dictionary<string, System.Func<INode>> _creators = new()
    {
        { "AND", () => new AndNode() },
        { "OR", () => new OrNode()  },
        // Add more as you build them, e.g.:
        // { "OR", () => new OrNode() },
        // { "Add", () => new AddNode() },
    };

    public static INode Create(string nodeType)
    {
        if (_creators.TryGetValue(nodeType, out var creator))
            return creator();

        Debug.LogError($"Unknown node type: {nodeType}");
        return null;
    }

    // Helper for VR menus later
    public static string[] GetAvailableNodeTypes() => new List<string>(_creators.Keys).ToArray();
}