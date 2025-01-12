// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("vzrJoV/yPe3syK7LA9Vkmt8LRNFM/n1eTHF6dVb6NPqLcX19fXl8fz1wihkCGEE1FJR87Xr2I5s9L8Qvs47gbZWAyKxpAmrp/A598+qFRf/rHlu8G9pSoi+E3yJR2ySTudmaTf59c3xM/n12fv59fXzCk8Mx26nQcaBegtnycKu/gA2Vrmwk79GGB6IEO+FEsBFrWv7i7usZuQVqx6Wz97PbO1GDHwxrIw0TvA0Ip/LlAE90SAuhfeLYdwYILa2Djsx23x4Y04yd6Wr6nrfX7pZT62b9YegQv/FxTZxYtwFQnz4Nd2dixZkGv0fmWq5R7IKSCpqGSE1qnqw+4RRKV8Iqz6jmMQZiKa0hfkyT9c7YNVxa026+YZaYTp3EAs0Df35/fXx9");
        private static int[] order = new int[] { 6,6,9,9,5,6,6,13,13,9,11,11,12,13,14 };
        private static int key = 124;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
