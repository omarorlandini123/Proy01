﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DetalleVersion
    {

        public const int tipo_material = 0;
        public const int tipo_servicio = 1;

        public int idDetalle { get; set; }
        public Version version { get; set; }
        public Material mat { get; set; }
        public double cantidadSoli { get; set; }
        public double precioSoli { get; set; }
        public double totalSoli { get; set; }
        public int tipo { get; set; }
        public int criticidad { get; set; }
        public Prioridad prioridad { get; set; }
        public double alto { get; set; }
        public double ancho { get; set; }
        public double largo { get; set; }
        public string sustento { get; set; }
        public string uniSoli { get; set; }
        public DateTime FechaReg { get; set; }
        public Usuario UsuarioReg { get; set; }
        public DateTime FechaUltModif { get; set; }
        public DateTime UsuarioUltModif { get; set; }
        public List<Observacion> observaciones { get; set; }
        public List<Archivo> archivosSustento { get; set; }
        public List<MesEntSoli> mesesSoli {get;set;}
        public List<MesEntSoli> mesesEnt { get; set;}
        public List<Aprobacion> aprobaciones { get; set; }
    }
}
