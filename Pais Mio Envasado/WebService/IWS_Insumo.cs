﻿using DO;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWS_Insumo" in both code and config file together.
    [ServiceContract]
    public interface IWS_Insumo
    {
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Bare, 
            Method = "POST", 
            UriTemplate = "agregarInsumo")]
        bool agregarInsumo(DO_Insumo doInsumo);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<DO_Insumo> obtenerListaInsumos();
    }
}
