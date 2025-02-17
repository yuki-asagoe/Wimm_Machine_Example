# Wimm Machine Example

## 新規ロボット作成の手順
この例ではVisualStudio 2022を使用

### Wimmのビルド
[Wimm](https://github.com/yuki-asagoe/Wimm)のリポジトリをクローンしVisualStudioで開き、  
`[ビルド] > [ソリューションのリビルド]`を選択。

これによってビルド成果物のDLLファイルなどがそれぞれのプロジェクトの`bin`フォルダに生成されます。

(この手順について将来的にはNugetなどから直接依存関係解決できるようにするか最低でもWimmのリポジトリから成果物のファイルを直接ダウンロードできるようにするべき、現在保留中)

以下はこの手順で作成したファイルを直接使用する場合の手順です。
### プロジェクトの作成
`[新しいプロジェクトを作成] > [クラスライブラリ] > [(プロジェクト名等の設定)]`

### 依存関係の追加
1. `[ソリューションエクスプローラー] > [(作成したプロジェクト)] > [依存関係]` を右クリックし `[プロジェクト参照の追加]`などから参照マネージャを開く
2. `[参照]` から作成したDLLファイルを選択し依存関係に追加する
  - 最低でも以下のアセンブリは必須
	- `Wimm.Common`
	- `Wimm.Machines`
  - 以下は推奨
	- `Wimm.Machines.TpipForRasberryPi`(TPIP for RasberryPiを使用する場合)
	- `Wimm.Machines.Tpip3`(TPIP 3を使用する場合)

### Tpipを使用する場合
`[ソリューションエクスプローラー] > [(作成したプロジェクト)]` をダブルクリックし、プロジェクトファイルに以下の変更

- `<PropertyGroup>`内に `<TargetFramework>net8.0-windows</TargetFramework>`
- `<PropertyGroup>`内に `<UseWPF>true</UseWPF>`

### ベースとなるロボットクラスの作成
`[ソリューションエクスプローラー] > [(作成したプロジェクト)]` を右クリックし `[追加] > [新しい項目]` から次のファイルを作成
- `{YourMachineName}.cs`(この例では`ExampleRobot.cs`)
- `AssemblyInfo.cs`

クラス`{YourMachineName}`(この例では`ExampleRobot`)にクラス`Wimm.Machines.Machine`を継承させます。(この例では`TpipForRasberryPiMachine`を継承)

その後`AssemblyInfo.cs`にてアセンブリに `LoadTargetAttribute`を付与します。引数に`typeof({YourMachineName})`を与える。

### ロボットの実装
作成した `{YourMachineName}` クラスを実装します。詳細はコメントを参照

### Wimmへの読み込み
`[ビルド] > [ソリューションのリビルド]`を選択し、生成された成果物(このプロジェクトでは`Wimm_Machine_Example.dll`)をWimmから読み込みます。

問題がなければ関連するファイルが自動で展開され、Wimmのランチャーに読み込まれるはずです。

このあと必要なら生成された制御スクリプトファイルを編集します。

制御スクリプトファイルをはじめとする一部の関連ファイルはあらかじめ埋め込みすることができます。`AssemblyInfo.cs`や`on_control.lua`を参照
