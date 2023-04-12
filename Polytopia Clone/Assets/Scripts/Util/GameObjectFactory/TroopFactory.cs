using Model;

namespace Util
{
    public class TroopFactory : GameObjectFactory<TroopBase>
    {
        public TroopFactory() : base()
        {
            StringToFuncDictionary.Add("Archer", Factory.Create<Archer>);
            StringToFuncDictionary.Add("Boat", Factory.Create<Boat>);
            StringToFuncDictionary.Add("Builder", Factory.Create<Builder>);
            StringToFuncDictionary.Add("Catapult", Factory.Create<Catapult>);
            StringToFuncDictionary.Add("Scout", Factory.Create<Scout>);
            StringToFuncDictionary.Add("Settler", Factory.Create<Settler>);
            StringToFuncDictionary.Add("Warrior", Factory.Create<Warrior>);
        }
    }
}
