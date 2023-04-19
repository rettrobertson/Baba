using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.Particles
{
    public class Gradient
    {

        List<ColorNode> colors;
        
        private Gradient()
        {
            colors = new List<ColorNode>();
        }
        
        private struct ColorNode
        {
            public float t;
            public Color color;

            public ColorNode()
            {
                color = Color.Black;
                t = 0f;
            }

            public ColorNode(Color color, float t)
            {
                this.color = color;
                this.t = t;
            }
        }

        public Color Evaluate(float t)
        {
            ColorNode c2 = default;
            ColorNode c1 = default;

            for (int i = 1; i < colors.Count; i++)
            {
                if (colors[i].t > t)
                {
                    c2 = colors[i];
                    c1 = colors[i - 1];
                    break;
                }
            }

            float subT = (t - c1.t) / (c2.t - c1.t);
            return EvaluateSubNodes(c1, c2, subT);
        }

        private Color EvaluateSubNodes(ColorNode node1, ColorNode node2, float t)
        {
            return Color.Lerp(node1.color, node2.color, t);
        }

        public static Gradient FadeColor(Color color, float fadeStartTime = 0)
        {
            Gradient gradient = new Gradient();
            gradient.colors.Add(new ColorNode(color, 0));

            if (fadeStartTime > 0)
            {
                gradient.colors.Add(new ColorNode(color, fadeStartTime));
            }

            Color clearColor = new Color(color.R, color.G, color.B, (byte)0);
            gradient.colors.Add(new ColorNode(clearColor, 1));
            return gradient;
        }

        public static Gradient From2Color(Color color1, Color color2, float color2Time)
        {
            Gradient gradient = new Gradient();
            gradient.colors.Add(new ColorNode(color1, 0));

            gradient.colors.Add(new ColorNode(color2, color2Time));
            return gradient;
        }

        public static Gradient From2Color(Color color1, Color color2, float color2Time, float fadeTime)
        {
            Gradient gradient = new Gradient();
            gradient.colors.Add(new ColorNode(color1, 0));
            gradient.colors.Add(new ColorNode(color2, color2Time)); 
            
            Color clearColor = new Color(color2.R, color2.G, color2.B, (byte)0);
            gradient.colors.Add(new ColorNode(clearColor, 1));

            return gradient;
        }

        public static Gradient FadeInOut(Color color)
        {
            Gradient gradient = new Gradient();
            Color clearColor = new Color(color.R, color.G, color.B, (byte)0);

            gradient.colors.Add(new ColorNode(clearColor, 0));
            gradient.colors.Add(new ColorNode(color, .5f));
            gradient.colors.Add(new ColorNode(clearColor, 1));

            return gradient;
        }
    }
}
