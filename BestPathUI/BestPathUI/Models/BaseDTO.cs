using Models.Interfaces;
using System;

namespace Models
{
    public class BaseDTO : IBaseDTO
    {
        public Guid Id { get; set; }
    }
}
