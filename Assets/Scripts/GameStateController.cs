using System;

namespace PVZ
{
    public static class GameStateController
    {
        public static bool IsPlayable { get; set; }
        public static bool IsPaused { get; set; }
        public static bool IsPlaying { get; set; }
        public static bool IsStopped { get; set; }
        public static bool IsChoosingCards { get; set; }
    }
}
