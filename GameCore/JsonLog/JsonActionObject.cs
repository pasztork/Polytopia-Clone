namespace JsonLog
{
    public class JsonActionObject
    {
        public string Name { get; set; } = "";
        public string Action { get; set; } = "";
        public JsonActionDatas ActionDatas { get; set; } = new JsonActionDatas();
    }
}
