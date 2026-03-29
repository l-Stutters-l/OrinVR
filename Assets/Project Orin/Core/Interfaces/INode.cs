public interface INode
{
    string Name { get; }

    string[] Inputs { get; }
    string[] Outputs { get; }

    UnityEngine.Color Color { get; }

    object[] Evaluate(object[] inputs);
}