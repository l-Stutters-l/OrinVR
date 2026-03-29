using UnityEngine;

public class OrNode : INode
{
    public string Name => "OR";

    public string[] Inputs => new[] { "A", "B" };
    public string[] Outputs => new[] { "Result" };

    public Color Color => Color.black;

    public object[] Evaluate(object[] inputs)
    {
        bool a = inputs.Length > 0 && inputs[0] != null ? System.Convert.ToBoolean(inputs[0]) : false;
        bool b = inputs.Length > 0 && inputs[0] != null ? System.Convert.ToBoolean(inputs[1]) : false;

        return new object[] { a || b };
    }
}