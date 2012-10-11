// <applet code="Explosion.class" width="640" height="480"></applet>
import java.applet.*;
import java.awt.*;

public class Explosion extends Applet
{
	static final int WIDTH = 32;
	static final int N = 64;
	
	public void paint(Graphics g)
	{
		g.setColor(new Color(128, 0, 128));
		g.fillRect(0, 0, 640, 480);
		g.setColor(new Color(0, 0, 0));
		g.fillRect(0, 0, WIDTH * 4, WIDTH * 4);
		
		double[] x = new double[N];
		double[] y = new double[N];
		double[] r = new double[N];
		double[] s = new double[N];
		double[] l = new double[N];
		for (int i = 0; i < N; i++)
		{
			x[i] = 0;
			y[i] = 0;
			r[i] = 2 * Math.PI * Math.random();
			s[i] = 0.75 * Math.random();
			l[i] = (int)Math.floor(16 * Math.random());
		}
		Color[] c = new Color[6];
		c[0] = new Color(255, 192, 192);
		c[1] = new Color(255, 255, 192);
		c[2] = new Color(192, 255, 192);
		c[3] = new Color(192, 255, 255);
		c[4] = new Color(192, 192, 255);
		c[5] = new Color(255, 192, 255);
		
		for (int i = 0; i < 16; i++)
		{
			int xOffset = i % 4 * WIDTH + WIDTH / 2;
			int yOffset = i / 4 * WIDTH + WIDTH / 2;
			{
				int rand = (int)Math.floor(16 * Math.random());
				int rad = (int)Math.floor(Math.sqrt(x[rand] * x[rand] + y[rand] * y[rand]));
				g.setColor(new Color(255, 255, 255));
				g.drawOval(xOffset - rad, yOffset - rad, rad * 2, rad * 2);
				for (int j = 0; j < (i + 1); j++)
				{
					double br = 2 * Math.PI * Math.random();
					int bx = (int)Math.round(rad * Math.cos(br));
					int by = (int)Math.round(rad * Math.sin(br));
					g.setColor(new Color(0, 0, 0));
					g.fillRect(xOffset + bx - 1, yOffset + by - 1, 2, 2);
				}
			}
			for (int j = 0; j < N; j++)
			{
				if (l[j] < i) continue;
				double newX = WIDTH / 2 * Math.cos(r[j]) * s[j] + x[j] * (1 - s[j]);
				double newY = WIDTH / 2 * Math.sin(r[j]) * s[j] + y[j] * (1 - s[j]);
				int x1 = xOffset + (int)Math.round(x[j]);
				int y1 = yOffset + (int)Math.round(y[j]);
				int x2 = xOffset + (int)newX;
				int y2 = yOffset + (int)newY;
				g.setColor(c[(i + j) % c.length]);
				g.drawLine(x1, y1, x2, y2);
				// g.fillRect(x2, y2, 2, 2);
				x[j] = newX;
				y[j] = newY;
			}
		}
	}
}
