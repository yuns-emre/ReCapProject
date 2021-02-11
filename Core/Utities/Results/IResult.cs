using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utities.Results
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
