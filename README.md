# Metasequoia.Sharp

C# �p Metasequoia �v���O�C�� �t���[�����[�N

## ����͉�

C# ���g�p���� Metasequoia �p�v���O�C�����쐬�\�ȃt���[�����[�N�ł��B

## clone

```
$ git clone --recursive https://github.com/mfakane/Metasequoia.Sharp.git
```

## �����

- Metasequoia 4.5.6 or later
- .NET Framework 4.5.1 or later

## �g�p���@

1. �쐬����v���O�C���� C# �v���W�F�N�g���쐬���A�\�����[�V�����t�H���_�� Metasequoia.Sharp �� clone
2. Metasequoia.Sharp ���L�v���W�F�N�g���\�����[�V�����ɒǉ����A�v���W�F�N�g����Q��
3. �v���W�F�N�g�t�@�C����ҏW���A`</Project>` ���O�Ɉȉ��̍s��ǉ�
```
<UsingTask AssemblyFile="$(SolutionDir)\Metasequoia.Sharp\Tools\DllExport.Tasks.dll" TaskName="DllExport.Tasks.DllExport" />
<Target Name="DllExport" AfterTargets="AfterBuild">
	<DllExport InputPath="$(TargetPath)" Configuration="$(Configuration)" />
</Target>
```

## ���C�Z���X

Metasequoia.Sharp �� zlib License �̉��ŗ��p�\�ł��B
