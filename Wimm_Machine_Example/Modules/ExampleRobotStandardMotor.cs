using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wimm.Common;
using Wimm.Machines.Component;

namespace Wimm_Machine_Example.Modules
{
    // モーターを表現する場合はMotorクラスの継承を推奨します
    // ただし必ずしも必須ではありません
    internal class ExampleRobotStandardMotor : Motor
    {
        int MotorID { get; }
        //引数nameには、実際の実装時のパーツの名前が来ます(モジュール自体の名前ではなくその用途によるもの、例えば`right_wheel`とか
        //descriptionも同様です
        //motorIDは必須ではありませんが付け足しました。 モーターを識別する情報がないと実際の処理時にどのモーターを制御するかがわからないでしょうから
        public ExampleRobotStandardMotor(string name, string description, int motorId) : base(name, description)
        {
            MotorID = motorId;

            // Featuresプロパティに格納されているFeatureクラスに基づいてそのモジュールの機能がWimmに公開されます。
            // Featureはそのモジュールがもつ能力・機能を表現するクラスです。
            // 例えば、そのモーターが停止することができるなら、`stop`という名前のFeatureを持つべきです
            Features = [
                // Motorクラスにおいては以下のRotationFatureから与えられるFeatureは自動的に追加されるため、
                // Featuresには何も指定せずとも最初から一つのFeatureが与えられます。
                // ただし必要ならば、このように記述してFeatureに要素を追加できます。
                ..Features,
                new Feature<Delegate>(
                    "stop",
                    "回転を停止させます",
                    ()=>{
                        // DoSomethingToStopThisMotor(MotorID);
                    }
                )
            ];
        }

        // このモーターモジュールのFeaturesに自動的に登録されるモーター回転機能です。
        public override Feature<Action<double>> RotationFeature => new Feature<Action<double>>(

            Motor.RotationFeatureName, // 名前が`rotate`になります

            // 機能の説明です。引数の規定などや動作などを説明します
            "引数 double speed: 値範囲 -1~1 ...",

            // 実際の回転処理
            // ここではラムダ式で書きましたがメソッド参照などでも構いません
            // この部分は実際にそのモジュールを動作させる処理を書きます。
            // この実装はモジュールごとにそれぞれ異なるでしょう
            // 注意点として内部でどんな事情があったとしても外部にはそれはわかりません
            // そのためFeatureはどこからどのように呼び出されるかの保証ができません
            // 可能な限り無茶な入力、呼び出しに対してもエラーを発生させないような処理を書いてください
            // 複数のモーターの情報をバッファして同時にまとめて送信したいという場合はControlProcessを活用するとよいでしょう
            (double speed) =>
            {
                // speed = Math.Clamp(speed, -1, 1);
                // var rotationData=CreateDataToTransferToRobot(MotorID,speed);
                // SendInformationToRobot(rotationData);
            }
        );

        // モジュール自体の名前
        // このクラスが処理する部品の形式などの名前です。
        // 実際にそれがロボット上のどこに使われるかという情報は含みません
        public override string ModuleName => "ロボット用標準モーター";

        // モジュール自体の説明
        public override string ModuleDescription => "ロボットに使用される標準用途のモーター";
    }
}
