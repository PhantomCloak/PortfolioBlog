namespace Shared.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string Root = "api";

        private const string Base = Root + "/";

        public static class Entry
        {
            private const string Version = "v1";

            public const string GetAll = Base + Version + "/basket";
            public const string Get = Base + Version + "/basket/{productId}";
            public const string Create = Base + Version + "/basket";
            public const string Update = Base + Version + "/basket/{productId}";
            public const string Delete = Base + Version + "/basket/{productId}";
        }
    }
}