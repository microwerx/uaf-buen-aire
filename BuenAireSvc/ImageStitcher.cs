using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BuenAireSvc
{
    public class ImageStitcher
    {
        public static void mergeTiles(string folderPath)
        { 
            if (folderPath.Last() == '/' || folderPath.Last() == '\\')
            {
                folderPath = folderPath.Remove(folderPath.Length - 1);
            }

            Dictionary<string, SKImage> images = new Dictionary<string, SKImage>();
            int maxNumOfLevels = 3;
            for (int level = maxNumOfLevels; level > 0; --level)
            {
                int numOfTiles = Convert.ToInt32(Math.Pow(4, level - 1));
                int tilesToASide = Convert.ToInt32(Math.Sqrt(numOfTiles));
                for (int globaly = 0; globaly < tilesToASide; ++globaly)
                {
                    for (int globalx = 0; globalx < tilesToASide; ++globalx)
                    {
                        var tempSurface = SKSurface.Create(new SKImageInfo(512, 512));
                        var canvas = tempSurface.Canvas;
                        canvas.Clear(SKColors.Transparent);
                        for (int tiley = 0; tiley < 2; ++tiley)
                        {
                            for (int tilex = 0; tilex < 2; ++tilex)
                            {
                                int imgx = (globalx * 2 + tilex);
                                int imgy = (globaly * 2 + tiley);
                                string tilePath = $"{folderPath}/aqlatestL{level}T{(globalx * 2 + tilex).ToString("D2")}{(globaly * 2 + tiley).ToString("D2")}.png";
                                string tileKey = $"L{level}T{(globalx * 2 + tilex).ToString("D2")}{(globaly * 2 + tiley).ToString("D2")}";
                                if (images.ContainsKey(tileKey))
                                {
                                    SKBitmap tile = SKBitmap.FromImage(images[tileKey]);
                                    canvas.DrawBitmap(tile, SKRect.Create(tilex * 256, tiley * 256, tile.Width, tile.Height));
                                }
                                else if (File.Exists(tilePath))
                                {
                                    SKBitmap tile = SKBitmap.Decode(File.OpenRead(tilePath));
                                    canvas.DrawBitmap(tile, SKRect.Create(tilex * 256, tiley * 256, tile.Width, tile.Height));
                                }
                            }
                        }
                        SKBitmap bigTile = SKBitmap.FromImage(tempSurface.Snapshot());
                        SKBitmap littleTile = SKBitmap.FromImage(SKImage.Create(new SKImageInfo(256, 256)));
                        bigTile.ScalePixels(littleTile, SKFilterQuality.High);
                        SKImage outTile = SKImage.FromBitmap(littleTile);
                        images.Add($"L{level - 1}T{globalx.ToString("D2")}{globaly.ToString("D2")}", outTile);
                    }
                }
            }

            foreach(KeyValuePair<string, SKImage> file in images)
            {
                var data = file.Value.Encode();
                var stream = File.OpenWrite($"{folderPath}/aqlatest{file.Key}.png");
                data.SaveTo(stream);
            }
        }
       
        public static void sliceTiles(FileStream image, Tuple<double, double> topLeft, Tuple<double, double> bottomRight, string writePath)        
        {
            const int tileLength = 256;
            const int bottomLevel = 3;
            int tilesPerDim = Convert.ToInt32(Math.Pow(2, Convert.ToDouble(bottomLevel)));
            int pixelsPerDim = tilesPerDim * tileLength;
            double degreesLatPerPixel = 180d / pixelsPerDim;
            double degreesLonPerPixel = degreesLatPerPixel * 2;
            

            Tuple<int, int> imageTopLeft = new Tuple<int, int>(Convert.ToInt32((90 - topLeft.Item1) / degreesLatPerPixel), Convert.ToInt32((180 + topLeft.Item2) / degreesLonPerPixel));
            Tuple<int, int> imageBottomRight = new Tuple<int, int>(Convert.ToInt32((90 - bottomRight.Item1) / degreesLatPerPixel), Convert.ToInt32((180 + bottomRight.Item2) / degreesLonPerPixel));

            SKImage sKImage = SKImage.FromEncodedData(image);
            var fullSurface = SKSurface.Create(new SKImageInfo(pixelsPerDim, pixelsPerDim));
            var canvas = fullSurface.Canvas;

            SKBitmap scaledImage = new SKBitmap(imageBottomRight.Item2 - imageTopLeft.Item2, imageBottomRight.Item1 - imageTopLeft.Item1);
            SKBitmap.FromImage(sKImage).ScalePixels(scaledImage, SKFilterQuality.High);
            canvas.DrawBitmap(scaledImage, new SKPoint(imageTopLeft.Item2, imageTopLeft.Item1));

            SKImage fullImage = fullSurface.Snapshot();
            var tileSurface = SKSurface.Create(new SKImageInfo(tileLength, tileLength));
            canvas = tileSurface.Canvas;
            for (int y = 0; y < fullImage.Height; y += tileLength)
            {
                for (int x = 0; x < fullImage.Width; x += tileLength)
                {
                    SKBitmap tile = SKBitmap.FromImage(fullImage.Subset(SKRectI.Create(x, y, tileLength, tileLength)));
                    canvas.Clear(SKColors.Transparent);
                    canvas.DrawBitmap(tile, SKRect.Create(0, 0, tileLength, tileLength));

                    var stream = File.OpenWrite($"{writePath}/aqlatestL{bottomLevel}T{(x / tileLength).ToString("D2")}{(y / tileLength).ToString("D2")}.png");

                    var data = tileSurface.Snapshot().Encode();
                    data.SaveTo(stream);
                }
            }
        }
    }
}
