public enum InvaderType {
    Invader1,
    Invader2,
    Invader3,
    MysteryShip
}
public enum GameState {
    StartPlay,              // Prepare to play
    Playing,                // Playing
    Win,                    // IncreaseDifficulty: Voltar balas e inimigos mais forte => Playing
    Die,                    // Voltar balas, inimigos, base, player, e valores iniciais => Playing (Como se fosse o Win)
    GameOver                // Voltar menu (Set Demo)
}
public enum ExplosionType {
    Invader,
    BaseShelter,
    Cannon,
    MysteryShip
}