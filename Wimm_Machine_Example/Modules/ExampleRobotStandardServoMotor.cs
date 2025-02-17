using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wimm.Common;
using Wimm.Machines.Component;
using Wimm.Machines.TpipForRasberryPi.Import;

namespace Wimm_Machine_Example.Modules
{
    internal class ExampleRobotStandardServoMotor : ServoMotor
    {
        // Featureなどに関する解説はExampleRobotStandardMotor.csを参照
        int MotorID { get; }
        public ExampleRobotStandardServoMotor(string name, string description, int motorId, double minAngle, double maxAngle) : base(name, description, minAngle, maxAngle)
        {
            MotorID = motorId;
            // 本来、各モジュールは以下のようにFeatureのリストを与えなければなりませんが、
            // MotorクラスはRotationFeatureプロパティ、
            // ServoMotorクラスはRotationFeatureおよびSetAngleFeature(およびGetAngleFeature)プロパティから与えられるFeatureは自動的に登録されるため、
            // それ以上の機能が必要ないなら何もする必要はありません。
            /*
            Features = [
                // Featureのリスト
            ];
            */
        }

        public override Feature<Action<double, double>> SetAngleFeature => new Feature<Action<double, double>>(
            ServoMotor.SetAngleFeatureName,
            "引数 double angle, double speed : ...",
            (double angle,double speed) =>
            {
                // ServoMotorクラスはAngleプロパティを持ちます
                Angle = angle;
                // このプロパティに値を格納しておくとGetAngleFeatureとして自動登録されるFeatureを使用して値を取得できます
            }
        );

        public override Feature<Action<double>> RotationFeature => new Feature<Action<double>>(
            ServoMotor.RotationFeatureName,
            """
            Featureの解説が長文になるならヒアドキュメントとか生文字列リテラルつかってもいいと思います
            """,
            (double speed) =>
            {
                // 処理中にTpipとのやり取りが必要な場合、TPJT4名前空間にある構造体やメソッドが使用できます。
                // 一部名前が違いますが、おおむねTpip APIにあるものと同じです
                // 注意点として内部的にP/InvokeなのでやってることはTpip APIへのアクセスと同じです。
                // 引数の型やアンマネージドリソースの扱いなどに注意してください
                /*
                TPJT4.NativeMethods.Send_CANdata()
                */
            }
            
        );

        public override string ModuleName => "ロボット標準サーボモーター";

        public override string ModuleDescription => "ロボットで標準的に使用されるサーボモーター";
    }
}
