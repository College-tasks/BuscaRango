//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BuscaRangoCode
{
    using System;
    using System.Collections.Generic;
    
    public partial class BR_Bairro
    {
        public BR_Bairro()
        {
            this.BR_Estabelecimento = new HashSet<BR_Estabelecimento>();
        }
    
        public int Id { get; set; }
        public int Id_Cidade { get; set; }
        public string Bairro { get; set; }
    
        public virtual ICollection<BR_Estabelecimento> BR_Estabelecimento { get; set; }
        public virtual BR_Cidade BR_Cidade { get; set; }
    }
}