using UnityEngine;

public interface Attack {
    void AttStart();
    void AttUpdate(float attackTimeLeft);
    void AttEnd();
    float duration {get;}
    int priority {get;}
    Color attackColor {get; }
}
