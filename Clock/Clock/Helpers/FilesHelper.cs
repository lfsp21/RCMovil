namespace Clock.Helpers
{
    using System.IO;

    public class FilesHelper
    {
        /// <summary>
        /// METODO QUE HACE LECTURA Y CONVIERTE LA IMAGEN CAPTURADA A UN ARREGLO DE BYTES
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
