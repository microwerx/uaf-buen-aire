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
            var tempSurface = SKSurface.Create(new SKImageInfo(512, 512));
            var canvas = tempSurface.Canvas;
            SKBitmap bigTile;
            SKBitmap littleTile;
            SKImage outTile;
            for (int level = maxNumOfLevels; level > 0; --level)
            {
                int numOfTiles = Convert.ToInt32(Math.Pow(4, level - 1));
                int tilesToASide = Convert.ToInt32(Math.Sqrt(numOfTiles));
                for (int globalx = 0; globalx < tilesToASide; ++globalx)
                {
                    for (int globaly = 0; globaly < tilesToASide; ++globaly)
                    {
                        bool tileExisted = true;
                        for (int tilex = 0; tilex < 2; ++tilex)
                        {
                            for (int tiley = 0; tiley < 2; ++tiley)
                            {
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
                                else
                                {
                                    canvas.Clear(SKColors.Transparent);
                                    bigTile = SKBitmap.FromImage(tempSurface.Snapshot());
                                    littleTile = SKBitmap.FromImage(SKImage.Create(new SKImageInfo(256, 256)));
                                    bigTile.ScalePixels(littleTile, SKFilterQuality.High);
                                    outTile = SKImage.FromBitmap(littleTile);
                                    images.Add($"L{level}T{(globalx * 2 + tilex).ToString("D2")}{(globaly * 2 + tiley).ToString("D2")}", outTile);
                                    tileExisted = false;
                                    continue;
                                }
                            }
                        }

                        if (tileExisted)
                        {
                            bigTile = SKBitmap.FromImage(tempSurface.Snapshot());
                            littleTile = SKBitmap.FromImage(SKImage.Create(new SKImageInfo(256, 256)));
                            bigTile.ScalePixels(littleTile, SKFilterQuality.High);
                            outTile = SKImage.FromBitmap(littleTile);
                            images.Add($"L{level - 1}T{globalx.ToString("D2")}{globaly.ToString("D2")}", outTile);
                        }
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
       
        public static void sliceTiles(FileStream image, string writePath)        
        {
            int tileLength = 256;

            SKImage sKImage = SKImage.FromEncodedData(image);
            var tmpSurface = SKSurface.Create(new SKImageInfo(tileLength, tileLength));
            var canvas = tmpSurface.Canvas;
            if (sKImage.Height != sKImage.Width)
            {
                int sideLength = Math.Min(sKImage.Width, sKImage.Height);
                sKImage = sKImage.Subset(SKRectI.Create(0, 0, sideLength, sideLength));
            }

            if (sKImage.Width % tileLength != 0)
            {
                var cropWidth = sKImage.Width % tileLength;
                sKImage = sKImage.Subset(SKRectI.Create(0, 0, sKImage.Width - cropWidth, sKImage.Height));
            }

            if (sKImage.Height % tileLength != 0)
            {
                var cropHeight = sKImage.Height % tileLength;
                sKImage = sKImage.Subset(SKRectI.Create(0, 0, sKImage.Width, sKImage.Height - cropHeight));
            }

            while (Math.Log(Math.Pow(sKImage.Width / tileLength, 2), 2) % 1 != 0) //Throw out parts of the image that will not create full tiles
            {
                sKImage = sKImage.Subset(SKRectI.Create(0, 0, sKImage.Width - tileLength, sKImage.Height - tileLength));
            }

            int level = Convert.ToInt32(Math.Log(Math.Pow(sKImage.Width / tileLength, 2), 4));

            for (int y = 0; y < sKImage.Height; y += tileLength)
            {
                for (int x = 0; x < sKImage.Width; x += tileLength)
                {
                    SKBitmap tile = SKBitmap.FromImage(sKImage.Subset(SKRectI.Create(x, y, tileLength, tileLength)));
                    canvas.Clear();
                    canvas.DrawBitmap(tile, SKRect.Create(0, 0, tileLength, tileLength));

                    var stream = File.OpenWrite($"{writePath}/aqlatestL{level}T{(x/ tileLength).ToString("D2")}{(y/ tileLength).ToString("D2")}.png");
                    
                    var data = tmpSurface.Snapshot().Encode();
                    data.SaveTo(stream);
                }
            }
        }
    }
}
