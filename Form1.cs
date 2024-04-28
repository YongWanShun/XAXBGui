using System.Diagnostics.Metrics;
using System.Reflection;

namespace XAXBGui
{
    public partial class Form1 : Form
    {
        private string answer;//答案
        private int attempts;//嘗試的次數
        public Form1()
        {
            InitializeComponent();
            StartNewGame();//呼叫方法，初始化游戲
            label1.Text = "請輸入三個不重複的數字(0~9)";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckGuess();//呼叫方法判斷輸贏
        }
        private void StartNewGame()//初始化游戲
        {
            answer = GenerateAnswer();
            attempts = 0;
            textBox1.Text = ""; // 清空猜測框
            textBox2.Text = ""; // 清空提示欄
        }
        private string GenerateAnswer()//隨機生成三個數字
        {
            Random random = new Random();
            string[] digits = Enumerable.Range(0, 10).Select(i => i.ToString()).ToArray();
            digits = digits.OrderBy(d => random.Next()).ToArray();
            return string.Join("", digits.Take(3));
        }
        
        private void CheckGuess()//檢查數字的方法
        {
            string guess = textBox1.Text.Trim();
            textBox1.Text = "";

            if (guess.Length != 3 || !guess.All(char.IsDigit))
            {
                MessageBox.Show("請輸入一個有效的3位數字。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            attempts++;
            int aCount = 0;
            int bCount = 0;

            for (int i = 0; i < 3; i++)
            {
                if (guess[i] == answer[i])
                {
                    aCount++;
                }
                else if (answer.Contains(guess[i]))
                {
                    bCount++;
                }
            }
            //判斷是否勝利
            if (aCount == 3)
            {
                MessageBox.Show($"恭喜你猜對了！答案是 {answer}，共猜了 {attempts} 次。", "遊戲結束", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StartNewGame();
            }
            else
            {
                //告訴玩家結果
                textBox2.Text += $"提示：{aCount}A:{bCount}B 輸入的數字：{guess}\r\n";
            }
        }

        
    }
}
