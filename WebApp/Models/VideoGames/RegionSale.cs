﻿using System;
using System.Collections.Generic;

namespace WebApp.Models.VideoGames;

public partial class RegionSale
{
    public int? RegionId { get; set; }

    public int? GamePlatformId { get; set; }

    public decimal? NumSales { get; set; }

    public virtual GamePlatform? GamePlatform { get; set; }

    public virtual Region? Region { get; set; }
}
