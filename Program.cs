using System.Drawing;
using ZXing;

public class Application
{
    private static readonly string PathToImage = @"D:\C#\CodingTheory\QrAndBarcode\Code.png";

    private static readonly Size ImageSize = new Size(400, 400);
    private static readonly Dictionary<EncodeHintType, object> EncodingHints = new()
    {
        [EncodeHintType.CHARACTER_SET] = "windows-1251",
    };

    public static void Main()
    {
        var message = Console.ReadLine();
        var byteArray = EncodeTextToByteArray(message, BarcodeFormat.QR_CODE, ImageSize.Width, ImageSize.Height);
        var image = GenerateImageFromByteArray(byteArray);
        image.Save(PathToImage);
    }

    private static byte[,] EncodeTextToByteArray(string text, BarcodeFormat format, int width, int height)
    {
        var writer = new MultiFormatWriter();
        var bitMatrix = writer.encode(text, format, width, height, EncodingHints);

        var result = new byte[bitMatrix.Width, bitMatrix.Height];
        for (var y = 0; y < bitMatrix.Height; y++)
        {
            for (var x = 0; x < bitMatrix.Width; x++)
            {
                var byteValue = (byte)(bitMatrix[x, y] ? 0 : 255);
                result[x, y] = byteValue;
            }
        }

        return result;
    }

    private static Image GenerateImageFromByteArray(byte[,] byteArray)
    {
        var bitmap = new Bitmap(byteArray.GetLength(0), byteArray.GetLength(1));
        for (var y = 0; y < bitmap.Height; y++)
        {
            for (var x = 0; x < bitmap.Width; x++)
            {
                var byteValue = byteArray[x, y];
                bitmap.SetPixel(x, y, Color.FromArgb(byteValue, byteValue, byteValue));
            }
        }

        return bitmap;
    }
}