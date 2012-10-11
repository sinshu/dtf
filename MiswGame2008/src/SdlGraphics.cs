using System;
using Yanesdk.Ytl;
using Yanesdk.Draw;

namespace MiswGame2008
{
    public class SdlGraphics : IGraphics
    {
        private SDLWindow window;
        private IScreen screen;
        private int width, height;
        private ITexture[] textures;

        public SdlGraphics(SDLWindow window)
        {
            this.window = window;
            screen = window.Screen;
            width = screen.Width;
            height = screen.Height;
            Begin();
            SetColor(255, 128, 128, 128);
            Fill();
            End();
            Initialize();
        }

        private void Initialize()
        {
            // ファイル名をenum Imageから取ってくる方が後々楽じゃね？
            textures = new ITexture[Utility.GetEnumCount(typeof(Image))];
            for (int i = 0; i < textures.Length; i++)
            {
                string path = "dtf/images/" + Enum.GetName(typeof(Image), i) + ".png";
                Console.WriteLine("画像「" + path + "」を読み込みます。");
                textures[i] = LoadTextureByPath(path);
            }
        }

        private ITexture LoadTextureByPath(string path)
        {
            GlTexture texture = new GlTexture();
            texture.LocalOption.Smooth = false;
            YanesdkResult result = texture.Load(path);
            if (result == YanesdkResult.NoError)
            {
                return texture;
            }
            else
            {
                throw new Exception("画像「" + path + "」が読み込めません＞＜");
            }
        }

        public void Begin()
        {
            screen.Select();
            screen.Blend = true;
            screen.BlendSrcAlpha();
        }

        public void End()
        {
            screen.Update();
        }

        public void EnableAddBlend()
        {
            screen.BlendAddColor();
        }

        public void DisableAddBlend()
        {
            screen.BlendSrcAlpha();
        }

        public void SetColor(int a, int r, int g, int b)
        {
            screen.SetColor(r, g, b, a);
        }

        public void Fill()
        {
            int x1 = 0, y1 = 0;
            int x2 = width, y2 = 0;
            int x3 = width, y3 = height;
            int x4 = 0, y4 = height;
            screen.DrawPolygon(x1, y1, x2, y2, x3, y3, x4, y4);
        }

        public void DrawRectangle(int x, int y, int width, int height)
        {
            int x1 = x, y1 = y;
            int x2 = x + width, y2 = y;
            int x3 = x + width, y3 = y + height;
            int x4 = x, y4 = y + height;
            screen.DrawPolygon(x1, y1, x2, y2, x3, y3, x4, y4);
        }

        public void DrawImage(Image image, int x, int y)
        {
            screen.Blt(textures[(int)image], x, y);
        }

        public void DrawImage(Image image, int x, int y, int width, int height, int row, int col)
        {
            int left = width * col;
            int top = height * row;
            int right = left + width;
            int bottom = top + height;
            Rect srcRect = new Rect((float)left, (float)top, (float)right, (float)bottom);
            screen.Blt(textures[(int)image], x, y, srcRect);
        }

        public void DrawImage(Image image, int x, int y, int width, int height, int row, int col, int deg)
        {
            int left = width * col;
            int top = height * row;
            int right = left + width;
            int bottom = top + height;
            Rect srcRect = new Rect((float)left, (float)top, (float)right, (float)bottom);
            int yaneRad = -(int)Math.Round((double)(deg * 512) / 360);
            screen.BltRotate(textures[(int)image], x, y, srcRect, yaneRad, 1.0f, width / 2, height / 2);
        }

        public void DrawImage(Image image, int x, int y, int width, int height, int row, int col, int deg, double stretch)
        {
            int left = width * col;
            int top = height * row;
            int right = left + width;
            int bottom = top + height;
            Rect srcRect = new Rect((float)left, (float)top, (float)right, (float)bottom);
            int yaneRad = -(int)Math.Round((double)(deg * 512) / 360);
            screen.BltRotate(textures[(int)image], x, y, srcRect, yaneRad, (float)stretch, 0, 0);
        }

        public void DrawObject(Image image, int x, int y, int width, int height, int row, int col, int deg)
        {
            int left = width * col;
            int top = height * row;
            int right = left + width;
            int bottom = top + height;
            Rect srcRect = new Rect((float)left, (float)top, (float)right, (float)bottom);
            int yaneRad = -(int)Math.Round((double)(deg * 512) / 360);
            screen.BltRotate(textures[(int)image], x - width / 2, y - height / 2, srcRect, yaneRad, 1.0f, width / 2, height / 2);
        }

        public void DrawBullet(Image image, int x, int y, int width, int height, int row, int col, int deg)
        {
            int left = width * col;
            int top = height * row;
            int right = left + width;
            int bottom = top + height;
            Rect srcRect = new Rect((float)left, (float)top, (float)right, (float)bottom);
            int yaneRad = -(int)Math.Round((double)(deg * 512) / 360);
            screen.BltRotate(textures[(int)image], x - width, y - height / 2, srcRect, yaneRad, 1.0f, width, height / 2);
        }

        public void DrawBackground(Image image, int x, int y, int width, int height, int row, int col, double stretch)
        {
            int left = width * col;
            int top = height * row;
            int right = left + width;
            int bottom = top + height;
            Rect srcRect = new Rect((float)left, (float)top, (float)right, (float)bottom);
            Point[] points = new Point[4];
            points[0] = new Point((float)x, (float)(y * stretch));
            points[1] = new Point((float)(x + width), (float)(y * stretch));
            points[2] = new Point((float)(x + width), (float)((y + height) * stretch));
            points[3] = new Point((float)x, (float)((y + height) * stretch));
            screen.Blt(textures[(int)image], srcRect, points);
        }

        public void DrawCharacter(char c, int x, int y)
        {
            ITexture texture = textures[(int)Image.Hud];
            int u, v;
            if ('0' <= c && c <= '9')
            {
                u = (c - '0') * 16;
                v = 0;
            }
            else if ('A' <= c && c <= 'P')
            {
                u = (c - 'A') * 16;
                v = 16;
            }
            else if ('Q' <= c && c <= 'Z')
            {
                u = (c - 'Q') * 16;
                v = 32;
            }
            else
            {
                switch (c)
                {
                    case '#':
                        u = 160;
                        v = 0;
                        break;
                    case '$':
                        u = 160;
                        v = 32;
                        break;
                    case '_':
                        u = 176;
                        v = 32;
                        break;
                    case '<':
                        u = 0;
                        v = 48;
                        break;
                    case '>':
                        u = 16;
                        v = 48;
                        break;
                    default:
                        return;
                }
            }
            Rect srcRect = new Rect((float)u, (float)v, (float)(u + 16), (float)(v + 16));
            screen.Blt(texture, x, y, srcRect);
        }

        public void DrawString(string s, int x, int y)
        {
            for (int i = 0; i < s.Length; i++)
            {
                DrawCharacter(s[i], x + i * 16, y);
            }
        }
    }
}
