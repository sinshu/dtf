using System;
using System.Windows.Forms;
using Yanesdk.Ytl;
using Yanesdk.Draw;
using Yanesdk.Timer;
using Yanesdk.System;

namespace MiswGame2008
{
    public class MiswGame2008 : IDisposable
    {
        private SDLWindow window;
        private SdlGraphics graphics;
        private SdlInput input;
        private SdlAudio audio;
        private FpsTimer timer;

        private int startLevel;

        public MiswGame2008(bool fullscreen, int startLevel)
        {
            FileArchiverZip zip = new FileArchiverZip();
            zip.ZipExtName = ".bin";
            FileSys.AddArchiver(zip);
            window = new SDLWindow();
            window.SetCaption("DTF");
            window.BeginScreenTest();
            if (fullscreen)
            {
                window.TestVideoMode(640, 480, 32);
                window.TestVideoMode(640, 480, 16);
            }
            else
            {
                window.TestVideoMode(640, 480, 0);
            }
            window.EndScreenTest();
            graphics = new SdlGraphics(window);
            audio = new SdlAudio();
            input = new SdlInput(fullscreen);

            this.startLevel = startLevel;
        }

        public void Run()
        {
            timer = new FpsTimer();
            timer.Fps = 30;
            GameManager manager = new GameManager(audio, startLevel);
            manager.LoadScoreDataFromFile("score.dat");
            while (SDLFrame.PollEvent() == YanesdkResult.NoError)
            {
                input.Update();
                manager.Update(input);
                if (manager.Exiting)
                {
                    break;
                }
                if (!timer.ToBeSkip)
                {
                    graphics.Begin();
                    manager.Draw(graphics);
                    graphics.End();
                }
                timer.WaitFrame();
            }
            manager.SaveScoreDataToFile("score.dat");
        }

        public void Dispose()
        {
            SDLFrame.Quit();
            Console.WriteLine("èIóπÇµÇ‹ÇµÇΩÅB");
        }
    }
}
