// <applet code="Smoke.class" width="640" height="480"></applet>
import java.applet.*;
import java.awt.*;

public class Smoke extends Applet
{
	static final int WIDTH = 32;
	static final int N = 64;
	
	public void paint(Graphics g)
	{
		g.setColor(new Color(128, 0, 128));
		g.fillRect(0, 0, 640, 480);
		g.setColor(new Color(0, 0, 0));
		g.fillRect(0, 0, WIDTH * 4, WIDTH * 11);
		
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
			s[i] = Math.random() / 44;
			l[i] = (int)Math.floor(40 * Math.random()) + 4;
		}
		Color[] c = new Color[6];
		c[0] = new Color(64, 64, 64);
		c[1] = new Color(128, 128, 128);
		c[2] = new Color(192, 192, 192);
		
		for (int i = 0; i < 44; i++)
		{
			int xOffset = i % 4 * WIDTH + WIDTH / 2;
			int yOffset = i / 4 * WIDTH + WIDTH / 2;
			for (int j = 0; j < N; j++)
			{
				if (l[j] < i) continue;
				double newX = x[j] + WIDTH / 2 * s[j] * Math.cos(r[j]);
				double newY = y[j] + WIDTH / 2 * s[j] * Math.sin(r[j]);
				int x1 = xOffset + (int)Math.round(x[j]);
				int y1 = yOffset + (int)Math.round(y[j]);
				int x2 = xOffset + (int)newX;
				int y2 = yOffset + (int)newY;
				g.setColor(c[(i + j) % c.length]);
				// g.drawLine(x1, y1, x2, y2);
				g.fillRect(x2, y2, 1, 1);
				x[j] = newX;
				y[j] = newY;
			}
		}
	}
}
