﻿using System;
using System.Collections.Generic;

namespace WebApp.Models.VideoGames;

public partial class Genre
{
    public int Id { get; set; }

    public string? GenreName { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
