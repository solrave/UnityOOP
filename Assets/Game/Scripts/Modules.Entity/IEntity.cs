namespace Game.Gameplay
{
    public interface IEntity
    {
        string Name { get; set; }
        T Get<T>() where T : class;
        bool TryGet<T>(out T result) where T : class;
    }
}