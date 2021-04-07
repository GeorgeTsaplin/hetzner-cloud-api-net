namespace lkcode.hetznercloudapi.Objects.Server.Delete
{
    public class Response : Universal.ISuccessResponse
    {
        public Objects.Server.Universal.ServerAction action { get; set; }
    }
}