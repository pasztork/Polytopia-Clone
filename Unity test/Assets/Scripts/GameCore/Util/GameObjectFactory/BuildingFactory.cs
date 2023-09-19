using Model;

namespace Util
{
    public class BuildingFactory : GameObjectFactory<BuildingBase>
    {
        public BuildingFactory() : base()
        {
            StringToFuncDictionary.Add("Bank", Factory.Create<Bank>);
            StringToFuncDictionary.Add("City", Factory.Create<City>);
            StringToFuncDictionary.Add("Farm", Factory.Create<Farm>);
            StringToFuncDictionary.Add("Harbor", Factory.Create<Harbor>);
            StringToFuncDictionary.Add("Supplier", Factory.Create<Supplier>);
        }
    }
}
