namespace JsonLog
{
    public class JsonActionObject
    {
        public string Name { get; set; } = "";
        public string Action { get; set; } = "";
        public JsonActionParameters ActionDatas { get; set; } = new();
    }
}
