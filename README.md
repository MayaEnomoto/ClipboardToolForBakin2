# ClipboardToolForBakin2

[![](https://img.youtube.com/vi/shRh2ZDNV1A/0.jpg)](https://www.youtube.com/watch?v=shRh2ZDNV1A)

Shortcut keys are now customizable. Please refer to the end of this document for default values.

[English Guide (youtube)](https://www.youtube.com/watch?v=8xlUcF1CxJU)

[English GUide Ver2.4.0 function (youtube)](https://www.youtube.com/watch?v=Hfff0LJeg_8)

[Japanese GUide Ver2.5.0 function (english version not yet)](https://www.youtube.com/watch?v=0fjFRGD9lHA)

This is a support tool for copying and pasting the Talk (conversation), Message, and Notes event panels of RPG Developer Bakin.  
It is intended to be used for writing scenarios and then copying and pasting them into Bakin as a conversation panel.

The basic implementation is the same as ToolForBakin, but extended so that NPL, NPC, NPR, blspd, blrate, and lpspd can be set from the UI.
The recommended image file size is about 256x256 to 512x512 square.

You can also clipboard from Bakin, but only the three types of event panels mentioned above can be copied.  
We try to skip reading unsupported event panels as much as possible, but it is not perfect.
Basically, only the Talk, Message, and Notes event panels should be copied.

Also, the data structure of the clipboard is based on the output, so unexpected errors may occur, especially if there is an update on the Bakin side.  
Please be sure to back up your project before use.

---
[Japanese Guide (youtube)](https://www.youtube.com/watch?v=shRh2ZDNV1A)

[Japanese Guide Ver2.4.0 function (youtube)](https://www.youtube.com/watch?v=6zDd8ZkF47U)

[Japanese GUide Ver2.5.0 function (youtube)](https://www.youtube.com/watch?v=0fjFRGD9lHA)

RPG Developer BakinのTalk(会話), Message(メッセージ), Notes(注釈)のイベントパネルを良しなにコピペする為のサポートツールです。  
シナリオを書いてから会話パネルとして纏めてBakinにコピペしたりする使い方を想定しています。

ToolForBakinと基本的な実装は同じですが、NPL,NPC,NPR,blspd,blrate,lpspdをUI上から設定できるように拡張しています。
Toolに設定する画像ファイルは、256x256～512x512程度の正方形画像が推奨です。

Bakin側からクリップボードで取り込むこともできますが、前述の3種類のイベントパネル以外は取り込めません。  
※サポート外のイベントパネルはなるべく読み飛ばすように処理していますが、完全ではありません。
基本的にはTalk(会話), Message(メッセージ), Notes(注釈)のイベントパネルのみコピーするようにしてください。

また、クリップボードのデータ構造は、出力結果からの推測である為、予期しないエラー（特にBakin側のアップデートがあった場合等）が発生する事があります。  
必ずプロジェクトのバックアップを取ってから利用してください。

---
Additional libs (ClipboardToolForBakin)

[CsvHelper ](https://www.nuget.org/packages/csvhelper/)https://www.nuget.org/packages/csvhelper/

Additional libs (AsImageProcessingLibrary)

[ImageSharp ](https://www.nuget.org/packages/SixLabors.ImageSharp)https://www.nuget.org/packages/SixLabors.ImageSharp

---
Default Shortcut.

![shortcut](https://github.com/MayaEnomoto/ClipboardToolForBakin2/assets/2517436/1531a16f-89db-44ce-a874-210e04c28068)
