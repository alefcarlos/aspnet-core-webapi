namespace Application.Contracts.Shared
{
    /// <summary>
    /// Contrato de request comum para informação de endereço
    /// </summary>
    public class AddressRequest
    {
        /// <summary>
        /// CEP
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Nome da rua
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Número da casa
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Informações complementares
        /// </summary>
        public string AdditionalInformations { get; set; }

        /// <summary>
        /// Nome do bairro
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Sigla do estado
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Nome do país
        /// </summary>
        public string Country { get; set; }
    }
}
