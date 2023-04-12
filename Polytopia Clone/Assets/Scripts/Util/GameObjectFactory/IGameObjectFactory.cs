namespace Util
{
    public interface IGameObjectFactory<TBaseClass>
    {
        public TBaseClass Instantiate(string typeName);
    }
}