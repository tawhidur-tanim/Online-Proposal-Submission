using System;
using System.Collections.Generic;

namespace ProjectFinal101.Core.Resources
{
    public class AuthDataResource
    {
        public string Token { get; set; }

        public DateTime ExpireTime { get; set; }

        public string Id { get; set; }

        public IList<string> Roles { get; set; }
    }
}
