using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EjercicioA2_4.Model
{
    public class Video
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string nombre { get; set; }
        public string Path { get; set; }
        public DateTime Date { get; set; }

    }
}
