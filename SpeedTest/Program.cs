using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HexGrid;

namespace SpeedTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            int width = 512 * 1;
            int height = 512 * 1;

            using (HexGridRenderer r = new HexGridRenderer())
            {
                r.Width = width;
                r.Height = height;
                r.RenderThreaded(1);

                sw.Stop();

                r.Bitmap.Save("Test_stitched.png");
                
                Console.WriteLine($"1  {sw.Elapsed}");
            }
            sw.Restart();
            using (HexGridRenderer r = new HexGridRenderer())
            {
                r.Width = width;
                r.Height = height;
                r.RenderThreaded(2);

                sw.Stop();
                
                Console.WriteLine($"2  {sw.Elapsed}");
            }
            sw.Restart();
            using (HexGridRenderer r = new HexGridRenderer())
            {
                r.Width = width;
                r.Height = height;
                r.RenderThreaded(4);

                sw.Stop();
                
                Console.WriteLine($"4  {sw.Elapsed}");
            }
            sw.Restart();
            using (HexGridRenderer r = new HexGridRenderer())
            {
                r.Width = width;
                r.Height = height;
                r.RenderThreaded(8);

                sw.Stop();
                
                Console.WriteLine($"8  {sw.Elapsed}");
            }
            sw.Restart();
            using (HexGridRenderer r = new HexGridRenderer())
            {
                r.Width = width;
                r.Height = height;
                r.RenderThreaded(16);

                sw.Stop();
                
                Console.WriteLine($"16 {sw.Elapsed}");
            }
            sw.Restart();
            using (HexGridRenderer r = new HexGridRenderer())
            {
                r.Width = width;
                r.Height = height;
                r.RenderThreaded(24);

                sw.Stop();
                
                Console.WriteLine($"24 {sw.Elapsed}");
            }

            Console.ReadLine();
        }
    }
}
