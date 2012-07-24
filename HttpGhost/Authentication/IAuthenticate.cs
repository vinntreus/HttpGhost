using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpGhost.Transport;

namespace HttpGhost.Authentication
{
    public interface IAuthenticate
    {
        void Process(Request request);
    }
}
