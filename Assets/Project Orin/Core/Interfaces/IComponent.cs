namespace Project_Orin.Core.Interfaces
{
    public interface IComponent
    {
        long Id { get; }
        string Name { get; }
        void OnAttach(IWorldElement parent);
        void OnDetach(IWorldElement parent);
        void OnUpdate(float deltaTime);
    }
}
