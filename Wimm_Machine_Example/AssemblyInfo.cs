
using Wimm.Common;
using Wimm_Machine_Example;

//Wimmの読み込み対象になるクラスの指定です。
[assembly:LoadTarget(typeof(ExampleRobot))]

//Wimmに関連ファイルのデフォルト値を与えることができます。
//これらを指定しなくてもWimmはデフォルトで特定の内容を自動生成しますがその値を明示に与えたい場合は使用してください
//ファイルのプロパティ > ビルドアクション を 埋め込みリソース に設定する必要があります。
[assembly:BuiltInResource(ResourceType.Description,"Wimm_Machine_Example.Resources.description.txt")]
[assembly:BuiltInResource(ResourceType.ScriptOnControl, "Wimm_Machine_Example.Resources.on_control.lua")]

//アイコンは拡張子がpngでなければいけないので注意してください。
//[assembly: BuiltInResource(ResourceType.Icon, "Wimm_Machine_Example.Resources.icon.png")]
