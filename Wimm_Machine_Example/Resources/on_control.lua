-- on_control.lua ファイル内ではロボットに対する定期制御処理を行います
-- 一応コード上では最高で毎秒20回呼び出される可能性があります。
-- このファイル内においてはそれぞれ以下の値が使用できます
-- {Root Module Name} - StructuredModlues 今回はExampleRobot.StructuredModluesの根になるModuleGroupの名前がmodulesなのでその名前で参照します
-- buttons : Votice.XInput.GamepadButtons - https://github.com/amerkoleci/Vortice.Windows/blob/main/src/Vortice.XInput/GamepadButtons.cs
-- gamepad : Vortice.Xinput.Gamepad - https://github.com/amerkoleci/Vortice.Windows/blob/main/src/Vortice.XInput/Gamepad.cs
-- input : Wimm.Model.Control.Script.InputSupporter
-- wimm : Wimm.Model.Control.Script.WimmFeatureProvider

-- 例えばコントローラーのスティックが前に倒されているときにロボットを前進させたいなら以下のようになるでしょう

if input.IsLeftThumbUp(gamepad) then
	modules.wheels.left.rotate(1)
	modules.wheels.right.rotate(1)
else
	modules.wheels.left.rotate(0)
	modules.wheels.right.rotate(0)
end

-- ここでそれぞれmodules/wheelsはそれぞれExampleRobot.StructuredModluesで与えておいたModuleGroupの名前です
-- StructuredModluesで与えた木構造の通りに . 区切りでたどれば呼び出せます。
-- そこからさらに . 区切りであらかじめ設定しておいたFeatureを関数として呼び出すことができます。
-- 例えば以下のように

if input.IsButtonDown(gamepad,buttons.RightShoulder) then
	modules.container.open()
end
if input.IsButtonUp(Gamepad,buttons.LeftShoudlder) then
	modules.container.close()
end

-- アクセスできる要素のリストはwimmに読み込ませたときに自動生成する docs/Reference.htmlでも確認できます。
-- 単純にあるボタンが押されたときになにか関数を呼び出すだけでよいならcontrol_map.luaを利用することもできます。