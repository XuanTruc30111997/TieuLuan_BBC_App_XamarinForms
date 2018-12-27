using System;
using System.Collections.Generic;
using System.Text;

namespace bbc.Interfaces
{
    public interface IMessage
    {
        void LongToast(string message);
        void ShortToast(string message);
    }
}
