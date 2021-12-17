﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ExamenIIIParcial.Models
{
   

   public class Pagos
    {
       [PrimaryKey,AutoIncrement]
        public int IdPago { get; set; }
        [MaxLength(50)]
       public string Descripcion { get; set; }
        [MaxLength(60)]
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public byte [] FotoRecibo { get; set; }
    }
  

}
