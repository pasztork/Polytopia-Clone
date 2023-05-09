using LogView.LogTransformer;

var lt = new LogTransformer
{
    LogFilePath = @"D:\2023-05-05_19-48-34.json",
    SettingsFilePath = "D:\\felev6\\onlab\\Polytopia-Clone\\Polytopia Clone\\GameSettings\\PropertiesSettings.json"
};
var json = lt.Transform();
File.WriteAllText("D:\\test.json", json);