using System;
using System.Collections.Generic;

namespace webAPI_demo1.Models;

public partial class User
{
    public int? Id { get; set; }

    public string UserName { get; set; } = null!;

    public string? Password { get; set; } = null!;
}
