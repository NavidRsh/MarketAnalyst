using System;
using System.Collections.Generic;
using System.Text;

namespace MarketAnalyst.Core.Dtos.Common
{
    public sealed class ErrorDto
    {
        public string Code { get; }
        public string Description { get; }
        public string Stack { get; }

        public ErrorDto(string code, string description, string stack = "")
        {
            Code = code;
            Description = description;
#if DEBUG
            Stack = stack;
#endif
        }
    }

}
