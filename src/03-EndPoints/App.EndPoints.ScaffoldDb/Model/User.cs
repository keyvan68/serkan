using System;
using System.Collections.Generic;

namespace App.EndPoints.ScaffoldDb.Model;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string Gender { get; set; } = null!;

    public string Province { get; set; } = null!;

    public string City { get; set; } = null!;
}
