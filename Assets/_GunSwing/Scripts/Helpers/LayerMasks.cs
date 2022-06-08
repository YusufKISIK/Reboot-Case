public static class LayerMasks
{
    public static int InputRaycastLayer = 3;
    public static int UILayer = 5;
    public static int ExtraLightLayer = 6;
    public static int AvoidLightLayer = 7;
    public static int EnvironmentLayer = 8;
    public static int PlatformLayer = 9;
    public static int InteractableLayer = 10; 
    public static int ObstacleLayer = 11;
    public static int GameStateLayer = 12;
    public static int MultiplierLayer = 13;
    public static int PipeLayer = 14;
    public static int BrideLayer = 15;

    public static int InputRaycastMask = 1 << InputRaycastLayer;
    public static int UIMask = 1 << UILayer;
    public static int ExtraLightMask= 1 << ExtraLightLayer;
    public static int AvoidLightMask = 1 << AvoidLightLayer;
    public static int EnvironmentMask = 1 << EnvironmentLayer;
    public static int PlatformMask = 1 << PlatformLayer;
    public static int InteractableMask = 1 << InteractableLayer;
    public static int ObstacleMask = 1 << ObstacleLayer;
    public static int GameStateMask = 1 << GameStateLayer;
    public static int MultiplierMask = 1 << MultiplierLayer;
    public static int PipeMask = 1 << PipeLayer;
    public static int BrideMask = 1 << BrideLayer;
}