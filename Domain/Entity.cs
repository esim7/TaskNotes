﻿using System;

namespace Domain
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}