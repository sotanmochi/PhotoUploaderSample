using UnityEngine;
using ZXing;
using ZXing.QrCode;

namespace PhotoUploader
{
    public class QRCodeGenerator
    {
        public static Texture2D GenerateQRCodeTexture(string text, int width, int height)
        {
            Texture2D qrTex = new Texture2D(width, height);

            Color32[] pixels = Encode(text, width, height);
            qrTex.SetPixels32(pixels);
            qrTex.Apply();

            return qrTex;
        }

        public static Color32[] Encode(string text, int width, int height)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width
                }
            };
            return writer.Write(text);
        }
    }    
}
