# Wimm Machine Example

## �V�K���{�b�g�쐬�̎菇
���̗�ł�VisualStudio 2022���g�p

### Wimm�̃r���h
[Wimm](https://github.com/yuki-asagoe/Wimm)�̃��|�W�g�����N���[����VisualStudio�ŊJ���A  
`[�r���h] > [�\�����[�V�����̃��r���h]`��I���B

����ɂ���ăr���h���ʕ���DLL�t�@�C���Ȃǂ����ꂼ��̃v���W�F�N�g��`bin`�t�H���_�ɐ�������܂��B

(���̎菇�ɂ��ď����I�ɂ�Nuget�Ȃǂ��璼�ڈˑ��֌W�����ł���悤�ɂ��邩�Œ�ł�Wimm�̃��|�W�g�����琬�ʕ��̃t�@�C���𒼐ڃ_�E�����[�h�ł���悤�ɂ���ׂ��A���ݕۗ���)

�ȉ��͂��̎菇�ō쐬�����t�@�C���𒼐ڎg�p����ꍇ�̎菇�ł��B
### �v���W�F�N�g�̍쐬
`[�V�����v���W�F�N�g���쐬] > [�N���X���C�u����] > [(�v���W�F�N�g�����̐ݒ�)]`

### �ˑ��֌W�̒ǉ�
1. `[�\�����[�V�����G�N�X�v���[���[] > [(�쐬�����v���W�F�N�g)] > [�ˑ��֌W]` ���E�N���b�N�� `[�v���W�F�N�g�Q�Ƃ̒ǉ�]`�Ȃǂ���Q�ƃ}�l�[�W�����J��
2. `[�Q��]` ����쐬����DLL�t�@�C����I�����ˑ��֌W�ɒǉ�����
  - �Œ�ł��ȉ��̃A�Z���u���͕K�{
	- `Wimm.Common`
	- `Wimm.Machines`
  - �ȉ��͐���
	- `Wimm.Machines.TpipForRasberryPi`(TPIP for RasberryPi���g�p����ꍇ)
	- `Wimm.Machines.Tpip3`(TPIP 3���g�p����ꍇ)

### Tpip���g�p����ꍇ
`[�\�����[�V�����G�N�X�v���[���[] > [(�쐬�����v���W�F�N�g)]` ���_�u���N���b�N���A�v���W�F�N�g�t�@�C���Ɉȉ��̕ύX

- `<PropertyGroup>`���� `<TargetFramework>net8.0-windows</TargetFramework>`
- `<PropertyGroup>`���� `<UseWPF>true</UseWPF>`

### �x�[�X�ƂȂ郍�{�b�g�N���X�̍쐬
`[�\�����[�V�����G�N�X�v���[���[] > [(�쐬�����v���W�F�N�g)]` ���E�N���b�N�� `[�ǉ�] > [�V��������]` ���玟�̃t�@�C�����쐬
- `{YourMachineName}.cs`(���̗�ł�`ExampleRobot.cs`)
- `AssemblyInfo.cs`

�N���X`{YourMachineName}`(���̗�ł�`ExampleRobot`)�ɃN���X`Wimm.Machines.Machine`���p�������܂��B(���̗�ł�`TpipForRasberryPiMachine`���p��)

���̌�`AssemblyInfo.cs`�ɂăA�Z���u���� `LoadTargetAttribute`��t�^���܂��B������`typeof({YourMachineName})`��^����B

### ���{�b�g�̎���
�쐬���� `{YourMachineName}` �N���X���������܂��B�ڍׂ̓R�����g���Q��

### Wimm�ւ̓ǂݍ���
`[�r���h] > [�\�����[�V�����̃��r���h]`��I�����A�������ꂽ���ʕ�(���̃v���W�F�N�g�ł�`Wimm_Machine_Example.dll`)��Wimm����ǂݍ��݂܂��B

��肪�Ȃ���Ί֘A����t�@�C���������œW�J����AWimm�̃����`���[�ɓǂݍ��܂��͂��ł��B

���̂��ƕK�v�Ȃ琶�����ꂽ����X�N���v�g�t�@�C����ҏW���܂��B

����X�N���v�g�t�@�C�����͂��߂Ƃ���ꕔ�̊֘A�t�@�C���͂��炩���ߖ��ߍ��݂��邱�Ƃ��ł��܂��B`AssemblyInfo.cs`��`on_control.lua`���Q��
