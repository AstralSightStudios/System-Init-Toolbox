using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

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
    }
}
