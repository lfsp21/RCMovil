namespace Clock.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    ///    MODELO PARA CAPTURAS TODOS LOS REGISTROS
    /// </summary>
    public class Register
    {

        [Key]
        public int RegistroId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Tiempo { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Entrada { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Salida { get; set; }
        public string ImagePath { get; set; }
        public string UserCode { get; set; }

        [NotMapped]
        public byte[] ImageArray { get; set; }
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "user";
                }
                return $"https://rcmovilapi.azurewebsites.net/{this.ImagePath.Substring(1)}";
            }
        }
    }
}
