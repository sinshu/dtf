using System;
using System.Windows.Forms;

namespace MiswGame2008
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int startLevel = 1;
            try
            {
                startLevel = int.Parse(args[0]);
            }
            catch
            {
            }
            try
            {
                DialogResult result = MessageBox.Show("フルスクリーンで起動しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                using (MiswGame2008 game = new MiswGame2008(result == DialogResult.Yes, startLevel))
                {
                    game.Run();
                }
            }
            catch (Exception e)
            {
                string message = e.Message + "\r\n\r\n↓スタックトレース↓\r\n" + e.StackTrace;
                MessageBox.Show(message, "＞＜", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
