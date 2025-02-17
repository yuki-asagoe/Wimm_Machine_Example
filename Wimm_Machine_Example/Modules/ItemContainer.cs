using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wimm.Common;

namespace Wimm_Machine_Example.Modules
{
    // モジュールは既存のクラスを派生させる必要は必ずしもなくModuleクラスの継承によって自由に作成できます。
    // こっちの方が余計なことしないからかえってわかりやすいかもしれません
    // Featureなどに関する解説はExampleRobotStandardMotor.csを参照
    internal class ItemContainer : Module
    {
        bool IsOpen { get; set; }
        public ItemContainer(string name, string description) : base(name, description)
        {
            // 既存クラスを使用しない場合Featureの自動登録などはなされないので以下のように明示的にその機能を公開する必要があります
            Features = [
                new Feature<Delegate>(
                    "open",
                    "コンテナを開放します",
                    ()=>{
                        // DoSomethingToOpenContainer()
                        IsOpen=true;
                    }
                ),
                new Feature<Delegate>(
                    "close",
                    "コンテナを閉じます",
                    this.CloseContainer // メソッド参照でも可能です
                ),
                new Feature<Delegate>(
                    "get_contained_item_count",
                    "コンテナ内部のアイテム数を返します",
                    ()=>{
                        return 1; // 値を返すFeatureを作成することもできます
                    }
                ),
                new Feature<Delegate>(
                    "put_item_out",
                    "引数 int count : countの数だけアイテムを輩出します",
                    (int count)=>{
                        // 引数を受け取るFeatureも作成できます。
                    }
                )
            ];
        }

        public override string ModuleName => "ロボット内部コンテナ";

        public override string ModuleDescription => "ロボット内部にある物資格納用コンテナ";

        private void CloseContainer()
        {
            IsOpen = false;
        }

    }
}
