using lkcode.hetznercloudapi.Objects.Universal;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.Network.Get
{
    public class Response
    {
        public List<Universal.Network> networks { get; set; }
        public Meta meta { get; set; }
    }
}
