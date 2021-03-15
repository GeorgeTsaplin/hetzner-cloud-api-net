namespace lkcode.hetznercloudapi.Api
{
    /// <summary>
    /// Private network information.
    /// </summary>
    public class PrivateNetwork
    {
        /// <summary>
        /// Network ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// IPv4 address assigned to this server.
        /// </summary>
        public string IP { get; set; }
    }
}