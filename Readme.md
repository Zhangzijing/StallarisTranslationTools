## 使用顺序:
+ IndexTranslation.exe --> LocateTranslation.exe --> CleanNumber.exe

## 文件说明:
+ 文件: IndexTranslation.exe 翻译索引器
+ 用途: 给所有翻译文件编号(也就是下文所说的"索引")
+ 使用方法:
  - 1. 把localisation文件夹(未索引过)复制到IndexTranslation.exe同级目录下
  - 2. 打开IndexTranslation.exe, 按提示操作.
  - 3. 程序目录生成了Mapping文件, 文本格式, 请妥善保管.
  - 4. 可以拿去测试, 内测或公测, 这样玩家遇到感觉翻译不对的地方反馈翻译ID即可.

+ 文件: LocateTranslation.exe 翻译定位器
+ 用途: 定位翻译在的文件和行
+ 使用方法:
  - 1. 如果你安装了notepad++, 找到notepad++的程序路径, 保存在LocateTranslation.exe同级目录下
  - 2. 把Mapping文件复制到LocateTranslation.exe同级目录下
  - 3. 把localisation文件夹(索引过或未索引过的皆可)复制到LocateTranslation.exe同级目录下
  - 4. 打开LocateTranslation.exe, 按提示操作.

+ 文件: CleanNumber.exe 索引清除器
+ 用途: 清除索引, 正式发布时用
+ 使用方法:
  - 1. 把localisation文件夹(索引过的)复制到CleanNumber.exe同级目录下
  - 2. 打开CleanNumber.exe, 按提示操作.
+ 更新日志:
		2016/5/22 1.1版本 增加了错误检测功能

	文件: ReplaceCrlf.exe 替换\r\n为\n
	用途: 解决所谓的下划线问题
	使用方法:
		1. 文件拖上去
