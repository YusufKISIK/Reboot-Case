public enum Scenes
{
    Menu,
    Game
}

public enum AnimationType
{
    Success,
    Fail
}

public enum GameEventType
{
    Pickup,
    Obstacle
}

public enum PickupType
{
    //Bar,
    Rope,
    Drink
}

public enum ObstacleType
{
    Voxels,
    Wall
}

public enum GameState
{
    Run,
    End,
    Fail,
    Tap,
    Zipline,
    Watch,
    Bounce,
    None
}