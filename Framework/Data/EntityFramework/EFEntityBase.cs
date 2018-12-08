using System;

namespace Framework.Data.EntityFramework
{
    public class EFEntityBase
    {
        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Data de atualização do registro
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
