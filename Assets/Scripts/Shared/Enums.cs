public enum GameState {
    StartPlay,              // Prepare the environment to a new game and reset values
    Playing,                // Gameplay
    Die,                    // Stop Enemies, Player
    GameOver                // Clean the screen
}

public enum InvaderType {
    Invader1,
    Invader2,
    Invader3,
    MysteryShip
}

public enum ExplosionType {
    Invader,
    BaseShelter,
    Cannon,
    MysteryShip
}