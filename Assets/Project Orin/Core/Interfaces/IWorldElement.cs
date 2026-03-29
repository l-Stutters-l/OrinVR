using System.Collections.Generic;

namespace Project_Orin.Core.Interfaces
{
    public interface IWorldElement
    {
        string Name { get; set; }
        IWorldElement Parent { get; set; }
        List<IComponent> Components { get; }
    }
}
