using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace System_Init_Toolbox
{

    /// <summary>
    /// 新概念实用工具（其实就是获取uri中的txt的内容）
    /// </summary>
    public static partial class Utilities
    {
        /// <summary>
        /// HttpClient实例。
        /// </summary>
        private static HttpClient http_client = new HttpClient();

        /// <summary>
        /// 向指定的URI发送GET请求，并返回字符串。
        /// </summary>
        /// <param name="uri">指定的URI。</param>
        /// <returns>URI返回的字符串。</returns>
        public static async Task<string> get(string uri)
        {
            return await http_client.GetStringAsync(uri);
        }
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png); // 坑点：格式选Bmp时，不带透明度，但可以通过在程序里给予用户调节透明度的选项的方法来解决。

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
    }
}
