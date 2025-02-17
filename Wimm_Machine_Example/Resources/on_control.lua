-- on_control.lua �t�@�C�����ł̓��{�b�g�ɑ΂��������䏈�����s���܂�
-- �ꉞ�R�[�h��ł͍ō��Ŗ��b20��Ăяo�����\��������܂��B
-- ���̃t�@�C�����ɂ����Ă͂��ꂼ��ȉ��̒l���g�p�ł��܂�
-- {Root Module Name} - StructuredModlues �����ExampleRobot.StructuredModlues�̍��ɂȂ�ModuleGroup�̖��O��modules�Ȃ̂ł��̖��O�ŎQ�Ƃ��܂�
-- buttons : Votice.XInput.GamepadButtons - https://github.com/amerkoleci/Vortice.Windows/blob/main/src/Vortice.XInput/GamepadButtons.cs
-- gamepad : Vortice.Xinput.Gamepad - https://github.com/amerkoleci/Vortice.Windows/blob/main/src/Vortice.XInput/Gamepad.cs
-- input : Wimm.Model.Control.Script.InputSupporter
-- wimm : Wimm.Model.Control.Script.WimmFeatureProvider

-- �Ⴆ�΃R���g���[���[�̃X�e�B�b�N���O�ɓ|����Ă���Ƃ��Ƀ��{�b�g��O�i���������Ȃ�ȉ��̂悤�ɂȂ�ł��傤

if input.IsLeftThumbUp(gamepad) then
	modules.wheels.left.rotate(1)
	modules.wheels.right.rotate(1)
else
	modules.wheels.left.rotate(0)
	modules.wheels.right.rotate(0)
end

-- �����ł��ꂼ��modules/wheels�͂��ꂼ��ExampleRobot.StructuredModlues�ŗ^���Ă�����ModuleGroup�̖��O�ł�
-- StructuredModlues�ŗ^�����؍\���̒ʂ�� . ��؂�ł��ǂ�ΌĂяo���܂��B
-- �������炳��� . ��؂�ł��炩���ߐݒ肵�Ă�����Feature���֐��Ƃ��ČĂяo�����Ƃ��ł��܂��B
-- �Ⴆ�Έȉ��̂悤��

if input.IsButtonDown(gamepad,buttons.RightShoulder) then
	modules.container.open()
end
if input.IsButtonUp(Gamepad,buttons.LeftShoudlder) then
	modules.container.close()
end

-- �A�N�Z�X�ł���v�f�̃��X�g��wimm�ɓǂݍ��܂����Ƃ��Ɏ����������� docs/Reference.html�ł��m�F�ł��܂��B
-- �P���ɂ���{�^���������ꂽ�Ƃ��ɂȂɂ��֐����Ăяo�������ł悢�Ȃ�control_map.lua�𗘗p���邱�Ƃ��ł��܂��B