﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DO;
using BL;

namespace WebService
{
    /// <summary>
    /// Clase que implementa los métodos de la interfaz IWS_Usuario.cs
    /// </summary>
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WS_Usuario" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WS_Usuario.svc or WS_Usuario.svc.cs at the Solution Explorer and start debugging.
    public class WS_Usuario : IWS_Usuario
    {
        public DO_Operario consultarUsuario(string correo)
        {
            BL_Operario BLoperario = new BL_Operario();
            return BLoperario.buscarOperario(correo);
        }

        public bool crearUsuario2(DO_Operario usuario)
        {
            if (usuario.correo.Equals("")) {
                return false;
            }
            return true;
        }


        //public bool crearUsuario(string tipoUsuario, string correo, string estado, string nombre, string apellidos, string contrasena)
        //{
        //    DO_Operario usuario = new DO_Operario();
        //    usuario.correo = correo.Trim();
        //    usuario.contrasena = contrasena.Trim();
        //    usuario.nombre = nombre.Trim();
        //    usuario.apellidos = apellidos.Trim();
        //    usuario.estado = new DO_EstadoHabilitacion();
        //    usuario.estado.estado = estado.Trim();

        //    if (usuario.correo.Equals("") || usuario.contrasena.Equals("") || usuario.estado.estado.Equals("")
        //        || usuario.nombre.Equals("") || usuario.apellidos.Equals(""))
        //    {
        //        return false;
        //    }

        //    if (tipoUsuario.Equals("OPERARIO"))
        //    {
        //        BL_Operario BLoperario = new BL_Operario();

        //        return BLoperario.agregarOperario(correo, usuario.estado, nombre, apellidos, contrasena);
        //    }

        //    if (tipoUsuario.Equals("SUPERVISOR")) {
        //        BL_Supervisor BLsupervisor = new BL_Supervisor();
        //        return BLsupervisor.agregarSupervisor(correo, usuario.estado, nombre, apellidos, contrasena);
        //    }

        //    if (tipoUsuario.Equals("ADMINISTRADOR")) {
        //        BL_Administrador BLadministrador = new BL_Administrador();
        //        return BLadministrador.agregarAdministrador(correo, usuario.estado, nombre, apellidos, contrasena);
        //    }

        //    return false;
        //}

        /// <summary>
        /// Método para crear usuarios, ya sea Operario, Supervisor o Administrador
        /// </summary>
        /// <param name="usuario">Objeto usuario <param>
        /// <param name="tipo"> tipo de usuario</param>
        /// <returns></returns>
        public bool crearUsuario(DO_Operario usuario, String tipo)
        {

            if (usuario.correo.Equals("") || usuario.contrasena.Equals("") || usuario.estado.estado.Equals("")
                || usuario.nombre.Equals("") || usuario.apellidos.Equals(""))
            {
                return false;
            }

            if (tipo.Equals("OPERARIO")) {

                BL_Operario BLoperario = new BL_Operario();
                return BLoperario.agregarOperario(usuario.correo, usuario.estado, usuario.nombre, usuario.apellidos, usuario.contrasena);
            }

            if (tipo.Equals("SUPERVISOR"))
            {
                BL_Supervisor BLsupervisor = new BL_Supervisor();
                return BLsupervisor.agregarSupervisor(usuario.correo, usuario.estado, usuario.nombre, usuario.apellidos, usuario.contrasena);
            }

            if (tipo.Equals("ADMINISTRADOR"))
            {
                BL_Administrador BLadministrador = new BL_Administrador();
                return BLadministrador.agregarAdministrador(usuario.correo, usuario.estado, usuario.nombre, usuario.apellidos, usuario.contrasena);
            }

            return false;

        }

        public List<DO_Operario> obtenerListaOperario()
        {
            BL_Operario BLoperario = new BL_Operario();
            return BLoperario.obtenerListaOperario();
        }

        public List<DO_Operario> recibirLista(List<DO_Operario> lista)
        {

            return lista;
        }
    }
}
