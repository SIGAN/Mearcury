using Mearcury.Debugging;

namespace Mearcury
{
    public static class MearcuryConsts
    {
        public const string LocalizationSourceName = "Mearcury";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;

        public const int MaxNameLength = 120;

        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "a316836897134b86adfc6d449334f002";
    }
}
