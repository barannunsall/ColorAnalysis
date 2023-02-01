using ColorAnalitycs;
using System.Drawing;
using System.Net;

Helper helper = new();


helper.CsvRead();

//HashSet<Color> GetUniqueColors(Bitmap image)
//{
//    var colors = new HashSet<Color>();
//    for (int x = 0; x < image.Width; x++)
//    {
//        for (int y = 0; y < image.Height; y++)
//        {
//            var color = image.GetPixel(x, y);
//            if (!colors.Contains(color))
//            {
//                colors.Add(color);
//            }
//        }
//    }
//    return colors;
//}

//HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://d2cqobm8wcb2vo.cloudfront.net/crops/dd/5d4ab52a9ff3d57444446386_637007739305867.jpg");
//HttpWebResponse response = (HttpWebResponse)request.GetResponse();

//Stream stream = response.GetResponseStream();
//Bitmap image = new Bitmap(stream);

//stream.Close();
//response.Close();

//Color color = new Color();
//color.ToKnownColor();
//int colorCount = 0;
//int greyScale = 0;
//double totalSaturation = 0;
//double totalHue = 0;
//Color imageColor = image.GetPixel(0, 0);
//for (int x = 0; x < image.Width; x++)
//{
//    for (int y = 0; y < image.Height; y++)
//    {
//        Color pixelColor = image.GetPixel(x, y);
//        var hue = pixelColor.GetSaturation();
//        var light = pixelColor.GetBrightness();
//        if (hue <= 0.06 && light <= 20)
//        {
//            greyScale++;
//        }
//        else
//        {
//            colorCount++;
//            totalSaturation += pixelColor.GetSaturation();
//            totalHue += pixelColor.GetBrightness();
//        }
//    }
//};


//int totalPixels = image.Width * image.Height;
//double colorPercentage = (double)colorCount / totalPixels;
//double averageSat = totalSaturation / colorCount;
//double averageBlack = (double)greyScale / totalPixels;
//double avgLight = totalHue / totalPixels;
//Console.WriteLine(totalPixels);
//Console.WriteLine(Math.Round(averageBlack, 2));
//Console.WriteLine(Math.Round(colorPercentage, 3));
//var colors = color.ToKnownColor();
//var r = color.GetSaturation();


//if (greyScale * 0.2 > colorCount)
//    Console.WriteLine("resim gri " + greyScale + " " + colorCount);
//else
//    Console.WriteLine("resim renkli " + greyScale + " " + colorCount);


//Console.WriteLine($"Color Percentage: {Math.Round(colorPercentage, 2)}, Average Saturation: {Math.Round(averageSat)}, GreyScale: {greyScale}, Color Count: {colorCount}, Total Pixel: {totalPixels}, AVG Hue: {Math.Round(avgLight}");