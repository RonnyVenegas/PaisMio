﻿using DO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_SolicitudInsumos
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        /// <summary>
        /// Permite guardar la solicitud de insumos junto a la lista de descarte y consumo en la base de datos.
        /// </summary>
        /// <param name="solicitudInsumos">La solicitud de insumos</param>
        /// <returns></returns>
        public bool guardarSolicitudInsumos(DO_SolicitudInsumos solicitudInsumos)
        {
            SqlCommand insert = new SqlCommand("INSERT INTO SOLICITUD_INSUMO (OPE_CORREO, PED_CODIGO, SUP_OPE_CORREO, EST_SOL_ESTADO, SOL_FECHA)" +
                "VALUES (@operadorId, @codigoPedido, @correoAdmin, @estado, @fecha)", conexion);
            insert.Parameters.AddWithValue("@operadorId", solicitudInsumos.correoOperario);
            insert.Parameters.AddWithValue("@codigoPedido", solicitudInsumos.codigoPedido);
            insert.Parameters.AddWithValue("@correoAdmin", solicitudInsumos.correoAdministrador);
            insert.Parameters.AddWithValue("@estado", solicitudInsumos.estado);
            insert.Parameters.AddWithValue("@fecha", solicitudInsumos.fechaSolicitud);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                if (insert.ExecuteNonQuery() > 0)
                {
                    SqlCommand tomarCodigo = new SqlCommand("SELECT SOL_CODIGO FROM SOLICITUD_INSUMO ORDER BY SOL_CODIGO DESC");
                    solicitudInsumos.codigoSolicitud = (int)tomarCodigo.ExecuteScalar();
                }
                if (!agregarInsumosSolicitud(solicitudInsumos))
                {
                    return false;
                }
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }
        private bool agregarInsumosSolicitud(DO_SolicitudInsumos solicitud)
        {
            string query = consumidosConstructor(solicitud) + " ";
            query += descartadosConstructor(solicitud);
            SqlCommand comando = new SqlCommand(query, conexion);
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                comando.ExecuteNonQuery();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }
        private string consumidosConstructor(DO_SolicitudInsumos solicitud)
        {
            string query;
            if (solicitud.listaConsumo.Count == 0)
            {
                return "";
            }
            else
            {
                query = "INSERT INTO SOL_A_CONSUMIR (INS_CODIGO, SOL_CODIGO, ACS_CANTIDAD) VALUES ";
            }
            
            foreach (DO_InsumoEnBodega insumo in solicitud.listaConsumo)
            {
                query += "("+insumo.insumo.codigo+solicitud.codigoSolicitud+insumo.cantidadDisponible+"),";
            }

            return query.Substring(0,query.Length-1);
        }
        private string descartadosConstructor(DO_SolicitudInsumos solicitud)
        {
            string query;
            if (solicitud.listaConsumo.Count == 0)
            {
                return "";
            }
            else
            {
                query = "INSERT INTO POR_DESCARTE (INS_CODIGO, SOL_CODIGO, PDS_CANTIDAD) VALUES ";
            }

            foreach (DO_InsumoEnBodega insumo in solicitud.listaDescarte)
            {
                query += "(" + insumo.insumo.codigo + solicitud.codigoSolicitud + insumo.cantidadDisponible + "),";
            }

            return query.Substring(0, query.Length - 1);
        }
        /// <summary>
        /// Reduce los insumos de la base de datos según la cantidad que tenga la solicitud de insumos
        /// </summary>
        /// <param name="solicitud">La solicitud de insumos</param>
        /// <returns></returns>
        public bool reducirInsumos (DO_SolicitudInsumos solicitud)
        {
            string query = "";
            foreach (DO_InsumoEnBodega insumo in solicitud.listaConsumo)
            {
                query += "UPDATE INS_ESTA_BOD SET IEB_CANTIDAD_DISPONIBLE = IEB_CANTIDAD_DISPONIBLE - " + insumo.cantidadDisponible +
                "WHERE BOD_CODIGO = " + solicitud.codigoBodega + " AND INS_CODIGO = "+ insumo.insumo.codigo + " ";
            }
            foreach (DO_InsumoEnBodega insumo in solicitud.listaDescarte)
            {
                query += "UPDATE INS_ESTA_BOD SET IEB_CANTIDAD_DISPONIBLE = IEB_CANTIDAD_DISPONIBLE - " + insumo.cantidadDisponible +
                "WHERE BOD_CODIGO = " + solicitud.codigoBodega + " AND INS_CODIGO = " + insumo.insumo.codigo + " ";
            }

            SqlCommand reducirCantidad = new SqlCommand(query, conexion);
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                reducirCantidad.ExecuteNonQuery();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }

        }
        /// <summary>
        /// Refleja en la base de datos la aceptación o rechazo de la solicitud de insumos
        /// </summary>
        /// <param name="admin"> Objeto DO de administrador </param>
        /// <param name="estado"> Nombre del estado a asignar(Debe ser un estado valido) </param>
        /// <param name="solicitud">La solicitud de insumos</param>
        /// <returns></returns>
        public bool decisionSolicitud(DO_Administrador admin, string estado, DO_SolicitudInsumos solicitud)
        {
            SqlCommand actualizarSolicitud = new SqlCommand("UPDATE SOLICITUD_INSUMO " +
                "SET SUP_OPE_CORREO = @adminId, EST_SOL_ESTADO = @estado" +
               "WHERE SOL_CODIGO = @codigoSolicitud");
            actualizarSolicitud.Parameters.AddWithValue("@adminId", admin.correo);
            actualizarSolicitud.Parameters.AddWithValue("@estado", estado);
            actualizarSolicitud.Parameters.AddWithValue("@codigoSolicitud", solicitud.codigoSolicitud);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                actualizarSolicitud.ExecuteNonQuery();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }

    }
}
