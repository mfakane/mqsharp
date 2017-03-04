# Metasequoia.Sharp

C# 用 Metasequoia プラグイン フレームワーク

## これは何

C# を使用して Metasequoia 用プラグインを作成可能なフレームワークです。

## clone

```
$ git clone --recursive https://github.com/mfakane/Metasequoia.Sharp.git
```

## 動作環境

- Metasequoia 4.5.6 or later
- .NET Framework 4.5.1 or later

## 使用方法

1. 作成するプラグインの C# プロジェクトを作成し、ソリューションフォルダに Metasequoia.Sharp を clone
2. Metasequoia.Sharp 共有プロジェクトをソリューションに追加し、プロジェクトから参照
3. プロジェクトファイルを編集し、`</Project>` 直前に以下の行を追加
```
<UsingTask AssemblyFile="$(SolutionDir)\Metasequoia.Sharp\Tools\DllExport.Tasks.dll" TaskName="DllExport.Tasks.DllExport" />
<Target Name="DllExport" AfterTargets="AfterBuild">
	<DllExport InputPath="$(TargetPath)" Configuration="$(Configuration)" />
</Target>
```

## ライセンス

Metasequoia.Sharp は zlib License の下で利用可能です。
