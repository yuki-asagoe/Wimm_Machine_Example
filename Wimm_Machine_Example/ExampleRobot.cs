using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wimm.Common;
using Wimm.Common.Setting;
using Wimm.Machines;
using Wimm.Machines.TpipForRasberryPi;
using Wimm.Machines.Video;

namespace Wimm_Machine_Example
{
    //アセンブリにLoadTargetAttributeを適用する代わりに対象クラスに直接以下のようにLoadTargetAttributeを付与することもできます。
    //[LoadTarget]
    // ロボットを表現するクラスは Machineクラスを継承します
    // 今回は TpipForRasberryPiMachineクラスの継承によって間接的に継承しています
    internal class ExampleRobot : TpipForRasberryPiMachine
    {
        // Machine派生クラスは必ず引数(MachineConstrucotorArgs?)のコンストラクタを用意しなければなりません。
        // 引数のMachineContstructorArgsはロボットの初期化において扱う情報を格納しています。
        // この値がNullの場合はロボットの初期登録時などに情報を読み取る目的でインスタンス化する場合なので
        // 実際のロボットとの通信の初期化などは必要ありません
        public ExampleRobot(MachineConstructorArgs? args) : base(args)
        {
            // Wimmに通知を行うこともできます
            args?.Logger.Info("初期化を開始します");


            // カメラの設定です。Tpip4用にはTpip4Cameraクラスが用意されているため次のように初期化すればよいです。
            // Tpip4Cameraには第4カメラまで名前を指定でき、与えた名前の分だけ1番カメラから順に初期化されます。それ以上指定すると実行時例外が発生します。
            Camera = new Tpip4Camera(Hwnd,
                "第1カメラの名前",
                "第2カメラの名前"
            );


            // Wimmから受け取る設定情報の指定です
            // 必須ではありません
            // ConfigItemRegistryの可変長引数で与えます。それぞれに設定名の名前とデフォルト値を指定できます。
            // Tpipの接続先IPアドレス情報設定などもこれを使用しています。
            MachineConfig.AddRegistries(
                new ConfigItemRegistry("Control_Mode","Manual"),
                new ConfigItemRegistry("Safe_Mode","true")
            );
            // 以下のようにしてGetValueOrDefaultメソッドから設定情報を取り出せます
            // 指定した名前の設定が存在しない場合はnullを返します
            var safe_mode = MachineConfig.GetValueOrDefault("") ?? "true";


            // Wimm側のUIで表示する文字列情報を記入します
            // 必須ではありません
            // InformationNodeクラスの木構造のように表します。
            // 好きなように入れ子にできます
            
            Information = [
                new InformationNode("状態",[
                    new InformationNode("動作状態",[]),
                    new InformationNode("マイコン1接続",[]),
                    new InformationNode("マイコン2接続",[])
                ]),
                new InformationNode("物理情報",[
                    new InformationNode("座標",[
                        new InformationNode("X",[]),
                        new InformationNode("Y",[]),
                        new InformationNode("Z",[]),
                    ]),
                    new InformationNode("速度",[
                        new InformationNode("X",[]),
                        new InformationNode("Y",[]),
                        new InformationNode("Z",[]),
                    ])
                ])
            ];
            // 次のようにアクセスします
            // 好きな場所で値の取り出し、書き込みができ、書き込まれた情報はWimm上のUIから確認できます
            Information[0]!["動作状態"]!.Value = "問題なし";
            // 同じ意味です
            Information[0].Entries[0].Value = "問題なし";
            // 複数階層を下ることもできます
            Information[1]!["座標"]!["X"]!.Value = 10.ToString();


            // ロボットが持つ部品を登録します
            // ModuleGroupクラスの木構造のようになります。
            // 各ModuleGroupには対応する動作を表現する Module クラスのインスタンスを格納します。
            // 各ロボットごとにそのロボットの構造から考えられる自然なグルーピングを行います。
            // Moduleの作成については各クラスのコメントを参照してください
            StructuredModules = new ModuleGroup("modules",
                [
                    new ModuleGroup("wheels",
                        [/**/],
                        [
                            // このように作成した場合制御スクリプトからのアクセスは modules.wheels.right のようになります
                            new Modules.ExampleRobotStandardMotor("right","ロボット移動用右ホイール",0),
                            new Modules.ExampleRobotStandardMotor("left","ロボット移動用左ホイール",1),
                        ]
                    ),
                    new ModuleGroup("arms",
                        [
                            // ModuleGroupは好きなだけ入れ子にできます。
                            new ModuleGroup("hands",
                                [],
                                [/**/]
                            )
                        ],
                        [
                            new Modules.ExampleRobotStandardServoMotor("root","ロボットアーム根本サーボモーター",2,0,180)
                        ]
                    )
                ],
                [
                    // 必ずしもモーターのような構成単位である必要はなく必要ならばある程度大きな構成単位でも問題はありません。
                    // 構成単位が大きくなる例は複数のモーターが強調して動作する必要がある場合(=それぞれのモーターは個別には動かせない)などであり、
                    // 基本的にはその部品が目的とする動作を実現できる最小単位を作るようにするといいでしょう
                    new Modules.ItemContainer("container","ロボット内部アイテムコンテナ")
                ]
            );
        }


        // ロボットの名前
        public override string Name => "Example Robot";


        // Wimmに当たるロボットの制御システムの名前情報です。
        // (正直ユーザーに提示する以外になんにも使わないから適当でいいけど)
        // 今回はTpipForRasberryPiMachineが事前にオーバーライドしているのでここでは必要ありません。
        // public override string ControlSystem => base.ControlSystem;


        // Wimmに与えるロボットとの接続状態の情報を表すプロパティです。
        // 今回はTpipForRasberryPiMachineが事前にオーバーライドしているのでここでは必要ありません。
        // public override ConnectionState ConnectionStatus => base.ConnectionStatus;


        // Wimmのスクリプト処理(on_control.luaなど)の実施前に毎回呼び出されます。
        // スクリプト処理終了時にこのメソッドが返したControlProcessのDisposeメソッドが呼ばれます。
        // スクリプト処理の開始時と終了時にまとめて行う必要のある処理はここに記述します。
        protected override ControlProcess StartControlProcess() => new ExampleControlProcess();

        class ExampleControlProcess : ControlProcess
        {
            internal ExampleControlProcess()
            {
                Debug.WriteLine("スクリプト制御処理開始");
            }
            public override void Dispose()
            {
                Debug.WriteLine("スクリプト制御処理終了");
            }
        }

    }
}
