using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;

namespace clipboard
{
    class Program
    {
        // Copilot の入力欄にクリップボードの内容を送信
        static void SendToCopilot(string text)
        {
            var edge = AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.ClassNameProperty, "Chrome_WidgetWin_1")
            );

            if (edge == null) return;

            // Copilot の入力欄（AutomationId=userInput）
            var input = edge.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.AutomationIdProperty, "userInput")
            );

            if (input == null) return;

            // ValuePattern で直接入力
            if (input.TryGetCurrentPattern(ValuePattern.Pattern, out object pattern))
            {
                var vp = (ValuePattern)pattern;
                vp.SetValue(text);
            }

            // Enter で送信
            SendKeys.SendWait("{ENTER}");
        }

        // Copilot の最新メッセージを取得
        static string GetLatestMessage()
        {
            var edge = AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.ClassNameProperty, "Chrome_WidgetWin_1")
            );

            if (edge == null) return "";

            while (true)
            {
                try
                {
                    // Copilot の返答ブロック（ai-message）だけを取得
                    var msgs = edge.FindAll(
                        TreeScope.Descendants,
                        new PropertyCondition(AutomationElement.ClassNameProperty, "group/ai-message")
                    );

                    if (msgs == null || msgs.Count == 0)
                        return "";

                    // 最新の返答ブロック
                    var latest = msgs[msgs.Count - 1];

                    // 返答ブロック内の Text を全部取得
                    var texts = latest.FindAll(
                        TreeScope.Descendants,
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text)
                    );

                    if (texts == null || texts.Count == 0)
                        return "";

                    var sb = new StringBuilder();

                    // texts[0] はラベルなのでスキップ
                    for (int i = 1; i < texts.Count; i++)
                    {
                        string line = texts[i].Current.Name;

                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        // 正規化
                        line = line
                            .Replace('\u2010', '-')  // ハイフン
                            .Replace('\u2011', '-')  // ノンブレークハイフン
                            .Replace('\u2012', '-')  // フィギュアダッシュ
                            .Replace('\u2013', '-')  // エンダッシュ
                            .Replace('\u2014', '-')  // エムダッシュ
                            .Replace('\u2015', '-')  // ホリゾンタルバー
                            .Replace('\u2212', '-')  // マイナス記号
                            .Replace('／', '/')      // 全角スラッシュ
                            .Replace('＼', '\\')     // 全角バックスラッシュ
                            .Replace('＿', '_');     // 全角アンダーバー

                        sb.AppendLine(line);
                    }

                    return sb.ToString().TrimEnd('\r', '\n');
                }
                catch (ElementNotAvailableException)
                {
                    Thread.Sleep(50);
                    continue;
                }
            }
        }

        // Copilot の行数
        static int GetLatestMessageLineCount()
        {
            var edge = AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.ClassNameProperty, "Chrome_WidgetWin_1")
            );

            if (edge == null) return 0;

            // 最新の ai-message を探す
            var msgs = edge.FindAll(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ClassNameProperty, "group/ai-message")
            );

            if (msgs == null || msgs.Count == 0)
                return 0;

            var latest = msgs[msgs.Count - 1];

            // その中の Text ノードを全部数える
            var texts = latest.FindAll(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text)
            );

            return texts?.Count ?? 0;
        }

        // Copilot の生成完了待ち
        static string WaitUntilDone()
        {
            string last = "";
            int stable = 0;

            while (true)

            {
                string text = GetLatestMessage();

                if (string.IsNullOrWhiteSpace(text))
                {
                    Thread.Sleep(100);
                    continue;
                }

                if (text == "考え中…" ||
                    text == "自分の考えを整理し、計画を作成する")
                {
                    Thread.Sleep(100);
                    continue;
                }

                if (GetLatestMessageLineCount() < 3)
                {
                    Thread.Sleep(100);
                    continue;
                }

                // 安定判定
                if (text != last)
                {
                    last = text;
                    stable = 0;
                }
                else
                {
                    stable++;
                }

                if (stable >= 4)
                    return last;

                Console.WriteLine("待機中");
                Thread.Sleep(100);
            }
        }

        [STAThread]
        static void Main()
        {
            // テスト用のプロンプト
            // deepthinkを使って、世の中のめちゃくちゃ難しい問題に自分なりの答えを出して。

            // myfile.txt を空にする
            File.WriteAllText("myfile.txt", "", Encoding.UTF8);

            // クリップボードの内容を取得
            string prompt = Clipboard.GetText();

            if (string.IsNullOrWhiteSpace(prompt))
            {
                Console.WriteLine("クリップボードが空です。");

                return;
            }

            // Copilot に送信
            SendToCopilot(prompt);

            // 完了待ち
            string result = WaitUntilDone();

            // 実行ファイルと同じフォルダに myfile.txt を保存
            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            string outPath = Path.Combine(exeDir, "myfile.txt");

            File.WriteAllText(outPath, result, Encoding.UTF8);

            Console.WriteLine("=== Copilot 応答 ===");
            Console.WriteLine(result);
            Console.WriteLine("====================");
            Environment.Exit(0);
        }
    }
}
