﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DAO;

namespace BL
{
    /// <summary>
    /// Esta clase llama a los métodos del DAO_Adminstrador
    /// </summary>
    public class BL_Administrador
    {
        /// <summary>
        /// Metodo para llamar al meétodo agregarOperario del DAO_Aministrador
        /// </summary>
        /// <param name="correo"> correo del administrador</param>
        /// <param name="estado"> estado del administrador, HABILITADO o DESHABILITADO</param>
        /// <param name="nombre"> nombre del administrador</param>
        /// <param name="apellidos"> apellidos del administrador</param>
        /// <param name="contrasena"> contrasena del administrador</param>
        /// <returns>true si se agregó correctamente, false si ocurrió algún error</returns>
        public bool agregarAdministrador(string correo, DO_EstadoHabilitacion estado, string nombre, string apellidos, string contrasena) {

            DAO_Operario DAOoperario = new DAO_Operario();
            DAO_Supervisor DAOsupervisor = new DAO_Supervisor();
            DAO_Administrador DAOadministrador = new DAO_Administrador();

            String supervisor = DAOoperario.getQueryInsertar() + DAOsupervisor.getQueryInsertar();

            return DAOadministrador.agregarAdministrador(correo, estado, nombre, apellidos, contrasena, supervisor);
        }
    }
}
