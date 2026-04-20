# auto_ai_c
## 説明
このツールは、様々なcopilotを自動化するツールです。
windows(x64)でしか動作しません。
以下のAIに対応しています。
- [microsoft copilot](https://copilot.microsoft.com)

microsoft copilot（以下copilot）に関しての説明＆規約です。
## copilotの規約に関して
本ツールは、以下の[Copilot利用規約](https://www.microsoft.com/ja-jp/microsoft-copilot/for-individuals/termsofuse?msockid=35905b86a50e65332c1f4d57a4966429)に完全に従う形で設計されています。

> ツールやコンピューター プログラム (ボットやスクレイパーなど) を使用して Copilot にアクセスしないでください。  
> Copilot は、個人的な使用のためにのみ使用できます。

本ツールは以下の点で規約に抵触しません：

- **個人利用の範囲でのみ使用されること**  
- **クラウドに公開されず、外部サービスとして提供されないこと**  
- **Copilot の出力を第三者に提供しないこと**  
- **Copilot の制限解除（ジェイルブレイク）を目的としないこと**

また、以下の規約にも準拠します：

> お客様や他のお客様に Copilot を提供する Microsoft の能力を損なわないこと。  
> 技術的な攻撃、過剰な使用、プロンプトベースの操作、「ジェイルブレイク」、その他の悪用に関与しないこと。

本ツールは Copilot の制限を解除したり、Microsoft の技術的制御を回避するものではありません。  
あくまで **ユーザー自身の PC 上での操作を自動化する補助ツール**です。
## auto_ai copilot向け規約
### 利用規約（本ツール独自）
- 本ツールを使用して **Copilot を API サーバー化しない**こと  
- 本ツールを使用して **他者の権利・プライバシーを侵害しない**こと  
- 本ツールを使用して **違法行為を行わない**こと  
- Copilot の複製・模倣 AI を作成・公開しないこと  
- 本ツールの使用によって発生したトラブル・損害について、開発者は一切責任を負いません

**必ず Copilot の利用規約に従って使用してください。**
### 許可される利用例
- **ローカルで完結する自動化ツールとして使用すること**  
  例）Copilot の応答をもとに、ファイル編集やターミナルコマンドを実行する

- **Flask を使って、自分のスマホから “自分の PC 上の Copilot” を遠隔操作すること**  
  ※LAN 内または VPN 内のみ  
  ※外部公開は禁止
等。必ずcopilot,本利用規約に準ずる使い方をしてください。
### 禁止される利用例
- Copilot の機能をクラウド経由で提供する  
- Copilot の出力を第三者に配布する  
- Copilot を API のように外部サービス化する  
- Copilot の制限解除（ジェイルブレイク）を目的とする
等。必ずcopilot,本利用規約に準ずる使い方をしてください。
## 使い方
※Cドライブ直下にファイルを置いたとします。
### argument.exe
プロンプトをcopilotに送信します。
```terminal:comand prompt
cd C:\
argument.exe "プロンプト"
```
必ず、Egde内のcopilotタブをアクティブ（開いている状態）にしておいてください。
copilotからの返答は、.exeとおなじディレクトリに、myfile.txtとして配置されています。
### clipboard.exe
クリップボードの中身をcopilotに送信します。
```terminal:comand prompt
cd C:\
clipbpard.exe
```
必ず、Egde内のcopilotタブをアクティブ（開いている状態）にしておいてください。
copilotからの返答は、.exeとおなじディレクトリに、myfile.txtとして配置されています。
