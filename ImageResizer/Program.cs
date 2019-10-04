using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();

            ImageProcess imageProcess = new ImageProcess();

            #region 同步

            imageProcess.Clean(destinationPath);

            sw1.Start();
            imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
            sw1.Stop();

            Console.WriteLine($"花費時間: {sw1.ElapsedMilliseconds} ms");

            #endregion 同步

            #region 非同步

            imageProcess.Clean(destinationPath);

            sw2.Start();
            await imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0);
            sw2.Stop();

            Console.WriteLine($"花費時間: {sw2.ElapsedMilliseconds} ms");

            #endregion 非同步

            double efficacy = Math.Round(Convert.ToDouble(sw1.ElapsedMilliseconds - sw2.ElapsedMilliseconds) / sw1.ElapsedMilliseconds * 100, 4);

            Console.WriteLine($"提升效能: {efficacy} ");
            Console.ReadKey();
        }
    }
}
