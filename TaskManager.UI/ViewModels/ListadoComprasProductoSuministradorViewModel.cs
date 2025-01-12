﻿using TaskManager.Dominio.Entidades;

namespace TaskManager.UI.ViewModels
{
    public class ListadoComprasProductoSuministradorViewModel
    {
        public IEnumerable<CompraProductoSuministradorModel> ComprasProductoSuministrador {  get; set; }
        public int PaginaActual { get; set; }
        public int PaginasTotal { get; set; }
    }
}
