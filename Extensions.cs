using smr.Middleware;

namespace smr
{
    public static class Extensions
    {
        public static IApplicationBuilder UseTrack(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpMiddleware>();
        }
    }
}