using System.Runtime.InteropServices;
using System.Text;

namespace PolyTool
{
    namespace PrivateProfile
    {
        /// <summary>
        /// Ini ファイルの読み書きを扱うクラスです。
        /// </summary>
        public class IniFile
        {
            [DllImport("kernel32.dll")]
            private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

            [DllImport("kernel32.dll")]
            private static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

            /// <summary>
            /// Ini ファイルのファイルパスを取得、設定します。
            /// </summary>
            public string FilePath { get; set; }

            /// <summary>
            /// インスタンスを初期化します。
            /// </summary>
            /// <param name="filePath">Ini ファイルのファイルパス</param>
            public IniFile(string filePath)
            {
                FilePath = filePath;
            }
            /// <summary>
            /// Ini ファイルから文字列を取得します。
            /// </summary>
            /// <param name="section">セクション名</param>
            /// <param name="key">項目名</param>
            /// <param name="defaultValue">値が取得できない場合の初期値</param>
            /// <returns></returns>
            public string GetString(string section, string key, string defaultValue = "")
            {
                var sb = new StringBuilder(1024);
                var r = GetPrivateProfileString(section, key, defaultValue, sb, (uint)sb.Capacity, FilePath);
                return sb.ToString();
            }
            /// <summary>
            /// Ini ファイルから整数を取得します。
            /// </summary>
            /// <param name="section">セクション名</param>
            /// <param name="key">項目名</param>
            /// <param name="defaultValue">値が取得できない場合の初期値</param>
            /// <returns></returns>
            public int GetInt(string section, string key, int defaultValue = 0)
            {
                return (int)GetPrivateProfileInt(section, key, defaultValue, FilePath);
            }
            /// <summary>
            /// Ini ファイルに文字列を書き込みます。
            /// </summary>
            /// <param name="section">セクション名</param>
            /// <param name="key">項目名</param>
            /// <param name="value">書き込む値</param>
            /// <returns></returns>
            public bool WriteString(string section, string key, string value)
            {
                return WritePrivateProfileString(section, key, value, FilePath);
            }
        }
    }
}