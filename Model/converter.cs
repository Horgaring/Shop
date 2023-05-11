public static class Converter{
   public static byte[] Inttobyte(int[] a){
        byte[] b = new byte[a.Length];
        for (var item = 0;item < a.Length; item++){
            b[item] = (byte)a[item];
        }
        return b;
   }
}