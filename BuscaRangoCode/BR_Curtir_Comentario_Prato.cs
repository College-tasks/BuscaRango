
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
    
public partial class BR_Curtir_Comentario_Prato
{

    public int Id_Usuario { get; set; }

    public int Id_Comentario_Prato { get; set; }

    public bool Positivo { get; set; }



    public virtual BR_Comentario_Prato BR_Comentario_Prato { get; set; }

    public virtual BR_Usuario BR_Usuario { get; set; }

}

}
