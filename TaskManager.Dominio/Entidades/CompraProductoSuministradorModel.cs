using TaskManager.Dominio.Enum;

namespace TaskManager.Dominio.Entidades
{
    public class CompraProductoSuministradorModel
    {
        public int Id { get; set; }
        public string FacturacionID { get; set; }
        public int OrdenAdquisicionId { get; set; }
        public OrdenAdquisicionModel OrdenAdquisicion { get; set; }
        public IEnumerable<OrdenAdquisicionModel> Ordenes { get; set; }
        public string PaginaWebEmpresa { get; set; }
        public string CorreoElectronicoEmpresa { get; set; }
        public string TelefonoEmpresa { get; set; }
        public string NombreLegalDeLaEmpresa { get; set; }
        public string CIF { get; set; }
        public string DireccionLinea1 { get; set; }
        public string DireccionLinea2 { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public EstadoDeCompraEnum EstadoDeCompra { get; set; }
        public MetodoDePagoEnum MetodoDePago { get; set; }
        public DateTime FechaDeEmision { get; set; }
        public double? TasaDeIva { get; set; }
        public double? TasaDeTransporte { get; set; }
        public double? TasaDeDescuento { get; set; }
        public double? GastosDeMantenimiento { get; set; }
        public double? GastosDeEnvio { get; set; }
        public TerminosPagoEnum TerminosPago { get; set; }
        public CompraProductoSuministradorModel()
        {
            Id = new int();
            FacturacionID = GenerarFacturacionID();
        }
        private string GenerarFacturacionID()
        {
            string companyAbbreviation = "RFC";
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString("D2");
            string day = DateTime.Now.Day.ToString("D2");
            string uniqueID = Guid.NewGuid().ToString("N").Substring(0, 3); 
            return $"{companyAbbreviation}-{year}{month}{day}-{uniqueID}";

        }
    }
}
