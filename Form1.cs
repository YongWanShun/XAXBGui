using System.Diagnostics.Metrics;
using System.Reflection;

namespace XAXBGui
{
    public partial class Form1 : Form
    {
        private XAXBEngine xaxb;
        private int attempts;//嘗試的次數
        public Form1()
        {
            InitializeComponent();
            xaxb = new XAXBEngine();
            attempts = 0;
            label1.Text = "請輸入三個不重複的數字(0~9)";//告訴玩家輸入的提示
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text.Trim();//讀取輸入欄的資料
            if (!xaxb.IsLegal(input))//判斷輸入是否合法
            {
                MessageBox.Show("請輸入一個有效的3位數字。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                attempts++;
                if (xaxb.IsGameover(input))//判段游戲是否勝利
                {
                    MessageBox.Show($"恭喜你猜對了！答案是 {input}，共猜了 {attempts} 次。", "遊戲結束", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    xaxb.generateAnswer();//重新生成數字
                    textBox1.Text = "";//清空輸入欄
                    textBox2.Text = "";//清空信息欄
                }
                else
                {
                    //告訴玩家結果
                    textBox2.Text += $"{xaxb.GetResult(input)} 輸入的數字：{input}\r\n";
                    textBox1.Text = "";//清空輸入欄
                }
            }
        }
    }
    class XAXBEngine
    {
        private string answer;//存放正確答案的變數

        public XAXBEngine()
        {
            generateAnswer();
        }  
        public void generateAnswer()//生成3個誰記得數字
        {
            Random random = new Random();
            string[] digits = Enumerable.Range(0, 10).Select(i => i.ToString()).ToArray();
            digits = digits.OrderBy(d => random.Next()).ToArray();
            answer = string.Join("", digits.Take(3));
        }
        //檢查輸入的數字是否正確
        public bool IsLegal(string userNumber)
        {
            if (userNumber.Length != 3)
            {
                return false;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = i + 1; j < userNumber.Length; j++)
                {
                    if (userNumber[i] == userNumber[j])
                        return false;
                }
            }
            return true;
        }
        public string GetResult(string userNumber)//取得當前的結果
        {
            int aCount = 0;
            int bCount = 0;

            for (int i = 0; i < 3; i++)
            {
                if (userNumber[i] == answer[i])
                {
                    aCount++;
                }
                else if (answer.Contains(userNumber[i]))
                {
                    bCount++;
                }
            }
            return $"{aCount}A{bCount}B";
        }
        public bool IsGameover(String userNumber)//判斷游戲是否結束
        {
            if (GetResult(userNumber) == "3A0B")
            {
                return true;
            }
            return false;
        }
    }
}
