namespace Shared.Contracts.V1.Routes
{
    public static class ApiRoutes
    {
        private const string Root = "api";

        private const string Base = Root + "/";

        public static class Entry
        {
            private const string Version = "v1";

            public const string GetAll = Base + Version + "/content";
            public const string Get = Base + Version + "/content/{contentKey}";
            public const string Create = Base + Version + "/content";
            public const string Update = Base + Version + "/content/{contentKey}";
            public const string Delete = Base + Version + "/content/{contentKey}";
        }
    }
}