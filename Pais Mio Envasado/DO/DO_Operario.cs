﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DO
{
    /// <summary>
    /// En esta classe se representa a los operarios, son los usuarios base del sistema por lo que heredan a los supervisores
    /// </summary>
    /// 
    [DataContract]
    public class DO_Operario
    {
        [DataMember(Name ="correo")]
        public String correo { set; get; }

        [DataMember(Name = "estado")]
        public DO_EstadoHabilitacion estado { set; get; }

        [DataMember(Name = "nombre")]
        public String nombre { set; get; }

        [DataMember(Name = "apellidos")]
        public String apellidos { set; get; }

        [DataMember(Name = "contrasena")]
        public String contrasena { set; get; }

        public DO_Operario(string correo, DO_EstadoHabilitacion estado, string nombre, string apellidos, string contrasena)
        {
            this.correo = correo;
            this.estado = estado;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.contrasena = contrasena;
        }

        public DO_Operario()
        {
        }
    }
}
