namespace Game.Scripts.Components
{
    public interface IGameEntity
    {
        string Name { get; set; }
        T Get<T>() where T : class;
        bool TryGet<T>(out T result) where T : class;
    }
}