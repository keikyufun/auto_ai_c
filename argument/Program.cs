using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;

namespace argument
{
    class Program
    {
        // Copilot に送信
        static void SendToCopilot(string text)
        {
            Clipboard.SetText(text);

            var edge = AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.ClassNameProperty, "Chrome_WidgetWin_1")
            );

            if (edge == null) return;

            var input = edge.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.AutomationIdProperty, "userInput")
            );

            if (input == null)
            {
                Console.WriteLine("入力欄が見つかりません");
                return;
            }

            input.SetFocus();
            SendKeys.SendWait("^v");
            SendKeys.SendWait("{ENTER}");
        }

        // 最新の返答本文を取得
        static string GetLatestMessage()
        {
            var edge = AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.ClassNameProperty, "Chrome_WidgetWin_1")
            );

            if (edge == null) return "";

            var msgs = edge.FindAll(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ClassNameProperty, "group/ai-message")
            );

            if (msgs == null || msgs.Count == 0)
                return "";

            var latest = msgs[msgs.Count - 1];

            var texts = latest.FindAll(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text)
            );

            if (texts == null || texts.Count == 0)
                return "";

            var sb = new StringBuilder();

            for (int i = 1; i < texts.Count; i++)
            {
                string line = texts[i].Current.Name;

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                line = line
                    .Replace('\u2010', '-')
                    .Replace('\u2011', '-')
                    .Replace('\u2012', '-')
                    .Replace('\u2013', '-')
                    .Replace('\u2014', '-')
                    .Replace('\u2015', '-')
                    .Replace('\u2212', '-')
                    .Replace('／', '/')
                    .Replace('＼', '\\')
                    .Replace('＿', '_');

                sb.AppendLine(line);
            }

            return sb.ToString().Trim();
        }

        // 最新 ai-message の Text ノード数
        static int GetLatestMessageLineCount()
        {
            var edge = AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.ClassNameProperty, "Chrome_WidgetWin_1")
            );

            if (edge == null) return 0;

            var msgs = edge.FindAll(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ClassNameProperty, "group/ai-message")
            );

            if (msgs == null || msgs.Count == 0)
                return 0;

            var latest = msgs[msgs.Count - 1];

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

                Thread.Sleep(100);
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("引数がありません。");
                return;
            }

            string prompt = args[0];

            // Copilot に送信
            SendToCopilot(prompt);

            // 完了待ち
            string result = WaitUntilDone();

            // 保存処理
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
