using System;

namespace Domain.Dtos.ItemDtos
{
    public class ItemResponse : ItemBase
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
    }
}