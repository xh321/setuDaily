# setuDaily 更新日志

## 1.0.4:

* 可追加保存图片的EXIF信息，但代价是需要以JPG有损格式保存
* 快速保存改为保存EXIF，以JPG格式储存。因为EXIF中有原图链接，如有需要可随时访问原图
* 快速保存文件名修正为”色图标题@色图作者.jpg“，以避免多次保存造成的重复文件。



## 1.0.3:

* 修复缩放模式下的图片偏移BUG
* 增加R18涩图下拉框选项，以决定是否显示R18涩图
* 微调界面
* 保存时默认文件名为涩图标题
* 增加快速保存选项，键位替换掉保存图片的快捷键，为Ctrl+S
	* 快速保存为不选择路径，自动保存到可执行文件目录下的saves文件夹中
	* 文件名为：涩图标题_UNIX时间戳.jpg
	*注意：按保存快捷键时焦点一定要在窗体内部，不然可能会导致按键无效，请务必知悉。*
* 支持跳过加载缓慢的图片（注意，没有提示的，请不要不小心跳过自己心仪的涩图哦）
* 目前还存在一个BUG，也就是在后台加载的涩图会导致高度和宽度为0，你只需要重新拉大窗体即可（滚轮缩放模式下）。
* 增加重置缩放比例
* 支持直接打开作者Pixiv主页了



## 1.0.2：

* 加入图片右键快捷菜单，增加复制图片到剪贴板的功能，增加重新加载图片的菜单
* 注册全局快捷键，如下：
	* Ctrl+C：复制图片
	* Ctrl+S：保存图片
	* F5      ：重新加载图片
	* Enter  ：下一张涩图
	*右键菜单中也有对应快捷键说明*
* 增加色图分辨率的显示



## 1.0.1

* 增加复制色图链接功能
* 增加色图标题以及作者的显示
* 添加缩放至窗体大小的缩放选项，以显示高清色图（2K以上，取决于你的屏幕）。这个模式下窗体会最大化，图片也会随着最大化，并缩放至屏幕能容纳下。
* 优化一些体验


## 1.0.0

* 初版