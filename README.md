# ClipboardToolForBakin2

This is a support tool for copying and pasting the Talk (conversation), Message, and Notes event panels of RPG Developer Bakin.  
It is intended to be used for writing scenarios and then copying and pasting them into Bakin as a conversation panel.

The basic implementation is the same as ToolForBakin, but extended so that NPL, NPC, NPR, blspd, blrate, and lpspd can be set from the UI.
Since the way of handling data is different and the implementation of preview functions, etc. is under consideration, we have separated them into separate repositories instead of branches.
Operation has not been verified for blspd, blrate, and lpspd. (Ver1.0.5)

You can also clipboard from Bakin, but only the three types of event panels mentioned above can be copied.  
(The data size and other information will be unknown, so the process will exit when there is a panel that is not supported.)  
Depending on the state of the copied data, this application may fail.

Also, the data structure of the clipboard is based on the output, so unexpected errors may occur, especially if there is an update on the Bakin side.  
Please be sure to back up your project before use.

In most cases, we have confirmed that there is no problem by forcibly closing the event editor while editing, even if an unexpected format error occurs, but it may be necessary to forcibly close Bakin itself.

---

RPG Developer BakinのTalk(conversation), Message, Notesのイベントパネルを良しなにコピペする為のサポートツールです。  
シナリオを書いてから会話パネルとして纏めてBakinにコピペしたりする使い方を想定しています。

ToolForBakinと基本的な実装は同じですが、NPL,NPC,NPR,blspd,blrate,lpspdをUI上から設定できるように拡張しています。
データの扱い方が異なり、プレビュー機能等の実装も検討している為、ブランチとせず別のリポジトリに分けています。
blspd,blrate,lpspdに関しては動作未検証です。(ver1.0.5)

Bakin側からクリップボードで取り込むこともできますが、前述の3種類のイベントパネル以外は取り込めません。  
（データサイズ等が不明な状態となる為、サポート外のパネルがあった時点で処理を抜けます。コピーした状態によっては本アプリがエラーとなります。）

また、クリップボードのデータ構造は、出力結果からの推測である為、予期しないエラー（特にBakin側のアップデートがあった場合等）が発生する事があります。  
必ずプロジェクトのバックアップを取ってから利用してください。

殆どの場合、想定外のフォーマットエラーになっても編集中のイベントエディタを強制的に閉じる事で問題ない事は確認していますが、Bakin本体を強制終了しなければならなくなる事もあり得ます。
