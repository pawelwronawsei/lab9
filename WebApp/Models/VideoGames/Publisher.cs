﻿using System;
using System.Collections.Generic;

namespace WebApp.Models.VideoGames;

public partial class Publisher
{
    public int Id { get; set; }

    public string? PublisherName { get; set; }

    public virtual ICollection<GamePublisher> GamePublishers { get; set; } = new List<GamePublisher>();
}
