
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace httpListener.БД
{

using System;
    using System.Collections.Generic;
    
public partial class ProfileToPosition
{

    public System.Guid Id { get; set; }

    public System.Guid ProfileId { get; set; }

    public System.Guid PositionId { get; set; }

    public System.DateTimeOffset DateOff { get; set; }



    public virtual Profile Profile { get; set; }

    public virtual Position Position { get; set; }

}

}
