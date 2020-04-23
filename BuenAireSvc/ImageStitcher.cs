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
            int maxNumOfLevels = 3;
            var tempSurface = SKSurface.Create(new SKImageInfo(512, 512));
            var canvas = tempSurface.Canvas;
            for (int level = maxNumOfLevels; level < 1; --level)
            {
                int numOfTiles = Convert.ToInt32(Math.Pow(4, level));
                int tilesToASide = Convert.ToInt32(Math.Sqrt(numOfTiles));
                for (int globalx = 0; globalx < tilesToASide; ++globalx)
                {
                    for (int globaly = 0; globaly < tilesToASide; ++globaly)
                    {
                        canvas.Clear(SKColors.Transparent);
                        for (int tilex = 0; tilex < 2; ++tilex)
                        {
                            for (int tiley = 0; tiley < 2; ++tiley)
                            {
                                string tilePath = $"{folderPath}/L{level}T{(globalx * 2 + tilex).ToString("D2")}{(globaly * 2 + tiley).ToString("D2")}";
                                if (File.Exists(tilePath))
                                {
                                    SKBitmap tile = SKBitmap.Decode(File.OpenRead(tilePath));
                                    canvas.DrawBitmap(tile, SKRect.Create(tilex*256, tiley*256, tile.Width, tile.Height));
                                }
                            }
                        }
                        SKBitmap bigTile = SKBitmap.FromImage(tempSurface.Snapshot());
                        SKBitmap littleTile = SKBitmap.FromImage(SKImage.Create(new SKImageInfo(256, 256)));
                        bigTile.ScalePixels(littleTile, SKFilterQuality.High);
                        SKImage outTile = SKImage.FromBitmap(littleTile);
                        var data = outTile.Encode();
                        FileStream stream = File.OpenWrite($"{folderPath}/L{level - 1}T{globalx.ToString("D2")}{globaly.ToString("D2")}");
                        data.SaveTo(stream);
                    }
                }
            }
        }
       
        public static void sliceTiles(FileStream image, string writeMode)        
        {
            SKImage sKImage = SKImage.FromEncodedData(image);
            if (sKImage.Height != sKImage.Width)
            {
                int sideLength = Math.Min(sKImage.Width, sKImage.Height);
                sKImage = sKImage.Subset(SKRectI.Create(0, 0, sideLength, sideLength));
            }

            if (sKImage.Width % 256 != 0)
            {
                var cropWidth = sKImage.Width % 256;
                sKImage = sKImage.Subset(SKRectI.Create(0, 0, sKImage.Width - cropWidth, sKImage.Height));
            }

            if (sKImage.Height % 256 != 0)
            {
                var cropHeight = sKImage.Height % 256;
                sKImage = sKImage.Subset(SKRectI.Create(0, 0, sKImage.Width, sKImage.Height - cropHeight));
            }

            int level = Convert.ToInt32(Math.Log(Math.Pow(sKImage.Width / 256, 2), 4));

            for (int x = 0; x < sKImage.Width; x += 256)
            {
                for (int y = 0; y < sKImage.Height; y += 256)
                {
                    SKImage tile = sKImage.Subset(SKRectI.Create(x, y, x + 256, y + 256));
                    if (writeMode == "mlm")
                    {
                        var stream = File.OpenWrite($"Data/MLMOutput/aqlatestL0T{(x/256).ToString("D2")}{(y/256).ToString("D2")}.png");
                        var data = tile.Encode();
                        data.SaveTo(stream);
                    }
                    else if (writeMode == "uafsmoke")
                    {
                        var stream = File.OpenWrite($"Data/UAFSmoke/aqlatestL{level}T{(x / 256).ToString("D2")}{(y / 256).ToString("D2")}.png");
                        var data = tile.Encode();
                        data.SaveTo(stream);
                    }
                }
            }
        }
    }
}
