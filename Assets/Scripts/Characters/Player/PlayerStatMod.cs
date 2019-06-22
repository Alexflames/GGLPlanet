public struct PlayerStatMod
{
    public void Reset() {
        this.SpeedMul = 1.0f;
    }

    public PlayerStatMod Zero {
        get
        {
            PlayerStatMod zero = new PlayerStatMod();
            zero.Reset();
            return zero;
        }
    }

    public float SpeedMul;
}
